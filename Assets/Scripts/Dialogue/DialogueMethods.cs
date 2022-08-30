using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Dialogue
{
    public class DialogueMethods : MonoBehaviour
    {
        private IEnumerator coroutine;

        private DialogueController controller;
        private float textSpeed = 0.07f;
        private bool isPrinting = false;
        public bool IsPrinting 
        {
            get { return isPrinting; }
            set 
            {
                isPrinting = value;
                if (isPrinting == false)
                {
                    Image image = controller.DialogueCircle.GetComponent<Image>();
                    image.enabled = true;
                    coroutine = FadeInAndOut(image);
                    StartCoroutine(coroutine);
                    
                }
                else
                {
                    controller.DialogueCircle.GetComponent<Image>().enabled = false;
                    if (coroutine != null)
                    {
                        StopCoroutine(coroutine);
                    }
                    coroutine = null;
                } 
            }
        }
        private int dialogueLength = 0;
        private int dialogueIndex = 0;


        public DialogueUnits dialogues { get; set; }
        public static DialogueMethods instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                controller = DialogueController.instance;
                dialogues = GameManager.instance.LoadJsonFile<DialogueUnits>("Assets/Resources/Dialogues", "dialogue1");

                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            RegisterActions();
        }

        private void RegisterActions()
        {
            List<DialogueUnit> list = dialogues.data;
            DialogueUnit tempUnit;
            tempUnit = list[1];
            tempUnit.Action += GeneralDialogueAfterChangeBackGround;
            tempUnit.EtcInfo.Add("BackGroundImage", "DialogueExample");

            tempUnit = list[4];
            tempUnit.Action += GeneralDialogueAfterChangeBackGround;
            tempUnit.EtcInfo.Add("BackGroundImage","School");
        }

        public void ExecutePerFrame()
        {
            //현재 출력중이 아니며 스페이스를 눌렀을때 대화단위(DialogueUnit)의 액션 실행
            if (!isPrinting && GameManager.instance.SubmitDowned)
            {
                if (dialogueIndex >= dialogues.data.Count)
                {
                    ExecuteEndOfDialogues(new DialogueUnit(""));
                }
                else
                {
                    Debug.Log(dialogueIndex);
                    dialogues.data[dialogueIndex].CallAction();
                    dialogueIndex++;
                }
            }
        }

        public void ExecuteEndOfDialogues(DialogueUnit unit)
        {
            StartCoroutine(ExecuteEndOfDialoguesCor(unit));
        }

        public IEnumerator ExecuteEndOfDialoguesCor(DialogueUnit unit)
        {
            IsPrinting = true;
            TextMeshProUGUI speakerTMPro = controller.SpeakerText.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dialogueTMPro = controller.DialogueText.GetComponent<TextMeshProUGUI>();
            string speaker = "";
            string sentence = "끝났을때 실행되는 메소드 여기서 다음 대화를 로드하든 탐색챕터로 넘어가게 하든 할 수 있다.";

            speakerTMPro.text = speaker;

            for (int i = 1; i <= sentence.Length; i++)
            {
                string slicedSentence = sentence.Substring(0, i);
                string lastchar = sentence.Substring(i - 1, 1);

                dialogueTMPro.text = slicedSentence;

                if (lastchar == "\n" || lastchar == "," || lastchar == ".")
                    yield return new WaitForSeconds(textSpeed * 5);
                else
                    yield return new WaitForSeconds(textSpeed);


                if (GameManager.instance.SubmitDowned)
                {
                    dialogueTMPro.text = sentence;
                    break;
                }
            }
            IsPrinting = false;
        }

        public void GeneralDialogue(DialogueUnit unit)
        {
            StartCoroutine(GeneralDialogueCor(unit));
        }

        public void GeneralDialogueAfterChangeBackGround(DialogueUnit unit)
        {
            StartCoroutine(GeneralDialogueAfterChangeBackGrCor(unit));
        }

        public IEnumerator GeneralDialogueAfterChangeBackGrCor(DialogueUnit unit)
        {
            IsPrinting = true;

            GameObject beforeBackGroundImage = GameObject.Find("BackGroundImage(Clone)");
            GameObject backGroundImage = GameManager.LoadPrefab("Dialogue/BackgroundImage");
            Image image = backGroundImage.GetComponent<Image>();
            image.sprite = GameManager.LoadImage(unit.EtcInfo["BackGroundImage"] as string);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            backGroundImage = Instantiate(backGroundImage, controller.BaseBgrnd.transform);


            int beforeBackGrIndex =
                beforeBackGroundImage != null ?
                beforeBackGroundImage.transform.GetSiblingIndex() : -1;
            Debug.Log("sibling index = " + beforeBackGrIndex);
            backGroundImage.transform.SetSiblingIndex(beforeBackGrIndex + 1);
            Animator animator = backGroundImage.GetComponent<Animator>();
            animator.SetTrigger("FadeIn");

            yield return null;
            AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
            float duration = animState.length;
            yield return new WaitForSeconds(duration);
            Destroy(beforeBackGroundImage);

            TextMeshProUGUI speakerTMPro = controller.SpeakerText.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dialogueTMPro = controller.DialogueText.GetComponent<TextMeshProUGUI>();
            string speaker = unit.speaker;
            string sentence = unit.sentence;

            speakerTMPro.text = speaker;

            for (int i = 1; i <= sentence.Length; i++)
            {
                string slicedSentence = sentence.Substring(0, i);
                string lastchar = sentence.Substring(i - 1, 1);

                dialogueTMPro.text = slicedSentence;

                if (lastchar == "\n" || lastchar == "," || lastchar == ".")
                    yield return new WaitForSeconds(textSpeed * 5);
                else
                    yield return new WaitForSeconds(textSpeed);


                if (GameManager.instance.SubmitDowned)
                {
                    dialogueTMPro.text = sentence;
                    break;
                }
            }
            IsPrinting = false;

        }

        public IEnumerator GeneralDialogueCor(DialogueUnit unit)
        {
            IsPrinting = true;
            TextMeshProUGUI speakerTMPro = controller.SpeakerText.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dialogueTMPro = controller.DialogueText.GetComponent<TextMeshProUGUI>();
            string speaker = unit.speaker;
            string sentence = unit.sentence;

            speakerTMPro.text = speaker;

            for (int i = 1; i <= sentence.Length; i++)
            {
                string slicedSentence = sentence.Substring(0, i);
                string lastchar = sentence.Substring(i - 1, 1);

                dialogueTMPro.text = slicedSentence;

                if (lastchar == "\n" || lastchar == "," || lastchar == ".")
                    yield return new WaitForSeconds(textSpeed * 5);
                else
                    yield return new WaitForSeconds(textSpeed);

                
                if (GameManager.instance.SubmitDowned)
                {
                    dialogueTMPro.text = sentence;
                    break;
                }
            }
            IsPrinting = false;
        }

        public IEnumerator SetBackGround()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("coroutine while");
            }
        }

        public IEnumerator FadeInAndOut<T>(T element)
            where T: Graphic
        {
            float duration = 1f;
            while (true)
            {
                float deltaTimeDivDur = Time.deltaTime / duration;

                //element의 투명도가 0.5미만이라면
                if (element.color.a < 1.0f)
                {
                    //element의 투명도를 1까지 높이고
                    while (element.color.a <= 1f)
                    {
                        element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                        yield return new WaitForSeconds(deltaTimeDivDur);
                    }
                }
                //element의 투명도가 1이상이라면
                else if (element.color.a >= 1.0f)
                {
                    //element의 투명도를 0.5까지 낮춘다
                    while (element.color.a >= 0.5f)
                    {
                        element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a - deltaTimeDivDur);
                        yield return new WaitForSeconds(deltaTimeDivDur);
                    }
                }

                yield return new WaitForSeconds(deltaTimeDivDur);
            }
        }

    }
}
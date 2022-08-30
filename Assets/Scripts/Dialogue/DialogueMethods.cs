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
            //���� ������� �ƴϸ� �����̽��� �������� ��ȭ����(DialogueUnit)�� �׼� ����
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
            string sentence = "�������� ����Ǵ� �޼ҵ� ���⼭ ���� ��ȭ�� �ε��ϵ� Ž��é�ͷ� �Ѿ�� �ϵ� �� �� �ִ�.";

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

                //element�� ������ 0.5�̸��̶��
                if (element.color.a < 1.0f)
                {
                    //element�� ������ 1���� ���̰�
                    while (element.color.a <= 1f)
                    {
                        element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                        yield return new WaitForSeconds(deltaTimeDivDur);
                    }
                }
                //element�� ������ 1�̻��̶��
                else if (element.color.a >= 1.0f)
                {
                    //element�� ������ 0.5���� �����
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
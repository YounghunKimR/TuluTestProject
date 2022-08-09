using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



namespace Intro
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public GameObject bgrndImageObj;
        public GameObject titleTextObj;
        public GameObject pakTextObj;
        public GameObject TeamNameTextObj;
        public float duration = 2.0f;

        private bool readyToGo = false;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            StartCoroutine(startIntro());
        }

        // Update is called once per frame
        void Update()
        {
            if (!readyToGo)
            {
                return;
            }

            if (Input.anyKeyDown)
            {
                SceneChanger.instance.changeScene("Menu");
            }
        }


        IEnumerator startIntro()
        {
            Debug.Log("UI Enabled start");
            //ui�� ��� �ε��Ǿ��ٸ� ����
            if (bgrndImageObj.activeInHierarchy && titleTextObj.activeInHierarchy && pakTextObj.activeInHierarchy)
            {
                //title�� pressanykey�� �����ϰԸ���
                TextMeshProUGUI teamNameText = TeamNameTextObj.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI titleText = titleTextObj.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI pressAnyKeyText = pakTextObj.GetComponent<TextMeshProUGUI>();
                Image backgroundImage = bgrndImageObj.GetComponent<Image>();
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                pressAnyKeyText.color = new Color(pressAnyKeyText.color.r, pressAnyKeyText.color.g, pressAnyKeyText.color.b, 0);
                backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
                teamNameText.color = new Color(teamNameText.color.r, teamNameText.color.g, teamNameText.color.b, 0);

                //Ÿ��Ʋ�ؽ�Ʈ�� ���̵��ν�Ŵ

                yield return StartCoroutine(fadeInAndOutUI(teamNameText));
                StartCoroutine(fadeInUI(titleText));
                StartCoroutine(fadeInUI(backgroundImage));
                //duration �� �ڿ� pressanykey�� ���̵��ν�Ŵ
                yield return StartCoroutine(fadeInUI(pressAnyKeyText));

                //�̶����� �ƹ�Ű�� ������ ���� ������ ����
                readyToGo = true;

                //pressanykey�� �����Ÿ�����
                StartCoroutine(lastingFadeInAndOutUI(pressAnyKeyText));
            }
            Debug.Log("UI Enabled end");
            StopCoroutine(startIntro());
            yield return null;
        }

        IEnumerator fadeInAndOutUI<T>(T element)
        where T : Graphic
        {
            Debug.Log("fadeinandouttext start");

            float deltaTimeDivDur;
            float halfDuration = duration / 2f;

            //element�� ������ 1���� ���̰�
            while (element.color.a <= 1f)
            {
                deltaTimeDivDur = Time.deltaTime / halfDuration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }


            //element�� ������ 0���� �����
            while (element.color.a > 0f)
            {
                deltaTimeDivDur = Time.deltaTime / halfDuration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a - deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }

        }

        IEnumerator fadeInUI<T>(T element)
            where T : Graphic
        {
            Debug.Log("fadein start");

            float deltaTimeDivDur;
            while (element.color.a <= 1f)
            {
                deltaTimeDivDur = Time.deltaTime / duration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }

            Debug.Log("fadein end");
        }

        IEnumerator lastingFadeInAndOutUI<T>(T element)
            where T : Graphic
        {
            Debug.Log("lastingfadeinandouttext start");

            while (true)
            {

                float deltaTimeDivDur = Time.deltaTime / duration;

                //element�� ������ 0.5�̸��̶��
                if (element.color.a < 0.5f)
                {
                    //element�� ������ 1���� ���̰�
                    while (element.color.a <= 1f)
                    {
                        element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                        yield return new WaitForSeconds(deltaTimeDivDur);
                    }
                }
                //element�� ������ 1�ʰ����
                else if (element.color.a > 1.0f)
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

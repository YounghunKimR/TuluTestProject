using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


namespace Intro
{
    public class IntroController : MonoBehaviour
    {   
        public static IntroController instance;

        public GameObject bgrndImageObj;
        public GameObject titleTextObj;
        public GameObject pakTextObj;
        public GameObject TeamNameTextObj;
        public float duration = 2.0f;

        private bool readyToGo = true;
        private bool isChangingScene = false;
        

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
            StartCoroutine(StartIntro());
        }

        // Update is called once per frame
        void Update()
        {
            if (!readyToGo)
            {
                return;
            }

            if (Input.anyKeyDown && !isChangingScene)
            {
                SceneChanger.instance.changeScene("Menu", "BloodFilled");
                isChangingScene = true;
            }
        }


        IEnumerator StartIntro()
        {
            //ui가 모두 로딩되었다면 시작
            if (bgrndImageObj.activeInHierarchy && titleTextObj.activeInHierarchy && pakTextObj.activeInHierarchy)
            {
                //title과 pressanykey를 투명하게만듬
                TextMeshProUGUI teamNameText = TeamNameTextObj.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI titleText = titleTextObj.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI pressAnyKeyText = pakTextObj.GetComponent<TextMeshProUGUI>();
                Image backgroundImage = bgrndImageObj.GetComponent<Image>();
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                pressAnyKeyText.color = new Color(pressAnyKeyText.color.r, pressAnyKeyText.color.g, pressAnyKeyText.color.b, 0);
                backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
                teamNameText.color = new Color(teamNameText.color.r, teamNameText.color.g, teamNameText.color.b, 0);

                //타이틀텍스트를 페이드인시킴

                yield return StartCoroutine(FadeinAndOutUI(teamNameText));
                StartCoroutine(FadeInUI(titleText));
                StartCoroutine(FadeInUI(backgroundImage));
                //duration 초 뒤에 pressanykey도 페이드인시킴
                yield return StartCoroutine(FadeInUI(pressAnyKeyText));

                //이때부터 아무키를 누르면 다음 씬으로 진행
                readyToGo = true;

                //pressanykey를 깜빡거리게함
                StartCoroutine(LastingFadeinAndOutUI(pressAnyKeyText));
            }
            StopCoroutine(StartIntro());
            yield return null;
        }

        IEnumerator FadeinAndOutUI<T>(T element)
        where T : Graphic
        {

            float deltaTimeDivDur;
            float halfDuration = duration / 2f;

            //element의 투명도를 1까지 높이고
            while (element.color.a <= 1f)
            {
                deltaTimeDivDur = Time.deltaTime / halfDuration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }


            //element의 투명도를 0까지 낮춘다
            while (element.color.a > 0f)
            {
                deltaTimeDivDur = Time.deltaTime / halfDuration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a - deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }

        }

        IEnumerator FadeInUI<T>(T element)
            where T : Graphic
        {

            float deltaTimeDivDur;
            while (element.color.a <= 1f)
            {
                deltaTimeDivDur = Time.deltaTime / duration;
                element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                yield return new WaitForSeconds(deltaTimeDivDur);
            }

        }

        IEnumerator LastingFadeinAndOutUI<T>(T element)
            where T : Graphic
        {

            while (true)
            {

                float deltaTimeDivDur = Time.deltaTime / duration;

                //element의 투명도가 0.5미만이라면
                if (element.color.a < 0.5f)
                {
                    //element의 투명도를 1까지 높이고
                    while (element.color.a <= 1f)
                    {
                        element.color = new Color(element.color.r, element.color.g, element.color.b, element.color.a + deltaTimeDivDur);
                        yield return new WaitForSeconds(deltaTimeDivDur);
                    }
                }
                //element의 투명도가 1초과라면
                else if (element.color.a > 1.0f)
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

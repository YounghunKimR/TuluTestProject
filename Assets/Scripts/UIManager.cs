using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject backgroundObj;
    private GameObject titleObj;
    private GameObject pressAnyKeyObj;
    private float duration = 3.0f;

    void Start()
    {
        
        backgroundObj = GameObject.Find("Background");
        titleObj = GameObject.Find("Title");
        pressAnyKeyObj = GameObject.Find("PressAnyKey");

        StartCoroutine(checkUIActive());
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator checkUIActive()
    {
        Debug.Log("UI Enabled start");
        //ui가 모두 로딩되었다면 시작
        if (backgroundObj.activeInHierarchy && titleObj.activeInHierarchy && pressAnyKeyObj.activeInHierarchy)
        {
            //title과 pressanykey를 투명하게만듬
            TextMeshProUGUI titleText = titleObj.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pressAnyKeyText = pressAnyKeyObj.GetComponent<TextMeshProUGUI>();
            Image backgroundImage = backgroundObj.GetComponent<Image>();
            titleText.color = new Color(titleText.color.r,titleText.color.g,titleText.color.b,0);
            pressAnyKeyText.color = new Color(pressAnyKeyText.color.r, pressAnyKeyText.color.g, pressAnyKeyText.color.b, 0);
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);

            //타이틀텍스트를 페이드인시킴
            StartCoroutine(fadeInUI(titleText));
            StartCoroutine(fadeInUI(backgroundImage));
            //duration 초 뒤에 pressanykey도 페이드인시킴
            yield return new WaitForSeconds(duration);
            yield return StartCoroutine(fadeInUI(pressAnyKeyText));

            //pressanykey를 깜빡거리게함
            StartCoroutine(fadeInAndOutUI(pressAnyKeyText));
        }
        Debug.Log("UI Enabled end");
        StopCoroutine(checkUIActive());
        yield return null;
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

    IEnumerator fadeInAndOutUI<T>(T element)
        where T: Graphic
    {
        Debug.Log("fadeinandouttext start");

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

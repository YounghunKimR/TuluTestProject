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
        //ui�� ��� �ε��Ǿ��ٸ� ����
        if (backgroundObj.activeInHierarchy && titleObj.activeInHierarchy && pressAnyKeyObj.activeInHierarchy)
        {
            //title�� pressanykey�� �����ϰԸ���
            TextMeshProUGUI titleText = titleObj.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pressAnyKeyText = pressAnyKeyObj.GetComponent<TextMeshProUGUI>();
            Image backgroundImage = backgroundObj.GetComponent<Image>();
            titleText.color = new Color(titleText.color.r,titleText.color.g,titleText.color.b,0);
            pressAnyKeyText.color = new Color(pressAnyKeyText.color.r, pressAnyKeyText.color.g, pressAnyKeyText.color.b, 0);
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);

            //Ÿ��Ʋ�ؽ�Ʈ�� ���̵��ν�Ŵ
            StartCoroutine(fadeInUI(titleText));
            StartCoroutine(fadeInUI(backgroundImage));
            //duration �� �ڿ� pressanykey�� ���̵��ν�Ŵ
            yield return new WaitForSeconds(duration);
            yield return StartCoroutine(fadeInUI(pressAnyKeyText));

            //pressanykey�� �����Ÿ�����
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

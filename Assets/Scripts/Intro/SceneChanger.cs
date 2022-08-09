using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Intro
{
    public class SceneChanger : MonoBehaviour
    {
        public float duration = 3f;
        public static SceneChanger instance;
        private GameObject sceneBlinder;
        private Image sceneBlinderImage;
        private AudioSource audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                sceneBlinder = GameObject.Find("SceneBlinder");
                sceneBlinderImage = sceneBlinder.GetComponent<Image>();
                audioSource = BGMManager.instance.BGMAudioSource;
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void changeScene(string sceneName)
        {
            StartCoroutine(fadeOutScene(sceneName));
        }

        public void loadSceneWithFadeIn()
        {
            StartCoroutine(fadeInScene());
        }

        IEnumerator fadeOutScene(string sceneName)
        {
            float deltaTimeDivDur;
            Image image = sceneBlinderImage;
            while (image.color.a <= 1f)
            {
                deltaTimeDivDur = Time.deltaTime / duration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + deltaTimeDivDur);
                audioSource.volume -= deltaTimeDivDur; 
                yield return new WaitForSeconds(deltaTimeDivDur);
            }


            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        IEnumerator fadeInScene()
        {
            float deltaTimeDivDur;
            Image image = sceneBlinderImage;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            while (image.color.a > 0)
            {
                deltaTimeDivDur = Time.deltaTime / duration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - deltaTimeDivDur);
                audioSource.volume += deltaTimeDivDur;
                yield return new WaitForSeconds(deltaTimeDivDur);
            }
            
            image.gameObject.SetActive(false);
        }
    }
}
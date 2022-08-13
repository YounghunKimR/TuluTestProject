using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Animations;


public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    public GameObject canvas;
    private AudioSource audioSource;

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

    private void Start()
    {
        audioSource = BGMManager.instance.BGMAudioSource;
        canvas = GameObject.Find("Canvas");
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => canvas = GameObject.Find("Canvas");
    }


    public void changeScene(string sceneName, string effectName = "FadeOutScene")
    {
        if (effectName == "FadeOutScene")
        {
            StartCoroutine(fadeOutScene(sceneName));
        }
        else if(effectName == "BloodFilled")
        {
            StartCoroutine(fadeOutBloodFilled(sceneName));
        }
        else
        {
            StartCoroutine(fadeOutScene(sceneName));
        }


    }

    public void loadScene(string effectName = "FadeInScene")
    {
        if (effectName == "FadeInScene")
        {
            StartCoroutine(fadeInScene());
        }
        else if (effectName == "BloodFilled")
        {
            StartCoroutine(fadeInBloodFilled());
        }
        else
        {
            StartCoroutine(fadeInScene());
        }
    }

    IEnumerator fadeOutScene(string sceneName)
    {
        GameObject blinder = Instantiate(GameManager.loadPrefab("FadeOutSceneBlinder"), canvas.transform) as GameObject;
        Animator animator = blinder.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");

        yield return null;

        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
        float duration = animState.length;
        float deltaTimeDivDur;

        while (audioSource.volume > 0)
        {
            deltaTimeDivDur = Time.deltaTime / duration;
            audioSource.volume -= deltaTimeDivDur;
            yield return new WaitForSeconds(deltaTimeDivDur);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    IEnumerator fadeInScene()
    {
        GameObject blinder = Instantiate(GameManager.loadPrefab("FadeInSceneBlinder"), canvas.transform) as GameObject;
        Animator animator = blinder.GetComponent<Animator>();
        animator.SetTrigger("FadeIn");

        yield return null;

        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
        float duration = animState.length;
        float deltaTimeDivDur;

        while (audioSource.volume < 1)
        {
            deltaTimeDivDur = Time.deltaTime / duration;
            audioSource.volume += deltaTimeDivDur;
            yield return new WaitForSeconds(deltaTimeDivDur);
        }

        Destroy(blinder);
    }

    IEnumerator fadeOutBloodFilled(string sceneName)
    {
        GameObject blinder = Instantiate(GameManager.loadPrefab("BloodFilledFilter"), canvas.transform) as GameObject;
        Animator animator = blinder.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");

        yield return null;

        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
        float duration = animState.length;
        float deltaTimeDivDur;

        while (audioSource.volume > 0)
        {
            deltaTimeDivDur = Time.deltaTime / duration;
            audioSource.volume -= deltaTimeDivDur;
            yield return new WaitForSeconds(deltaTimeDivDur);
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    IEnumerator fadeInBloodFilled()
    {
        GameObject blinder = Instantiate(GameManager.loadPrefab("BloodFilledFilter"), canvas.transform) as GameObject;
        Animator animator = blinder.GetComponent<Animator>();
        animator.SetTrigger("FadeIn");

        yield return null;

        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
        float duration = animState.length;
        float deltaTimeDivDur;

        while (audioSource.volume < 1)
        {
            deltaTimeDivDur = Time.deltaTime / duration;
            audioSource.volume += deltaTimeDivDur;
            yield return new WaitForSeconds(deltaTimeDivDur);
        }

        Destroy(blinder);
    }
}

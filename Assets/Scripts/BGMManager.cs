using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioSource BGMAudioSource;
    public static BGMManager instance;
    public AudioClip BGMAudioClip;
    public AudioClip BGMAudioClip2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        float centerWidth = (float)Screen.width / 2f;
        float centerHeight = (float)Screen.height / 2f;
        transform.position = new Vector2(centerWidth, centerHeight);

        SceneManager.sceneLoaded += ChangeBgm;
    }

    void ChangeBgm(Scene scene, LoadSceneMode mode) 
    {
        string sceneName = scene.name;
        if (sceneName == "Intro")
        {
            BGMAudioSource.clip = BGMAudioClip;
            BGMAudioSource.Play();
        }
        else if(sceneName == "Menu")
        {
            BGMAudioSource.clip = BGMAudioClip2;
            BGMAudioSource.Play();
        }
    }

}

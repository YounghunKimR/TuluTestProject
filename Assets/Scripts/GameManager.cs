using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static UnityEngine.Object loadPrefab(string filename)
    {
        UnityEngine.Object loadedObject = Resources.Load("Prefab/" + filename);
        if (loadedObject == null)
        {
            throw new Exception(filename + " not found");
        }
        return loadedObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

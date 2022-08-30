using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool submitDowned = false;
    public bool SubmitDowned
    {
        get 
        {
            if (submitDowned)
            {
                submitDowned = false;
                return true;
            }
            return false;
        } 
    }

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

    private void Update()
    {
        InputLogic();
    }

    private void InputLogic()
    {
        if (Input.GetButtonDown("Submit"))
        {
            submitDowned = true;
        }
    }

    public static GameObject LoadPrefab(string filename)
    {
        GameObject loadedObject = Resources.Load<GameObject>("Prefab/" + filename);
        if (loadedObject == null)
        {
            throw new Exception(filename + " not found");
        }
        return loadedObject;
    }

    public static Sprite LoadImage(string filename)
    {
        Sprite sprite = Resources.Load<Sprite>("Images/" + filename);
        if (sprite == null)
        {
            throw new Exception(filename + " not found");
        }
        return sprite;
    }

    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
}

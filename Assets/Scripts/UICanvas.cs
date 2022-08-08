using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Debug.Log(canvas.activeSelf);
        Debug.Log(canvas.GetComponent<UICanvas>().enabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

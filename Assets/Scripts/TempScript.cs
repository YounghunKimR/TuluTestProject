using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Dialogue;

public class TempScript : MonoBehaviour
{
    public Sprite sprite1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = Instantiate(GameManager.LoadPrefab("Dialogue/BackgroundImage"), GameObject.Find("Canvas").transform);
        obj.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

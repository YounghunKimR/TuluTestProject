using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intro;

public class NewGame : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneChanger.instance.changeScene("Dialogue", "BloodFilled");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intro;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        // Start is called before the first frame update
        void Awake()
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
            SceneChanger.instance.loadScene("BloodFilled");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController instance;

        private GameObject canvas;
        public GameObject Canvas { get => canvas; private set => canvas = value; }

        private GameObject baseBgrnd;
        public GameObject BaseBgrnd { get => baseBgrnd; private set => baseBgrnd = value; }

        private GameObject speakerText;
        public GameObject SpeakerText { get => speakerText; private set => speakerText = value; }
        
        private GameObject dialogueText;
        public GameObject DialogueText { get => dialogueText; private set => dialogueText = value; }

        private GameObject dialogueCircle;
        public GameObject DialogueCircle { get => dialogueCircle; private set => dialogueCircle = value; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                canvas = GameObject.Find("Canvas");
                baseBgrnd = GameObject.Find("BaseBgrnd");
                speakerText = GameObject.Find("SpeakerText");
                DialogueText = GameObject.Find("DialogueText");
                dialogueCircle = GameObject.Find("DialogueCircle");
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            SceneChanger.instance.loadScene("BloodFilled");
        }

        // Update is called once per frame
        void Update()
        {
            DialogueMethods.instance.ExecutePerFrame();   
        }


    }
}
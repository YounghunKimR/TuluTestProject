using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Dialogue
{
    [Serializable]
    public class DialogueUnit
    {
        public string speaker;
        public string sentence;
        //private UnityAction<System.Object> action;
        public UnityAction<DialogueUnit> action;
        public UnityAction<DialogueUnit> Action { get { return action; } set => action = value; }
        private Dictionary<string, System.Object> etcInfo;
        public Dictionary<string, System.Object> EtcInfo 
        { 
            get 
            {
                if (etcInfo == null)
                {
                    etcInfo = new Dictionary<string, object>();
                }
                return etcInfo;
            }
            set => etcInfo = value; 
        }

        public DialogueUnit(string speaker, string sentence)
        {
            this.speaker = speaker;
            this.sentence = sentence;
        }
        public DialogueUnit(string sentence)
        {
            this.sentence = sentence;
        }

        public void CallAction()
        {
            if(action == null)
            {
                action += DialogueMethods.instance.GeneralDialogue;
            }
            action.Invoke(this);
        }
    }

    [Serializable]
    public class DialogueUnits
    {
        public List<DialogueUnit> data;
    }
}
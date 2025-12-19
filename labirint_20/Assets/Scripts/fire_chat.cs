using UnityEngine;
using LLMUnity;
using UnityEngine.UI;
using Palmmedia.ReportGenerator.Core.Common;
using System.Globalization;
using Unity.VisualScripting;
using TMPro;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;

namespace LLMUnitySamples
{

    public class fire_chat : MonoBehaviour
    {
        public LLMCharacter llmCharacter;
        // public InputField playerText;
        public  TMP_Text AIText;
        public GameObject panel;

        private string text_memory = "";

        public static bool isactive = false;
        public static int mem = 1;
        public int num = 1;
        private Coroutine timer;

        void Start()
        {
            // начальный текст ии
            AIText.text = "...";
            string message = "А сейчас просто скинь все части диалога через строку.";
            _ = llmCharacter.Chat(message, SetAIText, AIReplyComplete);
        }

        public void SetAIText(string text)
        {
            text_memory = text;
        }
        void Update()
        {
            if (num == 2)
            {
                Debug.Log(mem +" - NET");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    text_appear.isactive = true;
                    panel.SetActive(false);
                    num = 0;
                    AIText.text = "...";
                    
                }
            }
            if ((num == 1 || num == 0) && (mem < text_memory.Split("\n").Length || mem == 11) && !text_appear.isactive)
            {
                if (Input.GetKeyDown(KeyCode.Z) && AIText.text != "...")
                {
                        Debug.Log(mem+ " timerstop");
                        StopCoroutine(timer);
                        AIText.text = text_memory.Split("\n")[mem-1].Substring(2) + "\n   закрыть [z]";
                        isactive = false;
                        num = 2;
                }

                
            }
            if ((num == 1 || num == 0) && isactive && text_memory.Split("\n").Length > 1)
            {

                
                Debug.Log(text_memory.Split("\n").Length);
                if (mem < text_memory.Split("\n").Length || mem == 11) 
                {
                    AIText.text = "";
                    timer = StartCoroutine(TextAnimation(text_memory.Split("\n")[mem-1].Substring(2) + "\n   закрыть [z]"));
                    isactive = false;
                    
                } else {AIText.text = "...";}
                // 
                
                
            }
            
            Debug.Log(num +" "+isactive);
            
        }

        public void AIReplyComplete()
        {
            Debug.Log(text_memory);
        }
        private IEnumerator TextAnimation(string Text)
        {
            foreach (var letter in Text)
            {
                AIText.text += letter;
                Text = letter.ToString();
                yield return new WaitForSeconds(0.05f);

            }
            num = 2;
        }
    }

    
}

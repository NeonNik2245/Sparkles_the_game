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
        public int id = 0;
        public static int id_now = 0;
        public int num = 1;
        private Coroutine timer;
        private bool isgenerate = false;

        public int max_num_lines = 11;

        void Start()
        {
            // начальный текст ии
            AIText.text = "...";
            string message = "А сейчас просто скинь все части диалога через строку. (а также пиши только строчными буквами)";
            _ = llmCharacter.Chat(message, SetAIText, AIReplyComplete);
        }

        public void SetAIText(string text)
        {
            text_memory = text;
        }
        void Update()
        {
            
            if (id_now == id)
            {
                Debug.Log(id_now+" == "+id);
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
                if ((num == 1 || num == 0) && (mem < text_memory.Split("\n").Length || (mem == max_num_lines && isgenerate))
                && !text_appear.isactive)
                {
                    if (Input.GetKeyDown(KeyCode.Z) && AIText.text != "...")
                    {
                            Debug.Log(mem+ " timerstop");
                            StopCoroutine(timer);
                            AIText.text = text_memory.Split("\n")[mem-1].Substring(3) + "\n   закрыть [z]";
                            isactive = false;
                            num = 2;
                    }

                    
                }
                if ((num == 1 || num == 0) && isactive && text_memory.Split("\n").Length > 1)
                {

                    
                    Debug.Log(text_memory.Split("\n").Length);
                    if (mem < text_memory.Split("\n").Length || (mem == max_num_lines && isgenerate)) 
                    {
                        AIText.text = "";
                        timer = StartCoroutine(TextAnimation(text_memory.Split("\n")[mem-1].Substring(3) + "\n   закрыть [z]"));
                        isactive = false;
                        
                    } else {AIText.text = "...";}
                    // 
                    
                    
                }
                Debug.Log(num +" "+isactive);

            } 
        }

        public void AIReplyComplete()
        {
            Debug.Log(text_memory);
            text_memory = text_memory + "\n_";
            isgenerate = true;
            
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

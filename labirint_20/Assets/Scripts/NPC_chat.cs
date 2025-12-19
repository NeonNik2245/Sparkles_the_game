using UnityEngine;
using LLMUnity;
using UnityEngine.UI;
using Palmmedia.ReportGenerator.Core.Common;
using System.Globalization;
using Unity.VisualScripting;

namespace LLMUnitySamples
{

    public class NPC_chat : MonoBehaviour
    {
        public GameObject but;
        public GameObject thing;
        public GameObject cam;

        public GameObject cam_npc;
        public LLMCharacter llmCharacter;
        public InputField playerText;
        public Text AIText;
        public int num = 0;
        [TextArea(5, 10), Chat] public string hello_prompt = "";
        [TextArea(5, 10), Chat] public string task_prompt = "ТВОЯ ЗАДАЧА:   ПРИСЛАТЬ ОТВЕТ:";
        private string json = "";
        public static bool isReset = false;
        public class ai_json
        {
            public int score = 0;
            public int location = 0;
            public string ai_answer = "";

            public string ai_text = "";
            public string player = "";
            public string character = "";
        }

        void Start()
        {
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerText.Select();
            // начальный текст ии
            playerText.interactable = false;
            AIText.text = "...";
            string message = hello_prompt;
            _ = llmCharacter.Chat(message, SetAIText, AIReplyComplete);
            num = num + 1;
        }

        void onInputFieldSubmit(string message)
        {
            playerText.interactable = false;
            AIText.text = "...";
            string prompt = "";
            if (num == 1)
            {
                prompt = task_prompt;
            _ = llmCharacter.Chat("ИГРОК ГОВОРИТ: " + message + prompt, SetAIText, AIReplyComplete);
            
            }
            num = num + 1;
    
        }

        public void SetAIText(string text)
        {
            if (num < 2)
            {
                AIText.text = text;
            } else {
                json = text;
                string adding = "}";
                try {  
                    ai_json tx = JsonUtility.FromJson<ai_json>((json + "\" " + adding).Replace("\"\"", "\"").Replace("}}", "}"));  
                    AIText.text = AIText.text = tx.ai_answer + "\n" + tx.ai_text;
                } catch  { }
                // } catch  { AIText.text = json;} 
            
            }
        }
        void Update()
        {
            if (num == 3)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    thing.SetActive(false);
                    cam.SetActive(true);
                    but.SetActive(false);
                    cam_npc.SetActive(false);
                    MouseCamera.moving = true;
                    text_appear_npc.isactive = true;
                    text_appear.isactive = true;
                    num = num + 1;
                }
            }
            if (isReset) {
                llmCharacter.CancelRequests();
                isReset = false;
                }
        }

        public void AIReplyComplete()
        {
            
            if (num < 2)
            {
                playerText.interactable = true;
                playerText.Select();
                playerText.text = "";
            } else
            {
                ai_json text = JsonUtility.FromJson<ai_json>(json);
                AIText.text = text.ai_answer + "\n" + text.ai_text;
                if (text.score == 1) add_score.chaos = add_score.chaos + 1;
                else if (text.score == 4) add_score.memory = add_score.memory + 2;
                else if (text.score == 3) add_score.memory = add_score.memory - 2;
                num = num + 1;
            }
            
        }
    }
}

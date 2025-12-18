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

        private int num = 0;
        private string json = "";
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
            string message = @"Ты встречаешь игрока (в виде звезды, девочка),
            и привествуешь, рассказывая немного о себе (про плюшевого слоника не говори)
            - короткая 1-2 фразы, просто по-привествуй,
            и вконце спросить игрока - 'хочешь ли ты что-то узнать от меня?' (не спрашивай кто она!)";
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
                prompt = @" ТВОЯ ЗАДАЧА:
            И отвечаешь на ответ, а после ОБЯЗАТЕЛЬНО говоришь про игрушку хозяйки и ты не помнишь,
            где он находится, но знаешь что недалеко.

            И завершаешь разговор.

            ПРИСЛАТЬ ОТВЕТ:
            Важно - пришли все ответы в JSON формате:
            {
            ai_answer: твой ответ игроку (который игроку говоришь);
            ai_text: заключительный обязательный текст про плюшевого слоника и прощание (который игроку говоришь после ответа игроку);
            player: текст игрока;
            character: 'призрак';
            location: 1;
            (
            а также сделай отдельную переменную score для вопроса игрока со значением (то что ты сказал в ai_text не учитывается):
            1 - вопрос/ответ негативный или если тебя пытаются задеть или провацируют или например то, что может кого-то оскорбить этим даже если чучуть, что-то неприятное (запомни ты очень чувствителен, тебя легко оскорбить);
            2 - вопрос/ответ не по теме или ничего не спрашивает (если не знаешь куда ответ игрока поставить, то можешь в эту категори score: 2);
            3 - вопрос/ответ пытается приврать воспоминания призрака (твои)
            или выдумать что-то своё как будто он знает хозяйку,
            но говорит не правильные факты;
            4 - вопрос/ответ при котором призрак говорит важную информацию о детсве хозяйки или об окружении
            (то есть игрок просит продолжение истории или какую-нибуть подсказку, подробности);
            )
            }
            .";
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
                // ai_json tx = JsonUtility.FromJson<ai_json>((json + "\", " + adding).Replace("\"\"", "\"").Replace("}}", "}"));
                // AIText.text = tx.ai_answer + "\n" + tx.ai_text + "\n score= " + 
                
            
            }
        }
        void Update()
        {
            if (num == 3)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    thing.SetActive(false);
                    // isactivate = true;
                    // cam = GameObject.FindGameObjectWithTag("MainCamera");
                    // cam_npc = GameObject.FindGameObjectWithTag("npc");
                    cam.SetActive(true);
                    but.SetActive(false);
                    text_appear.isactive = false;
                    cam_npc.SetActive(false);
                    MouseCamera.moving = true;
                    text_appear.isactive = true;
                }
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
                
                // input.ActivateInputField();
                // text_appear.thisobj.GetComponent<npc>
                // isactive = false;
                // Debug.Log(EventSystem);
            }
            
        }

        public void CancelRequests()
        {
            llmCharacter.CancelRequests();
            AIReplyComplete();
        }

        public void ExitGame()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
        }

        bool onValidateWarning = true;
        void OnValidate()
        {
            if (onValidateWarning && !llmCharacter.remote && llmCharacter.llm != null && llmCharacter.llm.model == "")
            {
                Debug.LogWarning($"Please select a model in the {llmCharacter.llm.gameObject.name} GameObject!");
                onValidateWarning = false;
            }
        }
    }
}

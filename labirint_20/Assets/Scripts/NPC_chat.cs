using UnityEngine;
using LLMUnity;
using UnityEngine.UI;


namespace LLMUnitySamples
{
    // Задаёт запросы LLM для НПС с однотипным сценарием:
    // приветствие (1) > ответ игрока (2) > ответ LLM с подсказкой локации и прощанием (3)
    public class NPC_chat : MonoBehaviour
    {
        // вся панель диалога
        public GameObject thing;
        // объект главной камеры игрока
        public GameObject cam;
        // камера для статичного просмотра диалога с нпс
        public GameObject cam_npc;
        // для взаимодействие с LLM с заготовленным запросом
        public LLMCharacter llmCharacter;
        // формочка ввода текста для игрока
        public InputField playerText;
        // объект для отображения текста LLM
        public Text AIText;
        // отвечает за событие сценария описанный выше
        public int num = 0;
        // формочки запросов приветствия (1) и генерации ответа по шаблону json (3)
        [TextArea(5, 10), Chat] public string hello_prompt = "";
        [TextArea(5, 10), Chat] public string task_prompt = "ТВОЯ ЗАДАЧА:   ПРИСЛАТЬ ОТВЕТ:";
        // сохранение json ответа (3)
        private string json = "";
        // очистка памяти LLM после каждого перехода локации
        public static bool isReset = false;
        // шаблон json
        public class ai_json
        {
            // показатель начисления очков памяти или хаоса
            public int score = 0;
            // локация нпс
            public int location = 0;
            // ответ (3) на вопрос/ответ игрока (2)
            public string ai_answer = "";
            // подсказка локации и прощание (3)
            public string ai_text = "";
            // запрос игрока (2)
            public string player = "";
            // имя нпс
            public string character = "";
        }

        // заранее генерирует текст
        void Start()
        {
            // фокусируем на формочку ввода и лишаем доступ писать пока LLM генерирует приветствие (1)
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerText.Select();
            playerText.interactable = false;
            // начальный текст ии (пока анализирует) и задаём запрос на приветствие 
            AIText.text = "...";
            // SetAIText - пока генерируется, печатает текст в настоящем времени
            // AIReplyComplete - после полного сгенерированного текста, даёт возможность писать игроку
            _ = llmCharacter.Chat(hello_prompt, SetAIText, AIReplyComplete);
            // переход на событие (1)
            num = num + 1;
        }

        void onInputFieldSubmit(string message)
        {
            // игрок нажал Enter (отправил запрос) - лишаем возможность печатать
            playerText.interactable = false;
            AIText.text = "...";
            if (num == 1)
            {
                // даём ответ игрока и запрос на подсказку и прощание
                _ = llmCharacter.Chat("ИГРОК ГОВОРИТ: " + message + task_prompt, SetAIText, AIReplyComplete);
                // переход на событие (2)
                num = num + 1;
            }
        }

        public void SetAIText(string text)
        {
            // приветствие печатает как обычный текст
            if (num < 2)
            {
                AIText.text = text;
            } else {
                // второго ответ LLM с шаблоном json
                json = text;
                // пока генерирует, добавляем конец структуры JSON: " }
                // чтобы текст отображался и игрок слишком долго не ждал
                try {  
                    ai_json tx = JsonUtility.FromJson<ai_json>((json + "\" }").Replace("\"\"", "\"").Replace("}}", "}"));  
                    AIText.text = AIText.text = tx.ai_answer + "\n" + tx.ai_text;
                } catch {}
            }
        }
        void Update()
        {
            // закрытие диалога
            if (num == 3 && Input.GetKeyDown(KeyCode.Z))
            {
                // закрываем панель диалога (текст взаимодействия)
                thing.SetActive(false);
                // возвращем камеру игроку и управление
                cam.SetActive(true);
                cam_npc.SetActive(false);
                MouseCamera.moving = true;
                // отображаем плашки текста взаимодействия с объектами
                text_appear.isactive = true;
                text_appear_npc.isactive = true;
                GoToScene.isactive = true;
                GoToEndScene.isactive = true;
                playerText.text = "";
                num = num + 1;
            }
            // остановка/очистка LLM
            if (isReset)
            {
                llmCharacter.CancelRequests();
                llmCharacter.ClearChat();
                isReset = false;
            }
        }

        public void AIReplyComplete()
        {
            if (num < 2)
            {
                // после сгенерированного приветствия даём возможность игроку писать в форму
                playerText.interactable = true;
                playerText.Select();
                playerText.text = "";
            } 
            else {
                // после генерации json, начисляем очки
                try {  
                    ai_json text = JsonUtility.FromJson<ai_json>(json);
                    AIText.text = text.ai_answer + "\n" + text.ai_text;
                // score 1 - негативное, 2 - не по теме, 3 - враньё, 4 - интерес/просьба подсказки
                if (text.score == 1) add_score.chaos = add_score.chaos + 1;
                else if (text.score == 4) add_score.memory = add_score.memory + 2;
                else if (text.score == 3) add_score.memory = add_score.memory - 2;
                } catch {}
                playerText.text = "Пора идти (закрыть [Z])";
                // переход на событие (3) - даём выйти из диалога
                num = num + 1;
            }
        }
    }
}

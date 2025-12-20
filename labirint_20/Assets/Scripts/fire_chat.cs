using UnityEngine;
using LLMUnity;
using TMPro;
using System.Collections;

namespace LLMUnitySamples
{
    // Задаёт запросы выводя фразы-ответы по строчно для отображение описание нескольким связным объектам
    public class fire_chat : MonoBehaviour
    {
        // для взаимодействие с LLM с заготовленным запросом
        public LLMCharacter llmCharacter;
        // объект отображение строки текста сгенерированной LLM 
        public TMP_Text AIText;
        // верхняя панель для отображение текста
        public GameObject panel;
        // id каждого запроса для отдельных объектов (чтобы не накладывались ответы)
        public int id = 0;
        // num = 1 - текст генерируется, num = 0 - текст сгенерирован, num = 2 - текст напечатан
        public int num = 1;
        // максимальное количество строк ответа LLM (в промте llmCharacter важно указать кол-во строк тоже)
        public int max_num_lines = 11;
        // сгенерированный список ответов по строчно
        private string text_memory = "";
        // активация отображение строки ответа LLM (для запуска одноразовой анимации появления текста)
        public static bool isactive = false;
        // номер строки (индекс mem - 1)
        public static int mem = 1;
        // действующий запрос LLM (чтобы показывался только один конкретный)
        public static int id_now = 0;
        // таймер для анимации появления текста
        private Coroutine timer;
        // ответы LLM полностью сгенерированы?
        private bool isgenerate = false;

        // заранее генерирует текст
        void Start()
        {
            // начальный текст ии (до сгенерированной его строки)
            AIText.text = "...";
            // запрос на генерацию ответа запроса заданное в LlmCharacter
            string message = "А сейчас просто скинь все части диалога через строку. (а также пиши только строчными буквами)";
            // SetAIText - пока генерируется, сохраняет его текст (для отображения текста на панеле пока генерируется)
            // AIReplyComplete - после полного сгенерированного текста, даёт возможность закрыть верхнюю панель
            _ = llmCharacter.Chat(message, SetAIText, AIReplyComplete);
        }
        public void SetAIText(string text)
        {
            text_memory = text;
        }
        void Update()
        {
            // сверка id запросов
            if (id_now == id)
            {
                // закрывает верхнюю плашку с текстом (выход) кнопкой [Z]
                if (num == 2 && Input.GetKeyDown(KeyCode.Z))
                {
                    // возвращаем отображение всех плашек с текстом взаимодействия объектов
                    text_appear.isactive = true;
                    GoToScene.isactive = true;
                    GoToEndScene.isactive = true;
                    // скрываем верхнюю плашку
                    panel.SetActive(false);
                    // заранее возвращаем изначальные показатели
                    AIText.text = "...";
                    num = 0;
                    
                }
                // перемотка анимации текста через [Z]
                if ((num == 1 || num == 0)
                    && (mem < text_memory.Split("\n").Length || (mem == max_num_lines && isgenerate))
                    && !text_appear.isactive)
                {
                    if (Input.GetKeyDown(KeyCode.Z) && AIText.text != "...")
                    {
                        // останавливаем таймер анимации
                        StopCoroutine(timer);
                        // выводим полный текст
                        AIText.text = text_memory.Split("\n")[mem-1].Substring(3) + "\n   закрыть [z]";
                        isactive = false;
                        // даём возможность закрыть верхнюю плашку
                        num = 2;
                    }
                }
                // активируем анимацию появления ответа LLM (если строка mem уже полностью сгенерирована)
                if ((num == 1 || num == 0) && isactive && text_memory.Split("\n").Length > 1)
                {
                    if (mem < text_memory.Split("\n").Length || (mem == max_num_lines && isgenerate)) 
                    {
                        // очищаем текст
                        AIText.text = "";
                        // запускаем таймер с текстом
                        timer = StartCoroutine(TextAnimation(text_memory.Split("\n")[mem-1].Substring(3) + "\n   закрыть [z]"));
                        // убираем возможность снова начать анимацию текста
                        isactive = false; 
                    } else AIText.text = "...";
                }
            } 
        }
        public void AIReplyComplete()
        {
            // делаем запасную строку
            text_memory = text_memory + "\n_";
            // текст ответов полностью сгенерирован
            isgenerate = true;
        }

        // анимация появление текста в верхней панеле
        private IEnumerator TextAnimation(string Text)
        {
            // появление буквы каждую 0.05с. 
            foreach (var letter in Text)
            {
                AIText.text += letter;
                Text = letter.ToString();
                yield return new WaitForSeconds(0.05f);
            }
            // даём возможность закрыть верхнюю плашку
            num = 2;
        }
    }
}

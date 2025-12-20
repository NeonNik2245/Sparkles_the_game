using UnityEngine;
using LLMUnitySamples;

// Отображение плашки взаимодействия с объектом
public class text_appear : MonoBehaviour
{
    // плашка с текстом
    public GameObject thisobj;
    // расстояние активации
    public float offset = 15f;
    // начисление очков памяти
    public int score = 0;
    // объект будет поворачиваться к игроку?
    public bool isrotate = true;
    // скорость поворота на камеру игрока (для светлячков)
    public float rotationSpeed = 100f; 
    // взаимодействие с верхней плашком (описание через LLM)
    public bool ispanel = false;
    // верхняя плашка (описание через LLM)
    public GameObject panel;
    // индекс строки из текста LLM (задаётся промт на построчный вывод фраз)
    public int mem = 1;
    // для контроля отображения текста LLM генераций, если задаётся на плашку несколько запросов
    public int id_task = 0;
    // пропадает объект после взаимодействия? (для игрушки слона)
    public bool ishide = false;
    // объект, который скрывается
    public GameObject objhide;
    // координаты камеры игрока
    private Transform cam;
    // координаты объекта над которым будет плашка с текстом thisobj
    private Transform stuff;
    // расстояние камеры до объекта
    private float distanse;
    // одноразовое взаимодействие (контроль через bool)
    private bool turn = true;
    // возвращение взаимодействия
    public static bool resetTurn = false;
    // для скрытие всех плашек с текстом при взаимодействии с одним (чтобы не накладывались текста)
    public static bool isactive = true;

    void Start()
    {
        // заранее скрываем плашки с текстом
        thisobj.SetActive(false);
    }

    void Update()
    {
        // возвращение взаимодействия с объектом
        if (resetTurn) {
            turn = true;
            resetTurn = false;
        }
        // если активен и ещё имеет одноразовую попытку взаимодействия
        if (isactive && turn)
        {
            // появление плашки с текстом
            Appear();
            // при нажатии на кнопки взаимодействия [Z] и если рядом - используется одноразовое взаимодействие
            // и активируется описание объекта если оно задано ispanel через верхную плашку
            if (Input.GetKeyDown(KeyCode.Z) && distanse < offset && turn)
            {
                // начисление очков памяти
                add_score.memory = add_score.memory + score;
                // активация верхней плашки описания
                if (ispanel) {
                    panel.SetActive(true);
                    // выбор назначенного id запроса LLM
                    fire_chat.id_now = id_task;
                    // задание индекса строки отображения
                    fire_chat.mem = mem;
                    // активируем отображение
                    fire_chat.isactive = true;
                    // скрываем все плашки текстов взаимодействия (чтобы не накладывались текста)
                    isactive = false;
                    GoToScene.isactive = false;
                    GoToEndScene.isactive = false;
                }
                // скрытие объекта взаимодействиея
                if (ishide) objhide.SetActive(false);
                // скрытие данной плашки тектса
                thisobj.SetActive(false);
                // лишение одноразовой попытки для данного объекта
                turn = false;
            } 
            // поворот объекта на игрока
            if (isrotate) Rotation();
        }
    }

    // поворот светлячков (объект сферы) для поворота плашки
    void Rotation() 
    {
        // смотрит на камеру игрока
        stuff.transform.LookAt(cam.transform);
    }   

    // появление плашки с текстом
    void Appear()
    {
        // получение координат главной камеры (игрока) и предмета
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        stuff = GetComponent<Transform>();
        // расстояние камеры и предмета
        distanse = Vector3.Distance(cam.position, stuff.position); 
        // рядом - активация/отображение плашки с текстом
        if (distanse < offset) thisobj.SetActive(true);
        else thisobj.SetActive(false);
    }
}

using UnityEngine;

// Отдельное отображение плашки взаимодействия с нпс
public class text_appear_npc : MonoBehaviour
{
    // плашка с текстом
    public GameObject thisobj;
    // расстояние активации
    public float offset = 15f;
    // координаты камеры игрока
    private Transform cam;
    // координаты нпс над которым будет плашка с текстом thisobj
    private Transform stuff;
    // расстояние камеры до нпс
    private float distanse;
    // одноразовое взаимодействие (контроль через bool)
    private bool turn = true;
    // возвращение взаимодействия
    public static bool resetTurn = false;

    // для скрытие всех плашек с текстом при взаимодействии с одним
    public static bool isactive = true;

    void Start()
    {
        // заранее скрываем плашки с текстом
        thisobj.SetActive(false);
    }

    void Update()
    {
        // возвращение взаимодействия с объектом
        if (resetTurn)
        {   
            turn = true;
            resetTurn = false;
        }
        // имеет одноразовую попытку взаимодействия
        if (isactive && turn)
        {
            // появление плашки с текстом
            Appear();
            // при нажатии на кнопки взаимодействия [Z] и рядом - лишение одноразовой попытки для данного объекта
            if (Input.GetKeyDown(KeyCode.Z) && distanse < offset && turn) turn = false;
        }
    }
    
    // появление плашки с текстом
    void Appear()
    {
        // получение координат главной камеры (игрока) и нпс
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        stuff = GetComponent<Transform>();
        // расстояние камеры и нпс
        distanse = Vector3.Distance(cam.position, stuff.position);
        // рядом - активация/отображение плашки с текстом
        if (distanse < offset) thisobj.SetActive(true);
        else thisobj.SetActive(false);
    }
}

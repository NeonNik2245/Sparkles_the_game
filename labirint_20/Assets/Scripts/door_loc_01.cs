using LLMUnitySamples;
using UnityEngine; 
using UnityEngine.SceneManagement; 

// Перемещение по заданой локацией
public class GoToScene : MonoBehaviour 
{
    // название локации для перехода
    public string sceneName; 
    // расстояние активации
    public float offset = 10f;
    // координаты камеры игрока
    private Transform cam;
    // координаты предмета (двери) через которую перемещается игрок
    private Transform stuff;
    // расстояние камеры до предмета перемещение в локацию
    private float distanse;
    // выход из игры
    public bool isExit = false;
    // для скрытие плашки с текстом
    public static bool isactive = true;
    void Update()
    {
        if (isactive)
        {
            // получение координат главной камеры (игрока) и предмета
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            stuff = GetComponent<Transform>();
            // расстояние камеры и предмета перемещения
            distanse = Vector3.Distance(cam.position, stuff.position); 
            // если кнопка [Z] нажата и расстоние меньше заданного offset
            if (Input.GetKeyDown(KeyCode.Z) && distanse < offset)
            {
                // выход из игры если рядом с предметом в близи
                if (isExit) Application.Quit();
                else {
                    // перемещение на заданную локацию
                    SceneManager.LoadScene(sceneName); 
                    // возобновление параметров LLM и отображение плашек взаимодействия с предметами/НПС
                    NPC_chat.isReset = true;
                    text_appear_npc.resetTurn = true;
                    text_appear.resetTurn = true;
                }
            } 
        } 
    }
} 

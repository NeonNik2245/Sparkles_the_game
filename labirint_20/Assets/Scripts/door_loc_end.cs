using UnityEngine; 
using UnityEngine.SceneManagement; 

// Рассчёт перехода на концовки игры - локации 4
public class GoToEndScene : MonoBehaviour 
{ 
    // расстояние активации
    public float offset = 10f;
    // координаты камеры игрока
    private Transform cam;
    // координаты предмета (двери) через которую перемещается игрок
    private Transform stuff;
    // расстояние камеры до предмета перемещение в локацию
    private float distanse;
    // для скрытие плашки с текстом
    public static bool isactive = true;
    void Update() 
    { 
        if (isactive)
        {
            // получение очков за всю игру
            int memory = add_score.memory;
            int chaos = add_score.chaos;
            // название локации концовки
            string sceneNameEnd; 
            // получение координат главной камеры (игрока) и предмета
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            stuff = GetComponent<Transform>();
            // расстояние камеры и предмета перемещения
            distanse = Vector3.Distance(cam.position, stuff.position); 
            // если кнопка [Z] нажата и расстоние меньше заданного offset
            if (Input.GetKeyDown(KeyCode.Z) && distanse < offset)
            {
                // приоритет - очки хаоса
                if (chaos > 1) {sceneNameEnd = "chaos end";}
                else {
                    // диапазоны концовок по очкам памяти
                    if (memory > 24 - 7) sceneNameEnd = "good end";
                    else if (memory > 17 - 7) sceneNameEnd = "normal end";
                    else sceneNameEnd = "bad end";
                }
                // переход на локацию вычисленной локации
                SceneManager.LoadScene(sceneNameEnd);   
            }
        }
    } 
} 
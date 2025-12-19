using UnityEngine; 
using UnityEngine.SceneManagement; 

public class GoToEndScene : MonoBehaviour 
{ 
    public int set_memory = add_score.memory;
    public int set_chaos = add_score.chaos;
    public float offset = 10f;

    private Transform cam;
    private Transform stuff;
    private float distanse;
    
    void Update() 
    { 
        int memory = set_memory;
        int chaos = set_chaos;

        string sceneNameEnd; 

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        stuff = GetComponent<Transform>();
        distanse = Vector3.Distance(cam.position, stuff.position); 
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            if (distanse < offset)
            {
              if (chaos > 1) sceneNameEnd = "chaos end";
                else {
                    if (memory > 24 - 7) sceneNameEnd = "good end";
                    else if (memory > 17 - 7) sceneNameEnd = "normal end";
                    else sceneNameEnd = "bad end";
                }
                SceneManager.LoadScene(sceneNameEnd);   
                
            } else Debug.Log("___далеко кликаешь бро " + distanse);
        }
     
    } 
} 
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class GoToScene : MonoBehaviour 
{ 
    public string sceneName; 
    public float offset = 10f;
    // public Camera characterCamera;
    private Transform cam;
    private Transform stuff;
    private float distanse;

    void Update()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        stuff = GetComponent<Transform>();
        distanse = Vector3.Distance(cam.position, stuff.position); 
        // Debug.Log(distanse);
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            if (distanse < offset) SceneManager.LoadScene(sceneName); 
            else Debug.Log("далеко кликаешь бро " + distanse);
        }

    }

    // void OnMouseDown() 
    // { 
        
        
        
    //     // Shot ray to find object to pick
        
        
    //     distanse = Vector3.Distance(cam.position, stuff.position); 
    //     Debug.Log(distanse);
    //     // if (distanse < offset) {
    //     //     // SceneManager.LoadScene(sceneName);

    //     // }
        
    // } 
} 

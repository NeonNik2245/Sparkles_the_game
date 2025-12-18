using UnityEngine; 
using UnityEngine.SceneManagement; 

public class GoToEndScene : MonoBehaviour 
{ 
    public int set_memory = 0;
    public int set_chaos = 0;
    public float offset = 10f;
    public Camera characterCamera;
    
    void OnMouseDown() 
    { 
        int memory = set_memory;
        int chaos = set_chaos;

        string sceneNameEnd; 

        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        // Shot ray to find object to pick
        if (Physics.Raycast(ray, out hit, offset))
        {
            if (chaos > 1) sceneNameEnd = "chaos end";
            else {
                if (memory > 24) sceneNameEnd = "good end";
                else if (memory > 17) sceneNameEnd = "normal end";
                else sceneNameEnd = "bad end";
            }
            SceneManager.LoadScene(sceneNameEnd); 
        } else Debug.Log("далеко кликаешь бро");
     
    } 
} 
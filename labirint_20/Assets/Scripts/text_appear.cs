using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_appear : MonoBehaviour
{
    public GameObject thisobj;
    public float offset = 15f;
    public int score = 0;
    private Transform cam;
    private Transform stuff;
    private float distanse;
    private bool turn = true;
    public float rotationSpeed = 100f; 
    public bool isrotate = true;
    public static bool isactive = true;

    void Start()
    {
        thisobj.SetActive(false);
    }

    void Update()
    {
        if (isactive)
        {
            Appear();
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            if (distanse < offset) 
            {
                if (turn == true) {
                    add_score.memory = add_score.memory + score; 
                    turn = false;
                }
                // thisobj.SetActive(false);
                
            }
            else Debug.Log("далеко кликаешь бро " + distanse);
        }
        if (isrotate) Rotation();
        }
        
    }

    void Rotation() 
    {
        stuff.transform.LookAt(cam.transform);
    }   

    void Appear()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        stuff = GetComponent<Transform>();
        distanse = Vector3.Distance(cam.position, stuff.position); 
        if (distanse < offset) thisobj.SetActive(true);
        else thisobj.SetActive(false);
    }
}

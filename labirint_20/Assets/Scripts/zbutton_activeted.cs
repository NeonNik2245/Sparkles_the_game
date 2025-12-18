using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class zbutton_activeted : MonoBehaviour
{

    // public int display;
    public GameObject thing;
    private bool isactivate = false;
    private GameObject cam;

    public GameObject cam_npc;
    public GameObject but;
    public InputField input;


    void Update()
    {
        if (but.activeSelf)
        {
            if (!isactivate)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // Display.displays[display].Activate();
                thing.SetActive(true);
                isactivate = true;
                cam = GameObject.FindGameObjectWithTag("MainCamera");
                // cam_npc = GameObject.FindGameObjectWithTag("npc");
                cam.SetActive(false);
                but.SetActive(false);
                text_appear.isactive = false;
                cam_npc.SetActive(true);
                MouseCamera.moving = false;
                input.ActivateInputField();
                // text_appear.thisobj.GetComponent<npc>
                // isactive = false;
                // Debug.Log(EventSystem);
            }
        }
        } else input.ActivateInputField();
        
        
    }
}

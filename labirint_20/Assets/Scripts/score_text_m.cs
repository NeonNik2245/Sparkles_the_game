using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score_text_m : MonoBehaviour
{
    public TMP_Text canvasText;

    void Update()
    {
        if (add_score.memory > 9) canvasText.text = add_score.memory + " :П";
        else canvasText.text = "0" + add_score.memory + ": П";
        
        // Debug.Log(canvasText.text);
    }
}

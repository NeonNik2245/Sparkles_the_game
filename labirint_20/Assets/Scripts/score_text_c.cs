using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score_text_c : MonoBehaviour
{
    public TMP_Text canvasText;

    void Update()
    {
        if (add_score.chaos > 9) canvasText.text = add_score.chaos + " :Х";
        else canvasText.text = "0" + add_score.chaos + ": Х";
        
        // Debug.Log(canvasText.text);
    }
}

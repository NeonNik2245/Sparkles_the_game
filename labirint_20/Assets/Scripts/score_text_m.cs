using UnityEngine;
using TMPro;

// Отображение показателя очков памяти
public class score_text_m : MonoBehaviour
{
    // объект отображаение текста показателя памяти ( memory )
    public TMP_Text canvasText;

    void Update()
    {
        // отображение показателя в игре с добавление 0 в начале если [-9; 9]
        if (add_score.memory > 9 || add_score.memory < -9) canvasText.text = add_score.memory + " :П";
        else if (add_score.memory <= 9 && add_score.memory >= 0) canvasText.text = "0" + add_score.memory + " :П";
        else canvasText.text = "-0" + -add_score.memory + " :П";
    }
}

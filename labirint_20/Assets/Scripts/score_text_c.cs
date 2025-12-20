using UnityEngine;
using TMPro;

// Отображение показателя очков хаоса
public class score_text_c : MonoBehaviour
{
    // объект отображаение текста показателя хаоса ( chaos )
    public TMP_Text canvasText;

    void Update()
    {
        // отображение показателя в игре с добавление 0 в начале если [-9; 9]
        if (add_score.chaos > 9 || add_score.chaos < -9) canvasText.text = add_score.chaos + " :Х";
        else if (add_score.chaos <= 9 && add_score.chaos >= 0) canvasText.text = "0" + add_score.chaos + " :Х";
        else canvasText.text = "-0" + -add_score.chaos + " :Х";
    }
}

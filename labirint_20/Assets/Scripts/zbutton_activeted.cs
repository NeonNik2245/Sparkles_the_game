using UnityEngine;
using UnityEngine.UI;

// Отображение диалоговой панели с нпс через кнопку [Z]
public class zbutton_activeted : MonoBehaviour
{
    // вся панель диалога
    public GameObject thing;
    // камера для статичного просмотра диалога с нпс
    public GameObject cam_npc;
    // плашка с текстом (для его скрытия и действий скрипта)
    public GameObject but;
    // формочка ввода текста для игрока
    public InputField input;
    // взаимодействие произошло?
    private bool isactivate = false;
    private GameObject cam;

    void Update()
    {
        // если плашка с текстом отображется
        if (but.activeSelf)
        {
            // кнопка нажата и диалог ещё не происходил
            if (!isactivate && Input.GetKeyDown(KeyCode.Z))
            {
                // отображаем панель диалога
                thing.SetActive(true);
                // диалог происходит(л)
                isactivate = true;
                // объект главной камеры игрока
                cam = GameObject.FindGameObjectWithTag("MainCamera");
                // отключаем главную камеру игрока
                cam.SetActive(false);
                // включаем камеру направленную на нпс и диалог
                cam_npc.SetActive(true);
                // лишаем возможность двигаться игрока
                MouseCamera.moving = false;
                // скрываем плашку с текстом
                but.SetActive(false);
                // скрытие остальных плашек с текстом
                text_appear.isactive = false;
                text_appear_npc.isactive = false;
                GoToScene.isactive = false;
                GoToEndScene.isactive = false;
                input.ActivateInputField(); // фокус на форму ввода
            }
        } else input.ActivateInputField(); // фокус на форму ввода (если игрок выйдет из окна)
    }
}

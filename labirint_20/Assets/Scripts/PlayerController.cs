using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    // чувствительность поворота
    public float sensitivity = 1;
    public Vector3 deltaMove;
    public float speed = 6;
    public CharacterController mover;
    public Transform cameraHolder;
    // Скорость свободного перемещения игрока (по Y)
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    // Гравитация
    private float gravityValue = -9.87f;
    public static bool moving = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (moving)
        {
            Rotation();
            Move();  
        }
    }
    //  --ВРАЩЕНИЕ--
    private void Rotation()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        if (turn.y >= 50) {turn.y = 50;}
        else if (turn.y <= -50) {turn.y = -50;}
        cameraHolder.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
        // --ПЕРЕМЕЩЕНИЕ--
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * 0.7f;
        float verticalMove = Input.GetAxis("Vertical");
        // уменьшение скорости когда нажаты одновременно две кнопки передвижения
        float notwo = 1;
        if (horizontalMove > 0.6f && verticalMove > 0.8f) notwo = 0.8f;
        if (horizontalMove < -0.6f && verticalMove > 0.8f) notwo = 0.8f;
        if (horizontalMove > 0.6f && verticalMove < -0.8f) notwo = 0.8f;
        if (horizontalMove < -0.6f && verticalMove < -0.8f) notwo = 0.8f;
        // Debug.Log(horizontalMove+" _ "+verticalMove);
        // Если персонаж стоит на земле, убрать скорость
        groundedPlayer = mover.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        else playerVelocity.y += gravityValue * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, playerVelocity.y, 0);
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        mover.Move(((Input.GetKey(KeyCode.LeftShift) ? speed + 4 : speed) * Time.deltaTime * move + gravityMove * Time.deltaTime) * notwo);
        Debug.Log((Input.GetKey(KeyCode.LeftShift) ? "бег" : "ходьба"));
    }
}
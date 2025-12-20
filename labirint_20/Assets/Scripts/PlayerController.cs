using UnityEngine;

// Управление игрока - передвижение и поворот камеры по мыши
public class MouseCamera : MonoBehaviour
{
    // координаты мышки
    public Vector2 turn;
    // чувствительность поворота
    public float sensitivity = 1;
    // задержка движения
    public float speed = 6;
    // сфера игрока
    public CharacterController mover;
    // координаты захваченной камеры
    public Transform cameraHolder;
    // Скорость свободного перемещения игрока (по Y)
    private Vector3 playerVelocity;
    // игрок касается пола/земли?
    private bool groundedPlayer;
    // Гравитация
    private float gravityValue = -9.87f;
    // игрок движется?
    public static bool moving = true;
    void Start()
    {
        // убирает отображение курсора во время игры
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // если дано разрешение двигаться - выполняет поворот от мыши/движение
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
        // ограничение поворота по оси Y в диапазон [-50; 50]
        if (turn.y >= 50) {turn.y = 50;}
        else if (turn.y <= -50) {turn.y = -50;}
        // поворот ось Y камеры и сферы игрока ось X 
        cameraHolder.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }
    // --ПЕРЕМЕЩЕНИЕ--
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * 0.7f;
        float verticalMove = Input.GetAxis("Vertical");
        // уменьшение скорости когда нажаты одновременно две кнопки передвижения
        // устроняет диагональное ускорение
        float notwo = 1;
        if (horizontalMove > 0.6f && verticalMove > 0.8f) notwo = 0.8f;
        if (horizontalMove < -0.6f && verticalMove > 0.8f) notwo = 0.8f;
        if (horizontalMove > 0.6f && verticalMove < -0.8f) notwo = 0.8f;
        if (horizontalMove < -0.6f && verticalMove < -0.8f) notwo = 0.8f;
        // если персонаж стоит на земле, убрать скорость
        groundedPlayer = mover.isGrounded;
        // устронение падение сквозь текстур пола/земли, иначе постепенно падает по Time.deltaTime
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        else playerVelocity.y += gravityValue * Time.deltaTime;
        // создание только одной оси Y
        Vector3 gravityMove = new Vector3(0, playerVelocity.y, 0);
        // обычное перемещение / движение
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        // с учётом скорости (если нажат левый Shift), Time.deltaTime и гравитации и учётом диагонального движения
        mover.Move(((Input.GetKey(KeyCode.LeftShift) ? speed + 4 : speed) * Time.deltaTime * move + gravityMove * Time.deltaTime) * notwo);
    }
}
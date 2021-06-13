using UnityEngine;

//Движения игрока, прыжки, прысидания, бег

public class FPS_Input : MonoBehaviour
{
    public CharacterController controller; //ссылка на контроллер
    public Transform groundCheck; //позыция пустого объекта (сфера)
    public LayerMask groundMask; //слой

    public float speed = 5f; //скорость
    public float gravity = -20f; //гравитация
    public float jumpHeigh = 3f; //высота прижка
    public float sphereSize = 0.4f; //размер сферы (радиус)


    Vector3 velosity; //ускорения

    bool isGrounded; //проверка нахадится персонаж на земле или нет

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //создаем невидимую сферу, которая проверяет коснулись ли мы земли
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velosity.y < 0)
        {
            velosity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal"); //движения по горизонтали
        float z = Input.GetAxis("Vertical"); //движения по вертикали

        Vector3 move = transform.right * x + transform.forward * z; //перемещение вправо/влево, вверх/вниз.
        controller.Move(move * speed * Time.deltaTime); //принимаем значения перемещения в контроллер

        //Призимление персонажа
        velosity.y += gravity * Time.deltaTime;
        controller.Move(velosity * Time.deltaTime);

        //Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velosity.y = Mathf.Sqrt(jumpHeigh * -2 * gravity);
        }

        //Прысидания
        if (Input.GetKey("c"))
        {
            controller.height = 0.7f;
        }
        else
        {
            controller.height = 1.6f;
        }

        //Бег
        if (Input.GetKey("left shift"))
        {
            speed = 15f;
        }
        else
        {
            speed = 5f;
        }
    }
}

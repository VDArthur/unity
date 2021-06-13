using UnityEngine;

/* Метод заставляет Unity проверять наличие у объекта GameObject компонента именно того типа, 
 * который был передан в команду. Эта строка добавляется по желанию; 
 * но без этого компонента в сценарии появятся ошибки. */
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    //Сценарию нужна ссылка на объкт, относительно которого проходит перемещение.
    [SerializeField] private Transform target;

    public float rotSpeed = 7.0f;
    //Переменная для перемещений
    public float moveSpeed = 2.0f;

    public float pushForce = 3.0f;

    /* 
     * Переменные для движения по вертикали
     */
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;

    //Ссылка на контроллер персонажа.
    private CharacterController _charController;

    private Animator _animator;

    void Start()
    {
        _vertSpeed = minFall;

        //Метод GetComponent() возвращает остальные присоединенные к этому объекту компоненты.
        _charController = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Начинаем с вектора (0, 0, 0), постепенно добавляя компоненты движения.
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        /*
         * Код задающего перемещения по горизонтали
         * Движения обрабатывается только при нажатии клавиш со стрелками.
        */
        if (horInput != 0 || vertInput != 0)
        {
            //Умножаем обе оси, вдоль которых происходит движение, на его скорость,
            //а затем метод Vector3.ClampMagnitude() ограничивает модуль вектора этой скоростью.
            //Если так не сделать, у движения по диагонали будет больший вектор, чем у движения вдоль осей.

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.x = horInput * rotSpeed;
                movement.z = vertInput * rotSpeed;
                movement = Vector3.ClampMagnitude(movement, rotSpeed);
            }
            else
            {
                movement.x = horInput * moveSpeed;
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);
            }

            //Сохраняем начальную ориентацию, чтобы вернутся к ней после завершения работы с целевым объектом.
            Quaternion tmp = target.rotation;
            //Преобразование поворота, чтобы оно совершалось только относительно оси Y, а не всех трех осей.
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            //Преобразуем направления движения из локальных в глобальные координаты.
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            /* Назначаем полученный результат к персонажу, 
             * преобразуя Vector3 в Quaternion методом Quaternion.LookDirection() и присваивая это значение.            
             * Метод LookRotation() вычисляет кватернион, смотрящий в этом направлении. */
            Quaternion direction = Quaternion.LookRotation(movement);

            //Метод Quaternion.Lerp() выполняет плавный поворот из текущего положения в целевое
            //(третий параметр метода контролирует скорость вращения).
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        /*
         * Код задающего перемещения по вертикали.
         * Этот новый фрагмент кода проверяет, стоит ли персонаж на поверхности, 
         * так как именно от этого зависит изменение вертикальной скорости.
         */

        //Свойство isGrounded, предназначенно для проверки соприкосновения персонажа с поверхностью.
        if (_charController.isGrounded)
        {
            //Если нажата кнопка Jump, отвечающая за прыжок...
            if (Input.GetButtonDown("Jump"))
            {
                //Вертикальная скорость увеличивается.
                _vertSpeed = jumpSpeed;

                _animator.SetBool("Jumping", true);
            }
            else
            {
                //Иначе персонаж стоит на поверхности, значение вертикальной скорости становится нулевым.
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            //Если персонаж не стоит на поверхности, применяем графитацию, пока не будет достигнута предельная скорость.
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }
        //Рассчитанная вертикальная скорость присваивается оси Y вектора движения.
        movement.y = _vertSpeed;

        //умножаем значения перемещения на параметр deltaTime,
        //чтобы сделать данное преобразование независимым от частоты кадров
        //Полученные значения передаются методу CharacterController.Move(),
        //который и приводит персонаж в движение.
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
}

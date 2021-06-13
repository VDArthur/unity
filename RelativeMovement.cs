using UnityEngine;

/* ����� ���������� Unity ��������� ������� � ������� GameObject ���������� ������ ���� ����, 
 * ������� ��� ������� � �������. ��� ������ ����������� �� �������; 
 * �� ��� ����� ���������� � �������� �������� ������. */
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    //�������� ����� ������ �� �����, ������������ �������� �������� �����������.
    [SerializeField] private Transform target;

    public float rotSpeed = 7.0f;
    //���������� ��� �����������
    public float moveSpeed = 2.0f;

    public float pushForce = 3.0f;

    /* 
     * ���������� ��� �������� �� ���������
     */
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private float _vertSpeed;

    //������ �� ���������� ���������.
    private CharacterController _charController;

    private Animator _animator;

    void Start()
    {
        _vertSpeed = minFall;

        //����� GetComponent() ���������� ��������� �������������� � ����� ������� ����������.
        _charController = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //�������� � ������� (0, 0, 0), ���������� �������� ���������� ��������.
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        /*
         * ��� ��������� ����������� �� �����������
         * �������� �������������� ������ ��� ������� ������ �� ���������.
        */
        if (horInput != 0 || vertInput != 0)
        {
            //�������� ��� ���, ����� ������� ���������� ��������, �� ��� ��������,
            //� ����� ����� Vector3.ClampMagnitude() ������������ ������ ������� ���� ���������.
            //���� ��� �� �������, � �������� �� ��������� ����� ������� ������, ��� � �������� ����� ����.

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

            //��������� ��������� ����������, ����� �������� � ��� ����� ���������� ������ � ������� ��������.
            Quaternion tmp = target.rotation;
            //�������������� ��������, ����� ��� ����������� ������ ������������ ��� Y, � �� ���� ���� ����.
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            //����������� ����������� �������� �� ��������� � ���������� ����������.
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            /* ��������� ���������� ��������� � ���������, 
             * ���������� Vector3 � Quaternion ������� Quaternion.LookDirection() � ���������� ��� ��������.            
             * ����� LookRotation() ��������� ����������, ��������� � ���� �����������. */
            Quaternion direction = Quaternion.LookRotation(movement);

            //����� Quaternion.Lerp() ��������� ������� ������� �� �������� ��������� � �������
            //(������ �������� ������ ������������ �������� ��������).
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        /*
         * ��� ��������� ����������� �� ���������.
         * ���� ����� �������� ���� ���������, ����� �� �������� �� �����������, 
         * ��� ��� ������ �� ����� ������� ��������� ������������ ��������.
         */

        //�������� isGrounded, �������������� ��� �������� ��������������� ��������� � ������������.
        if (_charController.isGrounded)
        {
            //���� ������ ������ Jump, ���������� �� ������...
            if (Input.GetButtonDown("Jump"))
            {
                //������������ �������� �������������.
                _vertSpeed = jumpSpeed;

                _animator.SetBool("Jumping", true);
            }
            else
            {
                //����� �������� ����� �� �����������, �������� ������������ �������� ���������� �������.
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            //���� �������� �� ����� �� �����������, ��������� ����������, ���� �� ����� ���������� ���������� ��������.
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }
        //������������ ������������ �������� ������������� ��� Y ������� ��������.
        movement.y = _vertSpeed;

        //�������� �������� ����������� �� �������� deltaTime,
        //����� ������� ������ �������������� ����������� �� ������� ������
        //���������� �������� ���������� ������ CharacterController.Move(),
        //������� � �������� �������� � ��������.
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
}

using UnityEngine;

//�������� ������, ������, ����������, ���

public class FPS_Input : MonoBehaviour
{
    public CharacterController controller; //������ �� ����������
    public Transform groundCheck; //������� ������� ������� (�����)
    public LayerMask groundMask; //����

    public float speed = 5f; //��������
    public float gravity = -20f; //����������
    public float jumpHeigh = 3f; //������ ������
    public float sphereSize = 0.4f; //������ ����� (������)


    Vector3 velosity; //���������

    bool isGrounded; //�������� ��������� �������� �� ����� ��� ���

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //������� ��������� �����, ������� ��������� ��������� �� �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velosity.y < 0)
        {
            velosity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal"); //�������� �� �����������
        float z = Input.GetAxis("Vertical"); //�������� �� ���������

        Vector3 move = transform.right * x + transform.forward * z; //����������� ������/�����, �����/����.
        controller.Move(move * speed * Time.deltaTime); //��������� �������� ����������� � ����������

        //����������� ���������
        velosity.y += gravity * Time.deltaTime;
        controller.Move(velosity * Time.deltaTime);

        //������
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velosity.y = Mathf.Sqrt(jumpHeigh * -2 * gravity);
        }

        //����������
        if (Input.GetKey("c"))
        {
            controller.height = 0.7f;
        }
        else
        {
            controller.height = 1.6f;
        }

        //���
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

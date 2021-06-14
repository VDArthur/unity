using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    //����������, � �������� ���������� ��������� ���������� �����������.
    public float radius = 1.5f;

    //� ������ Update() ��������������� ������������ ����.
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            //����� OverlapSphere() �� ���������� ������ ���� ��������, ������������� �� ����� ��� �� ������������ ���������� �� �������� ��������������.
            //� ����� ���������� ��������� ��������� � ���������� radius, � �� ���������� ��� ������� ����� � ����������.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                //������� ������ ����������� �� ��������� � ������� ���������� ��������� ��������� �� ��������� �������.
                //����� ���������� ����� Vector3.Dot(), � ������� ���������� ����������� ������ ����������� � ������ �������� ��������� ������. 
                Vector3 direction = hitCollider.transform.position - transform.position;
                //���� ������������ ������� ��������� ������ � 1 (�������� ��� ������� � ���� ������� �������, ��� 0,5�), ������, ����������� �������� ����������� ���������.
                if (Vector3.Dot(transform.forward, direction) > .5f)
                {
                    //����� SendMessage() �������� ������� ����������� ������� ���������� �� ���� ��������� �������.
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}

using UnityEngine;

//�������� ���������� monitor, ������� ����� ������ ����

public class ColorChangeDevice : MonoBehaviour
{
    //����� �� �������� ������� � ��� �� ����� ������, ��� � � �������� ���������� ������. ����������� ������������ ��� ������ �������� ������� � ������ Operate � ��� ����� ��� ���������. � ������ ������ �� ��� ����������� ��������� ������� ��������� ����.
    public void Operate()
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<Renderer>().material.color = random;
    }
}

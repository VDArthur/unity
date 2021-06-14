using UnityEngine;

//��� ��� ��������, ��������������� ����������

public class DeviceTrigger : MonoBehaviour
{
    //������ ������� ��������, ������� ����� ������������ �������.
    [SerializeField] private GameObject[] targets;

    /*
     * ���� ����������� ��� �������� ��������� ���� ������� �������� ������ ��� ������ OnTriggerEnter(), ��� � ������
     * OnTriggerExit().
     */

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}

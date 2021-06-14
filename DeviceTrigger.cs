using UnityEngine;

//Код для триггера, контролирующего устройство

public class DeviceTrigger : MonoBehaviour
{
    //Массив целевых объектов, которые будет активировать триггер.
    [SerializeField] private GameObject[] targets;

    /*
     * Цикл применяется для рассылки сообщений всем целевым объектам внутри как метода OnTriggerEnter(), так и метода
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

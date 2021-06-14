using UnityEngine;

//Сценарий, по команде закрывающий и открывающий дверь

public class DoorOpenDevice : MonoBehaviour
{
    //Переменная определяет смещение, возникающее при открывании двери.
    [SerializeField] private Vector3 dPos;

    //Переменная для слежения за открытым состояние двери.
    private bool _open;

    //В методе Operate()преобразованию transform объекта присваивается новое положение, путем добавления или вычитания величины смещения в зависимости от того, закрыта или открыта дверь.
    public void Operate()
    {
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
        }
        _open = !_open;
    }

    public void Activate()
    {
        //Открывает дверь при условии что она закрыта.
        if (!_open)
        {
            Vector3 pos = transform.position + dPos;
            transform.position = pos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        //Аналогично, закрывает дверь при условии что она открита.
        if (_open)
        {
            Vector3 pos = transform.position - dPos;
            transform.position = pos;
            _open = false;
        }
    }
}

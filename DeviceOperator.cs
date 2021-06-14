using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    //Расстояние, с которого становится возможным управление устройством.
    public float radius = 1.5f;

    //В методе Update() рассматривается клавиатурный ввод.
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            //Метод OverlapSphere() он возвращает массив всех объектов, расположенных не более чем на определенном расстоянии от текущего местоположения.
            //В метод передаются положение персонажа и переменная radius, а он распознает все объекты рядом с персонажем.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                //Создаем вектор направление от персонажа к объекту вычитанием координат персонажа из координат объекта.
                //Затем вызывается метод Vector3.Dot(), в который передаются вычисленный вектор направления и вектор движения персонажа вперед. 
                Vector3 direction = hitCollider.transform.position - transform.position;
                //Если возвращенный методом результат близок к 1 (особенно при наличии в коде условия «больше, чем 0,5»), значит, направление векторов практически совпадает.
                if (Vector3.Dot(transform.forward, direction) > .5f)
                {
                    //Метод SendMessage() пытается вызывть именованную функцию независимо от типа цельевого объекта.
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}

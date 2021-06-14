using UnityEngine;

//—ценарий устройства monitor, которое может мен€ть цвет

public class ColorChangeDevice : MonoBehaviour
{
    //«десь мы объ€вили функцию с тем же самым именем, что и в сценарии управлени€ дверью. ”правл€ющий устройствами код всегда содержит функцию с именем Operate Ч она нужна дл€ активации. ¬ данном случае ее код присваивает материалу объекта случайный цвет.
    public void Operate()
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<Renderer>().material.color = random;
    }
}

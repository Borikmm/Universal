
using UnityEngine;

public abstract class ViewCollisionRadius : MonoBehaviour
{

    protected abstract void OnTriggerEnter2D(Collider2D collision);


    // Метод, вызываемый при выходе объекта из радиуса действия турели
    protected abstract void OnTriggerExit2D(Collider2D collision);
}


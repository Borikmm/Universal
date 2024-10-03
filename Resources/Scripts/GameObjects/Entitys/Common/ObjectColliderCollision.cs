
using UnityEngine;

public abstract class ObjectColliderCollision : MonoBehaviour
{
    protected abstract void OnCollisionEnter2D(Collision2D collision);


    // Метод, вызываемый при выходе объекта из радиуса действия турели
    protected abstract void OnCollisionExit2D(Collision2D collision);
}



using static UnityEngine.GraphicsBuffer;
using UnityEngine;

public abstract class TurelShoutRadiusCollider : MonoBehaviour
{

    protected BaseTurel _turelScript;
    protected EntityFraction _fractionTarget;

    public void Init(BaseTurel baseTurel, EntityFraction entityFraction)
    {
        _turelScript = baseTurel;
        _fractionTarget = entityFraction;
    }


    protected abstract void OnTriggerEnter2D(Collider2D collision);


    // Метод, вызываемый при выходе объекта из радиуса действия турели
    protected abstract void OnTriggerExit2D(Collider2D collision);
}


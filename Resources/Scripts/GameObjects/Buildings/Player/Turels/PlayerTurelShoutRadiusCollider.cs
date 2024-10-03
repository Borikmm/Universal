
using UnityEngine;

public class PlayerTurelShoutRadiusCollider : TurelShoutRadiusCollider
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, является ли объект врагом (вы можете заменить на вашу проверку тегов)
        if (collision.gameObject.layer == 6)
        {
            if (collision.GetComponentInParent<BaseEntity>().Fraction == _fractionTarget)
                _turelScript.AddTarget(collision.transform);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        _turelScript.RemoveTarget(collision.transform);
    }
}


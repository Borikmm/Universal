using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{

    [HideInInspector] public EntityFraction TargetEntityFraction;
    public float Damage;
    public float speed;
    //public float LifeTime;

    private Transform _target;



    public void Init(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        // Направление на цель

        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (_target.position - transform.position);
        float distanceThisFrame = speed * Time.deltaTime;

        // Двигаем пулю к цели
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }


    /// <summary>
    /// Hit with target
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            var obj = collision.gameObject.GetComponentInParent<BaseEntity>();
            if (obj.Fraction == TargetEntityFraction)
            {
                obj.SubXP(Damage);
                Destroy(gameObject);
            }
        }
    }
}


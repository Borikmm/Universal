
using UnityEngine;

public class EnemyViewCollisionRadius : ViewCollisionRadius
{
    private StateMachine _fsm;
    private GoToTargetEnemyState _state;
    private Transform _target;

    public void Init(StateMachine fsm)
    {
        _fsm = fsm;
        _state = _fsm.GetState<GoToTargetEnemyState>();
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject.GetComponent<BaseEntity>();
        if (obj != null)
        {
            if (obj.Fraction == EntityFraction.Player && obj is not BaseCube)
            {
                var dist1 = Vector2.Distance(transform.position, _state.Target);
                var dist2 = Vector2.Distance(transform.position, collision.transform.position);
                
                if (dist2 < dist1)
                {
                    _state.FixedTargeting = true;
                    _state.Target = collision.transform.position;
                    _target = collision.transform;
                }
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == _target)
        {
            _state.FixedTargeting = false;
        }
    }
}


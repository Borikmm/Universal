
using UnityEngine;

public class CubeColliderCollision : ObjectColliderCollision
{

    private ICollisionAction _colAction;


    public void Init(ICollisionAction colAction)
    {
        _colAction = colAction;
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
        var obj = collision.gameObject.GetComponentInParent<BaseEntity>();
        if (obj != null)
        {
            switch (obj.Fraction)
            {
                case EntityFraction.Enemy:
                    _colAction.CollisionWithEnemy(collision.gameObject);
                    break;
                case EntityFraction.Player:
                    _colAction.CollisionWithPlayer(collision.gameObject);
                    break;
            }
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
    }
}


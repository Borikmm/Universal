using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyCube : BaseCube, IBonfireAction
{


    private EnemyViewCollisionRadius _viewCollisionScript;


    protected override void AddStates()
    {
        _fsm.AddState(new GoToTargetEnemyState(_fsm, transform, _agent));//
    }


    protected override void EnterToStartState()
    {
        if (_isActive)
            _fsm.EnterIn<GoToTargetEnemyState>();
    }


    public override void CollisionWithEnemy(GameObject gameObject)
    {
        if (gameObject.GetComponentInParent<BaseEntity>() is BonFire bonfire)
        {
            EnemyBonFireAction(bonfire);
            DestroyThisObject();
        }
    }

    public override void CollisionWithPlayer(GameObject gameObject)
    {
        var obj = gameObject.GetComponentInParent<BaseEntity>();
        // Можо сделать enum с типом сущностей
        if (obj is BonFire bonfire)
        {
            PlayerBonFireAction(bonfire);
            DestroyThisObject();
        }
        else if (obj is BaseCube)
        {
            DestroyThisObject();
        }
        else if (obj is FreandlySpawner|| obj is BaseTurel)
        {
            obj.SubXP(1);
            DestroyThisObject();
        }
    }

    protected override void Start()
    {
        base.Start();
        _power = 2;
        InitViewCollision();
    }

    private void InitViewCollision()
    {
        _viewCollisionScript = GetComponentInChildren<EnemyViewCollisionRadius>();
        if (_viewCollisionScript != null) _viewCollisionScript.Init(_fsm);
    }

    private void Awake()
    {
        _fraction = EntityFraction.Enemy;
    }

    public void PlayerBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(-_power);
    }

    public void EnemyBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(_power);
    }
}


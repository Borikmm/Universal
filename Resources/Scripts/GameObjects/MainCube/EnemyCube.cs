using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyCube : BaseCube, IBonfireAction
{
    
    protected override void AddStates()
    {
        _fsm.AddState(new GoToTargetEnemyState(_fsm, transform, _agent));//
    }


    protected override void EnterToStartState()
    {
        _fsm.EnterIn<GoToTargetEnemyState>();
    }


    public override void CollisionWithEnemy(GameObject gameObject)
    {
        if (gameObject.GetComponent<BaseEntity>() is BonFire bonfire)
        {
            EnemyBonFireAction(bonfire);
            DestroyThisObject();
        }
    }

    public override void CollisionWithPlayer(GameObject gameObject)
    {
        // Можо сделать enum с типом сущностей
        if (gameObject.GetComponent<BaseEntity>() is BonFire bonfire)
        {
            PlayerBonFireAction(bonfire);
            DestroyThisObject();
        }
        else if (gameObject.GetComponent<BaseEntity>() is BaseCube)
        {
            BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(gameObject);
            Destroy(gameObject);
        }
        else if (gameObject.GetComponent<BaseEntity>() is FreandlySpawner spawner)
        {
            spawner.SubXP(1);
            DestroyThisObject();
        }
    }

    protected override void Start()
    {
        base.Start();
        _fraction = ClassEntity.Enemy;
        _power = 2;
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


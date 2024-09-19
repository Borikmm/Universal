using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : BaseCube, IBonfireAction
{

    public override void CollisionWithEnemy(GameObject gameObject)
    {
        if (gameObject.GetComponent<BaseEntity>() is EnemyBonfire bonfire)
        {
            EnemyBonFireAction(bonfire);
            DestroyThisObject();
        }
        else if (gameObject.GetComponent<BaseEntity>() is BaseCube)
        {
            BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(gameObject);
            Destroy(gameObject);
        }
    }

    public override void CollisionWithPlayer(GameObject gameObject)
    {
        if (gameObject.GetComponent<BaseEntity>() is PlayerBonfire bonfire)
        {
            PlayerBonFireAction(bonfire);
            DestroyThisObject();
        }
    }

    protected override void WhatDoWhenEnterInTheLight()
    {
        if (_destroyedCoroutine != null) StopCoroutine(_destroyedCoroutine);
        _destroyedProcess = false;
    }

    protected override void WhatDoWhenExitFromTheLight()
    {
        _destroyedCoroutine = StartCoroutine(DestroyAfterDelay());
        _destroyedProcess = true;
    }


    protected override void Start()
    {
        base.Start();
        _fraction = ClassEntity.Player;
        _power = 1;
    }

    public virtual void PlayerBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(_power);
    }

    public virtual void EnemyBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(-_power);
    }
}

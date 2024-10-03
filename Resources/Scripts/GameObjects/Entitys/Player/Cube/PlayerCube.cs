using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerCube : BaseCube, IBonfireAction, IControlable, IDarkRaycastObject
{

    // Destroyed process in the dark 
    protected bool _destroyedProcess;
    protected Coroutine _destroyedCoroutine;

    // InfinityControl
    private bool _goToInfinity = false;


    public void Select()
    {
        RecolorThis(Color.green);
    }

    public void UnSelect()
    {
        RecolorThis(Color.white);
    }

    public override void GoTo(Vector3 target)
    {
        var state = _fsm.GetState<GoToTargetState>();
        state.Speed = _speed;
        state.Target = target;
        _fsm.EnterIn(state);
        _goToInfinity = true;
    }

    public override void GoTo(Transform target)
    {
        var state = _fsm.GetState<GoToTargetState>();
        state.Speed = _speed;
        state.Target = target.position;
        _fsm.EnterIn(state);
        _goToInfinity = true;
    }

    private void GoToInfinity()
    {
        if (_goToInfinity)
        {
            _fsm.GetState<GoToTargetState>().Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_stateNow != "GoToTargetState") _fsm.EnterIn<GoToTargetState>();
        }
    }



    public override void Stop()
    {
        _goToInfinity = false;
    }

    public override void CollisionWithEnemy(GameObject gameObject)
    {
        var obj = gameObject.GetComponentInParent<BaseEntity>();
        if (obj is EnemyBonfire bonfire)
        {
            EnemyBonFireAction(bonfire);
            DestroyThisObject();
        }
        else if (obj is BaseCube)
        {
            DestroyThisObject();
        }
    }

    public override void CollisionWithPlayer(GameObject gameObject)
    {
        var obj = gameObject.GetComponentInParent<BaseEntity>();
        if (obj is PlayerBonfire bonfire)
        {
            PlayerBonFireAction(bonfire);
            DestroyThisObject();
        }
    }

    public override void WhatDoWhenEnterInTheLight()
    {
        if (_destroyedCoroutine != null) StopCoroutine(_destroyedCoroutine);
        _destroyedCoroutine = null;
    }

    public override void WhatDoWhenExitFromTheLight()
    {
        if (_destroyedCoroutine != null) return;
        _destroyedCoroutine = StartCoroutine(DestroyAfterDelay());
    }

    public override void EnterToDarkWall(Light2D light)
    {
        if (!DelObjFromWall)
        {
            WhatDoWhenExitFromTheLight();
            InTheLightList.Remove(light);
            _delObjFromWall = true;
        }
    }

    public override void ExitToDarkWall(Light2D light)
    {
        if (DelObjFromWall)
        {
            WhatDoWhenEnterInTheLight();
            InTheLightList.Remove(light);
            _delObjFromWall = false;
        }
    }


    protected override void Start()
    {
        base.Start();
        _power = 1;
        _fraction = EntityFraction.Player;
    }

    public virtual void PlayerBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(_power);
    }

    public virtual void EnemyBonFireAction(BonFire bonfire)
    {
        bonfire.CubeAttenuation(-_power);
    }


    protected override void DestroyThisObject()
    {
        // Remove from global list player cubes
        BootStrap.GameManager.PlayerBaseCubes.RemoveElement(this);
        base.DestroyThisObject();
    }


    protected override void Update()
    {
        base.Update();
        GoToInfinity();
    }

}

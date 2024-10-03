using Patterns.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
//[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseCube : BaseEntity, IMovable, ILightedEntity, ICollisionAction
{
    [Header("Cube parametres: ")]

    [SerializeField] protected float _speed = 1f;

    public List<Light2D> _inTheLight = new();

    protected bool _delObjFromWall = false;

    public float Speed { get { return _speed; } set { _speed = value; } }

    public List<Light2D> InTheLightList { get {return _inTheLight; } set { _inTheLight = value; } }

    public bool DelObjFromWall { get {return _delObjFromWall; } set { _delObjFromWall = value; } }

    protected int _power;
    protected NavMeshAgent _agent;

    // Collision mechanic

    public virtual void CollisionWithPlayer(GameObject gameObject)
    {
    }

    public virtual void CollisionWithEnemy(GameObject gameObject)
    {
    }
    ///

    public virtual void ExitToDarkWall(Light2D light) { }

    public virtual void EnterToDarkWall(Light2D light) { }


    public virtual void WhatDoWhenEnterInTheLight()
    {
    }

    public virtual void WhatDoWhenExitFromTheLight()
    {
    }



    public virtual void GoTo(Transform target)
    {
        
    }

    public virtual void GoTo(Vector3 target)
    {

    }

    public virtual void Stop()
    {
        
    }



    protected override void Start()
    {
        base.Start();
        Init();
        AddStates();
        EnterToStartState();
    }

    protected virtual void Init()
    {
        _agent = GetComponent<NavMeshAgent>();
        gameObject.layer = 6; // set mobs layer
        FSMInit();
    }

    protected virtual void AddStates()
    {
        _fsm.AddState(new WaitingState(_fsm));
        _fsm.AddState(new GoToTargetState(_fsm, transform, _agent));
    }

    protected virtual void EnterToStartState()
    {
        _fsm.EnterIn<WaitingState>();
    }


    protected override void Update()
    {
        base.Update();
        ResetRotationAndPosY();
        DarkChecker();

    }


    /// <summary>
    /// For truth view in navmeshagent
    /// </summary>
    private void ResetRotationAndPosY()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    protected override void DestroyThisObject()
    {
        BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(_collisionColider);
        base.DestroyThisObject();
    }

    private void DarkChecker()
    {
        // Destroyed checker
        if (InTheLightList.Count > 0)
        {
            WhatDoWhenEnterInTheLight();
        }
        if (InTheLightList.Count <= 0)
        {
            WhatDoWhenExitFromTheLight();
        }
    }


    protected IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(TheDarkGlobalMechanic.DestroyTime);
        BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(_collisionColider);
        Destroy(gameObject); // ”ничтожаем объект, который вышел из коллайдера
    }


}

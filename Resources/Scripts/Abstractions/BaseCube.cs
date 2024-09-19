using Patterns.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

//[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseCube : BaseEntity
{
    // Destroyed process in the dark 
    protected bool _destroyedProcess;
    protected Coroutine _destroyedCoroutine;
    public bool InTheLight;
    public bool AdditionLightZone;
    //

    public bool _goToInfinity = false;

    protected NavMeshAgent _agent;
    [SerializeField] private float _speed = 1f;




    protected int _power;

    public void GoTo()
    {

        var state = _fsm.GetState<GoToTargetState>();
        state.Speed = _speed;
        state.Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _fsm.EnterIn(state);
        _goToInfinity = true;
    }


    public void StopGoTo()
    {
        _goToInfinity = false;
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
        GoToInfinity();
    }


    /// <summary>
    /// For truth view in navmeshagent
    /// </summary>
    private void ResetRotationAndPosY()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    protected virtual void WhatDoWhenEnterInTheLight()
    {

    }

    protected virtual void WhatDoWhenExitFromTheLight()
    {

    }


    protected override void DestroyThisObject()
    {
        BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(gameObject);
        base.DestroyThisObject();
    }

    private void DarkChecker()
    {
        // Destroyed checker
        if (InTheLight && _destroyedProcess)
        {
            WhatDoWhenEnterInTheLight();
        }
        if (!InTheLight && !_destroyedProcess)
        {
            WhatDoWhenExitFromTheLight();
        }
    }

    private void GoToInfinity()
    {
        if (_goToInfinity)
        {
            _fsm.GetState<GoToTargetState>().Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_stateNow != "GoToTargetState") _fsm.EnterIn<GoToTargetState>();
        }
    }


    protected IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(TheDarkGlobalMechanic.DestroyTime);
        if (InTheLight)
        {
            _destroyedProcess = false;
            yield break;
        } 
        BootStrap.PlayerInput.SelectingService.RemoveSelectedObject(gameObject);
        Destroy(gameObject); // ”ничтожаем объект, который вышел из коллайдера
    }


}

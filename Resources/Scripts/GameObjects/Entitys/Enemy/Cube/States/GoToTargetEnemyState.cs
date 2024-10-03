using Patterns.FSM;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GoToTargetEnemyState : FSMState
{
    private UnityEngine.Vector3 _target;

    public bool FixedTargeting = false;

    public Vector3 Target 
    {  
        get { return _target; } 
        set 
        { 
            _target = value;
        }
    }


    private UnityEngine.Transform _thisTrans;
    private NavMeshAgent _navMeshAgent;
    public GoToTargetEnemyState(StateMachine workerStateMachine, UnityEngine.Transform thisTra, NavMeshAgent agent) : base(workerStateMachine)
    {
        _thisTrans = thisTra;
        _navMeshAgent = agent;
    }


    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        _navMeshAgent.ResetPath();

    }

    public override void Update()
    {
        if (!FixedTargeting)
            _target = FindNearest();
        GoToTarget();
    }



    private Vector3 FindNearest()
    {
        float minDist = Mathf.Infinity, distance;
        Vector3? minDistObj = null;
        foreach (var obj in BootStrap.GameManager.LightingPoints.Values)
        {
            distance = Vector2.Distance(_thisTrans.transform.position, obj.gameObject.transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                minDistObj = obj.gameObject.transform.position;
            }
        }
        if (minDistObj == null)
        {
            return Vector3.zero;
        }
        return (Vector3)minDistObj;
    }


    private void GoToTarget()
    {
        // Проверяем, есть ли цель
        if (_target != Vector3.zero && _target != null)
        {
            // Устанавливаем цель для NavMeshAgent
            if (_navMeshAgent.destination != _target)
            {
                _navMeshAgent.SetDestination(_target);
            }

                // Проверяем расстояние до цели
                /*            float distance = Vector2.Distance(_thisTrans.position, _target);
                            // Если расстояние меньше заданного, останавливаемся
                            if (distance <= StoppingDistance)
                            {
                                //_navMeshAgent.isStopped = true; // Останавливаем агента
                                //_finiteStatesMachine.EnterIn<WaitingState>();
                            }
                            else
                            {
                                //_navMeshAgent.isStopped = false;
                            }*/
        }
    }
}
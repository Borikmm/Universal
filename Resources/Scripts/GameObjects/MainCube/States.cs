using Patterns.FSM;
using UnityEngine.AI;
using UnityEngine;

public class WaitingState : FSMState
{
    public WaitingState(StateMachine workerStateMachine) : base(workerStateMachine)
    {

    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }
}


public class GoToTargetState : FSMState
{
    private UnityEngine.Vector3 _target;

    public UnityEngine.Vector3 Target
    {
        set
        {
            _target = new Vector3(value.x, value.y, 0);
        }
    }

    public float Speed = 2f; // Скорость движения
    public float StoppingDistance = 0.01f; // Расстояние, при котором останавливаемся
    private NavMeshAgent _navMeshAgent;
    private UnityEngine.Transform _thisTrans;

    public GoToTargetState(StateMachine workerStateMachine, UnityEngine.Transform thisTra, NavMeshAgent agent) : base(workerStateMachine)
    {
        _thisTrans = thisTra;
        _navMeshAgent = agent;
    }


    public override void Enter()
    {
        _navMeshAgent.speed = Speed;
    }

    public override void Exit()
    {
        
        _navMeshAgent.ResetPath();

    }

    public override void Update()
    {
        GoToTarget();
    }

    private void GoToTarget()
    {
        // Проверяем, есть ли цель
        if (_target != null)
        {
            // Устанавливаем цель для NavMeshAgent
            _navMeshAgent.SetDestination(_target);

            // Проверяем расстояние до цели
            float distance = Vector2.Distance(_thisTrans.position, _target);
            // Если расстояние меньше заданного, останавливаемся
            if (distance <= StoppingDistance)
            {
                //_navMeshAgent.isStopped = true; // Останавливаем агента
                _finiteStatesMachine.EnterIn<WaitingState>();
            }
            else
            {
                //_navMeshAgent.isStopped = false;
            }
        }
    }
}




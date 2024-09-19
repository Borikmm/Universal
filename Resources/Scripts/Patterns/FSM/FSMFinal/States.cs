
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Patterns.FSM
{
    public class WalkingState : FSMState
    {
        private Transform _transform;
        private float _movingSpeed;
        private bool _isWalking;
        private Vector3 _target;

        public WalkingState(StateMachine workerStateMachine, Transform transform, float movingSpeed) : base(workerStateMachine)
        {
            _transform = transform;
            _movingSpeed = movingSpeed;
        }

        public override void Enter()
        {
            StartWalking();
        }

        public override void Exit()
        {
            _isWalking = false;
        }


        private void StartWalking()
        {
            _target = FindingTarget();
            _isWalking = true;
        }

        private void Walking()
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target, _movingSpeed * Time.deltaTime);

            if (this._transform.position == _target)
            {
                _finiteStatesMachine.EnterIn<WaitingState>();
            }
        }

        private Vector3 FindingTarget()
        {
            return new Vector3(
                Random.Range(_transform.position.x - 10, _transform.transform.position.x + 10),
                _transform.transform.position.y,
                Random.Range(_transform.transform.position.z - 10, _transform.transform.position.z + 10)
                );
        }

        public override void Update()
        {
            if (_isWalking)
            {
                Walking();
            }
            
        }
    }
}

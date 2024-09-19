using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Patterns.FSM
{
    public class WorkerStateMachine : MonoBehaviour
    {

        private StateMachine _fsm;


        [SerializeField] float _movingSpeed;


        private void Start()
        {

            _fsm = new StateMachine();

            _fsm.AddState(new WalkingState(_fsm, this.transform, _movingSpeed));
            _fsm.AddState(new WaitingState(_fsm));


            _fsm.EnterIn<WaitingState>();
        }


        private void Update()
        {
            _fsm.Update();
        }


        private void OnMouseDown()
        {
            _fsm.EnterIn<WalkingState>();
        }

    }
}

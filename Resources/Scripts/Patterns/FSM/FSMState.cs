using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.FSM
{
    public abstract class FSMState : IState
    {
        protected readonly StateMachine _finiteStatesMachine;

        public FSMState(StateMachine stateMachine)
        {
            _finiteStatesMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }
    }
}

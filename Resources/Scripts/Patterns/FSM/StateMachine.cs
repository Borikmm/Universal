using Patterns.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    protected Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
    protected IState _currentState;

    public IState CurrentState => _currentState;
    public Action<string> AChangeState;

    public void EnterIn<Tstate>() where Tstate : IState
    {
        if (_states.TryGetValue(typeof(Tstate), out var state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            AChangeState?.Invoke(state.ToString());
        }
    }

    public void EnterIn(IState stateEnter)
    {
        if (_states.TryGetValue(stateEnter.GetType(), out var state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            AChangeState?.Invoke(state.ToString());
        }
    }


    public void AddState(IState state)
    {
        Type type = state.GetType();

        if (!_states.ContainsKey(type))
        {
            _states.Add(type, state);
        }
    }


    public Tstate GetState<Tstate>() where Tstate : IState
    {
        if (_states.TryGetValue(typeof(Tstate), out var state))
        {
            return (Tstate)state;
        }
        return default;
    }


    public void ClearCurentState()
    {
        _currentState = null;
    }



    public void Update()
    {
        _currentState?.Update();
    }

}

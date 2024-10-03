using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class BaseFSMEntity : MonoBehaviour
{
    // Finite state machine
    protected StateMachine _fsm;

    [Header("Main entity parametres:")]
    [SerializeField] protected bool _isActive = true;

    [SerializeField] protected string _stateNow = "base";



    /// <summary>
    /// Runtime update state name
    /// </summary>
    /// <param name="stateNow"></param>
    private void ChangeState(string stateNow)
    {
        _stateNow = stateNow;
    }



    protected virtual void Start()
    {
        FSMInit();
    }

    /// <summary>
    /// Init fsm
    /// </summary>
    protected virtual void FSMInit()
    {
        _fsm = new StateMachine();
        _fsm.AChangeState += ChangeState;
    }

    /// <summary>
    /// Unsubscribe ChangeState update
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (_fsm != null)
        {
            _fsm.AChangeState -= ChangeState;
        }
    }

    /// <summary>
    /// Update fsm
    /// </summary>
    protected virtual void Update()
    {
        if (_fsm != null)
        {
            _fsm.Update();
        }
    }

}


using System;
using Unity.VisualScripting;
using UnityEngine;


public class BaseXPManager : MonoBehaviour
{
    [Header("Main Parametres:")]
    [SerializeField] private float _xp;

    [Header("-------------")]
    private float _maxXP;


    protected void SetXP(float xp)
    {
        _xp = xp;
        _maxXP = xp;
    }


    public void AddXP(float xp)
    {
        if (xp < 0) return;
        if (_xp + xp >= _maxXP)
        {
            _xp = _maxXP;
            return;
        }
        _xp += xp;
    }


    protected virtual void DeadWhenZeroXP()
    {

    }

    public void SubXP(float xp)
    {
        if (xp < 0) return;
        if (_xp - xp <= 0)
        {
            DeadWhenZeroXP();
        }
        _xp -= xp;
    }
}


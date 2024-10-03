using System;
using Unity.VisualScripting;
using UnityEngine;


public class BaseXPManager : BaseFSMEntity
{
    [Header("Main Parametres:")]
    [SerializeField] private float _hp;

    [Header("-------------")]
    private float _maxHP;


    protected void SetXP(float xp)
    {
        _hp = xp;
        _maxHP = xp;
    }


    public void AddXP(float xp)
    {
        if (xp < 0) return;
        if (_hp + xp >= _maxHP)
        {
            _hp = _maxHP;
            return;
        }
        _hp += xp;
    }


    protected virtual void DeadWhenZeroXP()
    {
        DestroyThisObject();
    }

    public void SubXP(float xp)
    {
        if (xp < 0) return;
        if (_hp - xp <= 0)
        {
            DeadWhenZeroXP();
        }
        _hp -= xp;
    }

    protected virtual void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}


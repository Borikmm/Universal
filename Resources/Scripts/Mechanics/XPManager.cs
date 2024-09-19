using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct XPManager
{
    private float _maxXP;
    private float _xp;

    XPManager(float xp)
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

    public void SubXP(float xp)
    {
        if (xp < 0)
        {

        }
        if (_xp - xp <= 0)
        {
            _xp = _maxXP;
            return;
        }
        _xp += xp;
    }


}

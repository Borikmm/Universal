
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTurel : BaseTurel, IDarkRaycastObject
{
    protected override void Start()
    {
        base.Start();
        _fraction = EntityFraction.Player;
    }

    public override void EnterToDarkWall(Light2D light)
    {
        if (!DelObjFromWall)
        {
            WhatDoWhenExitFromTheLight();
            InTheLightList.Remove(light);
            _delObjFromWall = true;
        }
    }

    public override void ExitToDarkWall(Light2D light)
    {
        if (DelObjFromWall)
        {
            WhatDoWhenEnterInTheLight();
            InTheLightList.Add(light);
            _delObjFromWall = false;
        }
    }
}


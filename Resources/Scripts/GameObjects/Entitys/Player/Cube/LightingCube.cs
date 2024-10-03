using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightingCube : PlayerCube, ILightSource, IObjWithLightingCollider
{
    protected Light2D _light;
    public Light2D Light { get { return _light; } set { _light = value;  } }

    protected Light2DCollision _collisionScript;
    public Light2DCollision CollisionScript { get { return _collisionScript; } set { _collisionScript = value; } }

    protected override void DestroyThisObject()
    {
        BootStrap.GameManager.LightingPoints.RemoveElement(_light);
        base.DestroyThisObject();
        
    }

    public override void WhatDoWhenEnterInTheLight()
    {
        
    }

    public override void WhatDoWhenExitFromTheLight()
    {
        
    }

    protected override void Start()
    {
        _light = GetComponentInChildren<Light2D>(); // For init collider IObjWithLightingCollider on base start


        base.Start();
        _power = 5;
    }

    public void InitLightingCollider()
    {
        _collisionScript = GetComponentInChildren<Light2DCollision>();
        if (_collisionScript != null) _collisionScript.Init(Light);
    }
}


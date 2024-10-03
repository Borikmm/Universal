using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


/// <summary>
/// class who check cubes in light or dark zone and react this
/// </summary>
public abstract class Light2DCollision: MonoBehaviour
{

    protected Light2D _light;


    public void Init(Light2D light)
    {
        _light = light;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
    }


}


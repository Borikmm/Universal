using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;


public abstract class BaseLightingObject : BaseEntity
{
    protected Light2D _light;

    protected override void Start()
    {
        base.Start();
        _light = GetComponentInChildren<Light2D>();
    }
}


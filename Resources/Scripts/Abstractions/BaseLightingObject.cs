using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;


public abstract class BaseLightingObject : BaseEntity, ILightSource
{
    protected Light2D _light;
    public Light2D Light { get { return _light; } set { _light = value; } }

    protected override void Start()
    {
        _light = GetComponentInChildren<Light2D>();
        base.Start();
    }

    protected override void DestroyThisObject()
    {
        BootStrap.GameManager.LightingPoints.RemoveElement(_light);
        base.DestroyThisObject();
    }
}


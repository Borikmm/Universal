using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BaseBuilding : BaseEntity, ILightedEntity
{
    [Header("Buildings parametres:")]
    public float Power = 0f;

    // Power mechanic
    private Light2D _nearestLight;

    protected List<Light2D> _inTheLight = new();

    protected bool _delObjFromWall = false;

    public List<Light2D> InTheLightList { get { return _inTheLight; } set { _inTheLight = value; } }

    public bool DelObjFromWall { get { return _delObjFromWall; } set { _delObjFromWall = value; } }

    private float FindProp()
    {

        float maxProp = 0;

        foreach (var light in _inTheLight)
        {
            var propNow = PropFoLight(light);
            if (propNow > maxProp)
            {
                maxProp = propNow;
                _nearestLight = light;
            }
        }

        return maxProp;

    }

    protected override void Update()
    {
        base.Update();
        UpdateLightPower();
    }


    private void UpdateLightPower()
    {
        if (InTheLightList.Count > 0)
        {
            Power = FindProp();
            if (Power < 0)
            {
                Power = 0;
            }
        }
        else
        {
            Power = 0;
        }
    }


    private float PropFoLight(Light2D light)
    {
        var distanceMax = light.pointLightOuterRadius;
        var distanceNow = Vector2.Distance(transform.position, light.transform.position);
        float objectSizeOffset = light.GetComponentInParent<BaseEntity>().Collider.bounds.extents.magnitude; // Размер объекта. Можно оптимизировать, заменив Light2D классом который содержит и источник света и колайдер!!!!!!
        float adjustedDistanceNow = distanceNow - objectSizeOffset; // Коррекция расстояния
        float power = Mathf.Clamp(100 - (100 * adjustedDistanceNow / distanceMax), 0, 100);
        return power;

    }

    public virtual void WhatDoWhenEnterInTheLight()
    {
    }

    public virtual void WhatDoWhenExitFromTheLight()
    {
    }

    public virtual void ExitToDarkWall(Light2D light)
    {
    }

    public virtual void EnterToDarkWall(Light2D light)
    {
    }
}
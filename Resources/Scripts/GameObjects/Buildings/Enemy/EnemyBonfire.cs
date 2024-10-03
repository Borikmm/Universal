using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyBonfire : BonFire
{

    private float OutherDifference => _light.pointLightInnerRadius / _light.pointLightOuterRadius;
    public override void CubeAttenuation(int power)
    {
        float step = (float)startMinInnerRadiusStart / 10 * (float)power;
        ChangeIntensivity(step / IntensivityDifference);
        ChangeOuterRadius(step / OutherDifference);
        ChangeInnerRadius(step);

        float adjustment = Convert.ToSingle(startMinInnerRadiusStart) / (Convert.ToSingle(startOutherRadiusStart) / step);
        ChangeMinInnerRadius(adjustment);
        ChangeMaxInnerRadius(adjustment);

        if (_theDarkCircleCollider != null) ChangesTheDarkColidersRadius(step);
    }

    protected override void Attenuation(bool direction)
    {
        float attenuationValue = direction ? _valueAttenuation : -_valueAttenuation;

        ChangeIntensivity(attenuationValue / IntensivityDifference);
        ChangeOuterRadius(attenuationValue);
        ChangeInnerRadius(attenuationValue / InnerDifference);

        float adjustment = startMinInnerRadiusStart / (startOutherRadiusStart / _valueAttenuation);

        ChangeMinInnerRadius(direction ? adjustment : -adjustment);
        ChangeMaxInnerRadius(direction ? adjustment : -adjustment);
        if (_theDarkCircleCollider != null) ChangesTheDarkColidersRadius(attenuationValue);
    }

    protected override void ChangesTheDarkColidersRadius(float value)
    {
        _theDarkCircleCollider.radius = minInnerRadius;
    }

    protected override void Start()
    {
        base.Start();
        _fraction = EntityFraction.Enemy;
        if (_theDarkCircleCollider != null) _theDarkCircleCollider.radius = startMinInnerRadiusStart;
    }

    protected override bool CheckFire()
    {
        return (_isActive && _light.pointLightOuterRadius > 0);
    }
}


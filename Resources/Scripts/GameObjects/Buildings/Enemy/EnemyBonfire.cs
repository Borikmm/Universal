using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyBonfire : BonFire
{


    protected override void ChangesTheDarkColidersRadius(float value)
    {
        _theDarkCircleCollider.radius = minInnerRadius;
    }

    protected override void Start()
    {
        base.Start();
        _fraction = ClassEntity.Enemy;
    }

    protected override bool CheckFire()
    {
        return (_isActive && _light.pointLightOuterRadius > 0);
    }
}


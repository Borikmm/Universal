using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LightingCube : PlayerCube
{
    protected override void WhatDoWhenEnterInTheLight()
    {
        
    }

    protected override void WhatDoWhenExitFromTheLight()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        _power = 5;
    }
}


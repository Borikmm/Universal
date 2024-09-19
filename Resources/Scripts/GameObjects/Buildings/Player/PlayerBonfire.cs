using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerBonfire : BonFire
{

    protected override void Start()
    {
        base.Start();
        _fraction = ClassEntity.Player;
    }
}


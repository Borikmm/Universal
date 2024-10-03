using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerBonfire : BonFire
{

    public List<IDarkRaycastObject> PlayersEntitysForRaycasting = new();
    private DarkRaycasting _darkRaycasting;

    protected override void Start()
    {
        base.Start();
        _fraction = EntityFraction.Player;
        InitServices();
    }


    private void InitServices()
    {
        _darkRaycasting = GetComponent<DarkRaycasting>();
        _darkRaycasting.Init(this, _light);
    }

    public override void InitLightingCollider()
    {
        base.InitLightingCollider();
        ((PlayerBonFireLightCollision)_collisionScript).InitBonfire(this);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBonFireLightCollision : Light2DCollision
{
    private PlayerBonfire _bonFire;

    public void InitBonfire(PlayerBonfire bonFire)
    {
        _bonFire = bonFire;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
        {
            var obj = collision.gameObject.GetComponentInParent<ILightedEntity>();
            if (obj == null) return; 
            //Debug.Log($"Обьект не будет уничтожен {collision.name}!");

            obj.InTheLightList.Add(_light);

            if (obj is PlayerCube || obj is BaseBuilding)
            {
                if (obj is IDarkRaycastObject)
                {
                    _bonFire.PlayersEntitysForRaycasting.Add((IDarkRaycastObject)obj);
                }

            }

        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
        {
            var obj = collision.gameObject.GetComponentInParent<ILightedEntity>();
            if (obj == null) return;
            //Debug.Log($"Через {TheDarkGlobalMechanic.DestroyTime} будет уничтожен {collision.name}!");
            if (obj.InTheLightList.Count > 0) obj.InTheLightList.Remove(_light);

            if (obj is PlayerCube || obj is BaseBuilding)
            {
                if (obj is IDarkRaycastObject)
                {
                    _bonFire.PlayersEntitysForRaycasting.Remove((IDarkRaycastObject)obj);
                }
            }

        }
    }
}


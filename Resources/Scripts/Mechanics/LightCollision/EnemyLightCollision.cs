using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyLightCollision : Light2DCollision
{


    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
        {
            var obj = collision.gameObject.GetComponentInParent<ILightedEntity>();
            if (obj == null) return;
           //Debug.Log($"Обьект не будет уничтожен {collision.name}!");
            obj.InTheLightList.Remove(_light);
        }



    }


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
        {
            var obj = collision.gameObject.GetComponentInParent<ILightedEntity>();
            if (obj == null) return;
            //Debug.Log($"Через {TheDarkGlobalMechanic.DestroyTime} будет уничтожен {collision.name}!");
            /*if (obj.AdditionLightZone)
            {
                obj.AdditionLightZone = true;
                return;
            }
            obj.InTheLight = true;*/
            obj.InTheLightList.Add(_light);
        }
    }



}


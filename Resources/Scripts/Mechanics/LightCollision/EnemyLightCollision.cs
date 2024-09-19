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

        if (collision.gameObject.layer == 6)
        {
            var obj = collision.gameObject.GetComponent<BaseCube>();
            if (obj.Fraction != ClassEntity.Player) return;
            //Debug.Log($"Обьект не будет уничтожен {collision.name}!");
            if (obj.InTheLight) obj.AdditionLightZone = false;
            obj.InTheLight = false;
        }



    }


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            var obj = collision.gameObject.GetComponent<BaseCube>();
            if (obj.Fraction != ClassEntity.Player) return;
            //Debug.Log($"Через {TheDarkGlobalMechanic.DestroyTime} будет уничтожен {collision.name}!");
/*            if (obj.AdditionLightZone)
            {
                obj.AdditionLightZone = true;
                return;
            }*/
            obj.InTheLight = true;
        }
    }



}


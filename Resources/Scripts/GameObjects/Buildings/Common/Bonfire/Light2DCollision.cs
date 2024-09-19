using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Light2DCollision: MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 6)
        {
            var obj = collision.gameObject.GetComponent<BaseCube>();
            if (obj.Fraction != ClassEntity.Player) return;
            //Debug.Log($"Обьект не будет уничтожен {collision.name}!");
            if (obj.InTheLight) obj.AdditionLightZone = true;
            obj.InTheLight = true;
        }


        
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            var obj = collision.gameObject.GetComponent<BaseCube>();
            if (obj.Fraction != ClassEntity.Player) return;
            //Debug.Log($"Через {TheDarkGlobalMechanic.DestroyTime} будет уничтожен {collision.name}!");
            if (obj.AdditionLightZone)
            {
                obj.AdditionLightZone = false;
                return;
            }
            obj.InTheLight = false;
        }
    }


}


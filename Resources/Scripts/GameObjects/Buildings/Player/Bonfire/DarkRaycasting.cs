using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.Image;


public class DarkRaycasting : MonoBehaviour
{
    private float maxDistance; // Максимальная дистанция для луча
    private float lightDistance; // Максимальная дистанция для луча

    private PlayerBonfire _bonFire;
    private Light2D _light;

    public void Init(PlayerBonfire bonFire, Light2D light)
    {
        _bonFire = bonFire;
        _light = light;
    }

    private void FixedUpdate()
    {
        if (_bonFire != null && _light != null)
        {
            lightDistance = _light.pointLightOuterRadius;
            RaycastLightsToPlayerCubes();
        }
    }

    private void RaycastLightsToPlayerCubes()
    {
        foreach (var target in _bonFire.PlayersEntitysForRaycasting)
        {
            var obj = (MonoBehaviour)target;
            if (target != null)
            {
                maxDistance = Vector2.Distance(transform.position, obj.transform.position);

                if (maxDistance > lightDistance)
                {
                    return;
                }

                // Отображаем луч для визуализации (опционально)
                Debug.DrawLine(transform.position, obj.transform.position, Color.red);

                // Получаем центр объекта
                Vector3 targetPosition = obj.transform.position;

                // Определяем направление от текущего объекта к целевому объекту
                Vector2 direction = targetPosition - transform.position;

                // Пускаем луч
                var hit = Physics2D.Raycast(transform.position, direction.normalized, maxDistance, BootStrap.TheDarkGlobalMechanic.ignoreLayers);

                // Проверяем, попал ли луч в целевой объект
                // 

                if (default(RaycastHit2D) == hit)
                {
                    DontDestroyCube(target);
                }
                else
                {
                    DestroyCube(target);
                }



            }
        }
    }

    private void DestroyCube(IDarkRaycastObject obj)
    {
        obj.EnterToDarkWall(_light);
    }

    private void DontDestroyCube(IDarkRaycastObject obj)
    {
        obj.ExitToDarkWall(_light);
    }
}




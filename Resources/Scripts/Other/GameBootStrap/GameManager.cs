
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;




/// <summary>
/// Store main game information 
/// </summary>
public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Light2D> StartLightingPoints = new List<Light2D>();


    public MyList<Light2D> LightingPoints;
    public MyList<BaseCube> PlayerBaseCubes;


    public void Init()
    {
        LightingPoints = new MyList<Light2D>(StartLightingPoints);
        FindPlayerBaseCubeOnScene();
    }


    private void FindPlayerBaseCubeOnScene()
    {
        // Получаем все объекты на сцене
        GameObject[] allMobs = FindObjectsOfType<GameObject>();

        // Список для хранения объектов с нужным слоем
        List<BaseCube> mobs = new List<BaseCube>();

        // Ищем объекты с нужным слоем
        foreach (GameObject mob in allMobs)
        {
            if (mob.layer == LayerMask.NameToLayer("Mobs"))
            {
                var comp = mob.GetComponent<BaseCube>();
                if (comp != null && comp.Fraction == EntityFraction.Player)
                {
                    mobs.Add(comp);
                }

            }
        }

        PlayerBaseCubes = new MyList<BaseCube>(mobs);
    }
}

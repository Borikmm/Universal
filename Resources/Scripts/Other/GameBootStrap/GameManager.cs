
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
        // �������� ��� ������� �� �����
        GameObject[] allMobs = FindObjectsOfType<GameObject>();

        // ������ ��� �������� �������� � ������ �����
        List<BaseCube> mobs = new List<BaseCube>();

        // ���� ������� � ������ �����
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

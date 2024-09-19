
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;




/// <summary>
/// Store main game information 
/// </summary>
public class GameManager : MonoBehaviour
{

    public List<Light2D> LightingPoints = new List<Light2D>();

    public void AddLightingPoint(Light2D light)
    {
        if (!LightingPoints.Contains(light))
        {
            LightingPoints.Add(light);
        }
    }

    public void RemoveLightingPoint(Light2D light)
    {
        if (LightingPoints.Contains(light))
        {
            LightingPoints.Remove(light);
        }
    }


}

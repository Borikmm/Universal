using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Говорит что обьект подвержен свету. 
/// </summary>
public interface ILightedEntity
{
    public List<Light2D> InTheLightList {  get; set; }
    public bool DelObjFromWall {  get; set; }
    public void WhatDoWhenEnterInTheLight();
    public void WhatDoWhenExitFromTheLight();
    public void ExitToDarkWall(Light2D light);
    public void EnterToDarkWall(Light2D light);
}


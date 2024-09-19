using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class FunctionalMethods
{
    public static void ChangeColorSprite(SpriteRenderer sprite, Color color)
    {
        sprite.color = color;
    }


    public static void SetPause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}


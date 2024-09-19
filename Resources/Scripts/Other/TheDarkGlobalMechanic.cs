using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TheDarkGlobalMechanic : MonoBehaviour
{
    [SerializeField] private float destroyTime = 4f; // Время, через которое объект будет уничтожен
    public static float DestroyTime;

    private void Start()
    {
        DestroyTime = destroyTime;
    }
}


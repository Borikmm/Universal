using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : BaseBuilding
{
    [SerializeField] private FreandlyCubeFactory _factory;
    [SerializeField] Transform _spawnPosition;

    [SerializeField] private float _spawnTime;
    private float _spawnTimeRemain;

    private void Spawn()
    {
        var spawnObject = _factory.Get();
        spawnObject.transform.position = _spawnPosition.position;
    }


    protected override void Start()
    {
        base.Start();
        ResetTime();
    }

    private void ResetTime()
    {
        _spawnTimeRemain = _spawnTime;
    }


    protected override void Update()
    {
        _spawnTimeRemain -= Time.deltaTime;
        if (_spawnTimeRemain <= 0f)
        {
            ResetTime();

            if (_isActive)
            {
                Spawn();
            }
        }
    }
}


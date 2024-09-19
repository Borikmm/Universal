using Patterns.GameObjectFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreandlySpawner : BaseBuilding
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

        _fraction = ClassEntity.Player;

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

    protected override void DestroyThisObject()
    {
        BootStrap.GameManager.RemoveLightingPoint(_light);
        base.DestroyThisObject();
    }


    protected override void DeadWhenZeroXP()
    {
        DestroyThisObject();
    }
}

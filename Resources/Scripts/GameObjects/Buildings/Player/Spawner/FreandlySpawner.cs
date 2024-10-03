using Patterns.GameObjectFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreandlySpawner : BaseLightingBuilding, IObjWithLightingCollider
{
    [SerializeField] private FreandlyCubeFactory _factory;
    [SerializeField] Transform _spawnPosition;

    [SerializeField] private float _spawnTime;
    private float _spawnTimeRemain;


    protected Light2DCollision _collisionScript;
    public Light2DCollision CollisionScript { get { return _collisionScript; } set { _collisionScript = value; } }
    private void Spawn()
    {
        var spawnObject = _factory.Get();
        spawnObject.transform.position = _spawnPosition.position;
    }


    protected override void Start()
    {
        base.Start();
        ResetTime();

        _fraction = EntityFraction.Player;

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


    protected override void DeadWhenZeroXP()
    {
        DestroyThisObject();
    }

    public void InitLightingCollider()
    {
        _collisionScript = GetComponentInChildren<Light2DCollision>();
        if (_collisionScript != null) _collisionScript.Init(Light);
    }
}

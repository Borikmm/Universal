using Patterns.GameObjectFactory;
using UnityEngine;



[CreateAssetMenu(fileName = "FreandlySpawner", menuName = "MyAssets/Spawners/FreandlySpawner")]
public class FreandlyCubeFactory : GameObjectFactory
{
    [SerializeField] private BaseCube _cubePrefub;

    public BaseCube Get()
    {
        return Get(_cubePrefub);
    }

    private T Get<T>(T prefub) where T : BaseCube
    {
        return CreateGameObjectInstance(prefub);
    }
}


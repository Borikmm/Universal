using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Patterns.GameObjectFactory
{
    public abstract class GameObjectFactory : ScriptableObject
    {
        protected T CreateGameObjectInstance<T>(T prefub) where T : MonoBehaviour
        {
            T instance = Instantiate(prefub);
            return instance;
        }
    }
}

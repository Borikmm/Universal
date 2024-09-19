using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Patterns.StaticInstance
{


   /// <summary>
   ///  В душе не чаю зачем нужен этот статичный инстанс. Но чел его сделал, и я тоже.
   ///  Предполагаю, чтобы отделить логику создания единственного экземпляра от другой логики.
   /// </summary>
   /// <typeparam name="T"></typeparam>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static StaticInstance<T> Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }

    /// <summary>
    /// Самый обычный синглтон
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : StaticInstance<T> where T: MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Попытка создать еще один singleton с названием: " + this.name + " === на обьекте: " + Instance );
                Destroy(gameObject);
            }
            base.Awake();
        }
    }

    /// <summary>
    /// Делает класс синглтоном, а также создает его только один раз на все сцены. Подходит, если одни и те же сервисы используются
    /// на разных сценах
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ParsistentSingleton<T> : Singleton<T> where T: MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Patterns.StaticInstance
{


   /// <summary>
   ///  � ���� �� ��� ����� ����� ���� ��������� �������. �� ��� ��� ������, � � ����.
   ///  �����������, ����� �������� ������ �������� ������������� ���������� �� ������ ������.
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
    /// ����� ������� ��������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : StaticInstance<T> where T: MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("������� ������� ��� ���� singleton � ���������: " + this.name + " === �� �������: " + Instance );
                Destroy(gameObject);
            }
            base.Awake();
        }
    }

    /// <summary>
    /// ������ ����� ����������, � ����� ������� ��� ������ ���� ��� �� ��� �����. ��������, ���� ���� � �� �� ������� ������������
    /// �� ������ ������
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

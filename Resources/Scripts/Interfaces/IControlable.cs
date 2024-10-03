
using UnityEngine;
/// <summary>
/// Интерфейс который говорит, что обьектом можно управлять: выделять и убирать выделение
/// </summary>
public interface IControlable
{
    void Select();
    void UnSelect();
}


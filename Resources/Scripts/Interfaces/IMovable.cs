
using UnityEngine;
/// <summary>
/// Интерфейс который содержит скорость обьекта и говорит, что он может двигаться
/// </summary>
public interface IMovable
{
    public float Speed { get; set; }
    public void CollisionWithPlayer(GameObject gameObject);
    public void CollisionWithEnemy(GameObject gameObject);
    void GoTo(Transform target);
    void GoTo(Vector3 target);
    void Stop();
}


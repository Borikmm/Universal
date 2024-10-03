
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurel : BaseBuilding
{
    [Header("Turel parametres:")]
    public float radius = 5f;            // Радиус поражения
    public float fireRate = 1f;          // Скорость стрельбы (выстрелов в секунду)
    public GameObject bulletPrefab;      // Префаб пули
    public Transform firePoint;          // Точка, из которой выпускаются пули
    public EntityFraction _targetFraction;

    private List<Transform> targets = new List<Transform>();  // Список целей в радиусе
    private Transform currentTarget;      // Текущая цель
    private float fireCountdown = 0f;     // Таймер до следующего выстрела

    protected override void Start()
    {
        StartShoutRadiusColliderScript();
    }


    private void StartShoutRadiusColliderScript()
    {
        // Тут берется общий колайдер. На разновидности турели, например на турели игрока вешается скрипт PlayerTurelShoutRadiusCollider и берется именно он
        var _radiusScript = GetComponentInChildren<TurelShoutRadiusCollider>();
        _radiusScript.Init(this, _targetFraction);
    }

    protected override void Update()
    {
        if (!_isActive)
        {
            return;
        }


        base.Update();


        // Если есть цель и таймер до следующего выстрела закончен
        if (currentTarget != null && fireCountdown <= 0f)
        {

            Shoot();
            fireCountdown = 1f / fireRate; // Сбросим таймер в зависимости от скорости стрельбы
        }

        // Уменьшаем таймер до следующего выстрела
        fireCountdown -= Time.deltaTime;

        // Если текущая цель вышла за пределы зоны поражения или уничтожена, выбираем новую
        if (currentTarget == null || !targets.Contains(currentTarget))
        {
            currentTarget = GetNearestTarget();
        }
    }

    // Стреляем пулей
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Создаем пулю в позиции firePoint и с направлением на цель
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Init(currentTarget);
            bulletScript.TargetEntityFraction = _targetFraction;
            // Можно добавить сюда логику направления пули на цель
            // bullet.GetComponent<Bullet>().SetTarget(currentTarget);
        }
    }

    // Метод для получения ближайшей цели
    Transform GetNearestTarget()
    {
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            if (target == null) continue;

            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }

    // Метод, вызываемый при входе объекта в радиус действия турели
    public void AddTarget(Transform Target)
    {
        targets.Add(Target);
    }

    // Метод, вызываемый при выходе объекта из радиуса действия турели
    public void RemoveTarget(Transform Target)
    {
        // Убираем цель, если она выходит за радиус действия
        if (targets.Contains(Target))
        {
            targets.Remove(Target);
        }
    }
}


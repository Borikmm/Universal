using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SelectingService : IEventReceiver<RightMouseButtonDown>, IEventReceiver<RightMouseButtonUp>
{
    private GameObject _spawnedSqure;
    private SelectingSquare _spawnedSqureScript;

    public List<Collider2D> _selectedObjects = new();

    private float _spacing; // Расстояние между агентами

    public SelectingService(GameObject squarePrefub, float agentsSpacing) 
    {
        _spacing = agentsSpacing;
        _spawnedSqure = squarePrefub;
        _spawnedSqureScript = _spawnedSqure.GetComponent<SelectingSquare>();
        BootStrap.EventBus.Register(this as IEventReceiver<RightMouseButtonDown>);
        BootStrap.EventBus.Register(this as IEventReceiver<RightMouseButtonUp>);
    }

    public void StartSelecting()
    {
        _spawnedSqureScript.StartMove();
        CancelSelectedObjects();
    }

    public void CancelSelectedObjects()
    {
        RecolorObjects(Color.white);
    }


    public List<Collider2D> GetSelectedObjects()
    {
        return _selectedObjects;
    }

    public void RecolorObjects(Color color)
    {
        var result = color == Color.white ? 1 : 0;

        foreach (var obj in _selectedObjects)
        {
            if (result == 1)
            {
                obj.GetComponentInParent<IControlable>().UnSelect();
            }
            else
            {
                obj.GetComponentInParent<IControlable>().Select();
            }
            
        }
    }

    public void StopSelecting()
    {
        if (_spawnedSqure != null)
        {
            _selectedObjects = new List<Collider2D>(_spawnedSqureScript.GetSelectedObjects());
            _spawnedSqureScript.StopMove();
        }
    }


    public void RemoveSelectedObject(Collider2D obj)
    {
        if (obj == null) return;
        if (!_selectedObjects.Contains(obj)) return;
        _selectedObjects.Remove(obj);
    }

    public void RemoveSelectedObject(GameObject obj)
    {
        if (obj == null) return;
        var col = obj.GetComponentInChildren<Collider2D>();
        if (!_selectedObjects.Contains(col)) return;
        _selectedObjects.Remove(col);
    }

    public void OnEvent(RightMouseButtonDown @event)
    {
        if (_selectedObjects.Count <= 0) return;
        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (var obj in _selectedObjects)
        {
            obj.GetComponentInParent<IMovable>().GoTo(target);
        }


        MoveGroup(target, _selectedObjects);
    }

    public void OnEvent(RightMouseButtonUp @event)
    {
        if (_selectedObjects.Count <= 0) return;

        var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveGroup(target, _selectedObjects);

        foreach (var obj in _selectedObjects)
        {
            obj.GetComponentInParent<IMovable>().Stop();
        }
    }


    public void SelectedOneObject(Collider2D gameObject)
    {
        RecolorObjects(Color.white);
        _selectedObjects.Clear();
        _selectedObjects.Add(gameObject);
        RecolorObjects(Color.green);
    }


    public void MoveGroup(Vector3 target, List<Collider2D> agents)
    {
        // Сортируем юнитов по их текущему расстоянию до целевой точки
        var sortedAgents = agents.OrderBy(agent => Vector3.Distance(agent.transform.position, target)).ToList();

        // Рассчитываем количество юнитов в ряду
        int agentsPerRow = Mathf.CeilToInt(Mathf.Sqrt(sortedAgents.Count));

        // Назначаем каждому агенту новую позицию
        for (int i = 0; i < sortedAgents.Count; i++)
        {
            // Вычисляем смещённые позиции для каждого агента
            float xOffset = (i % agentsPerRow) * _spacing;
            float zOffset = (i / agentsPerRow) * _spacing;

            // Смещаем точку назначения для каждого агента относительно цели
            Vector3 newPosition = target + new Vector3(xOffset, zOffset, 0);
            agents[i].GetComponentInParent<IMovable>().GoTo(newPosition);
        }
    }
}


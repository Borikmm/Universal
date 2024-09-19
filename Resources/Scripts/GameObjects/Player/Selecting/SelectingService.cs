using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SelectingService : IEventReceiver<RightMouseButtonDown>, IEventReceiver<RightMouseButtonUp>
{
    private GameObject _spawnedSqure;
    private SelectingSquare _spawnedSqureScript;

    private List<Collider2D> _selectedObjects = new();

    public SelectingService(GameObject squarePrefub) 
    {
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
        foreach (var obj in _selectedObjects)
        {
            obj.GetComponent<BaseCube>().RecolorThis(color);
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
        var col = obj.GetComponent<Collider2D>();
        if (!_selectedObjects.Contains(col)) return;
        _selectedObjects.Remove(col);
    }

    public void OnEvent(RightMouseButtonDown @event)
    {
        if (_selectedObjects.Count <= 0) return;
        
        foreach (var obj in _selectedObjects)
        {
            obj.GetComponent<BaseCube>().GoTo();
        }
    }

    public void OnEvent(RightMouseButtonUp @event)
    {
        if (_selectedObjects.Count <= 0) return;

        foreach (var obj in _selectedObjects)
        {
            obj.GetComponent<BaseCube>().StopGoTo();
        }
    }


    public void SelectedOneObject(Collider2D gameObject)
    {
        RecolorObjects(Color.white);
        _selectedObjects.Clear();
        _selectedObjects.Add(gameObject);
        RecolorObjects(Color.green);
    }
}


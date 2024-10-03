using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] float _agentsSpacing = 5f;


    private SelectingService _selectingService;

    public SelectingService SelectingService => _selectingService;
    

    public void StartSelectingBackground()
    {
        if (_selectingService != null)
            _selectingService.StartSelecting();
    }

    public void StopSelectingBackground()
    {
        if (_selectingService != null) 
            _selectingService.StopSelecting();
    }

    private void CancelSelectedObjects()
    {
        if (_selectingService != null)
            _selectingService.CancelSelectedObjects();
    }


    public void Init(GameObject selectingsquarePrefub)
    {
        _selectingService = new SelectingService(selectingsquarePrefub, _agentsSpacing);
    }



    private GameObject CheckTap()
    {
        // Получаем позицию курсора мыши в мировых координатах
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Получаем все коллайдеры под курсором
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePosition);
        
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == 6 &&  hit.gameObject.GetComponentInParent<IControlable>() != null)
            {
                return hit.gameObject;
            }
        }

        return null;
        
    }


    void Update()
    {
        CheckInput();
    }



    private void CheckInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopSelectingBackground();

            if (_selectingService.GetSelectedObjects().Count <= 1)
            {
                var a = CheckTap();
                if (a != null)
                {
                    _selectingService.SelectedOneObject(a.GetComponent<Collider2D>());
                    return;
                }
            }
            _selectingService.RecolorObjects(Color.green);
        }


        if (Input.GetMouseButtonDown(0))
        {
            StartSelectingBackground();
        }


        if (Input.GetMouseButtonDown(1))
        {
            BootStrap.EventBus.Raise(new RightMouseButtonDown());
        }

        if (Input.GetMouseButtonUp(1))
        {

            BootStrap.EventBus.Raise(new RightMouseButtonUp());
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            FunctionalMethods.SetPause();
        }
    }
}


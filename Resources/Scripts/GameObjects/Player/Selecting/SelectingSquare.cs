using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectingSquare : MonoBehaviour
{

    private BoxCollider2D _box;
    private Vector3 startMousePosition;
    private List<Collider2D> _selectedObjects = new(); 
    private bool _isMoving;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer != 6) return;
        var f = collision.gameObject.GetComponent<BaseCube>();
        if (f.Fraction != ClassEntity.Player) return;
        if (!_selectedObjects.Contains(collision))
        {
            collision.gameObject.GetComponent<BaseCube>().RecolorThis(Color.green);
            _selectedObjects.Add(collision);
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_selectedObjects.Contains(collision))
        {
            collision.gameObject.GetComponent<BaseCube>().RecolorThis(Color.white);
            _selectedObjects.Remove(collision);
        }    
    }



    private void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        UpdateValues();
    }

    private void UpdateValues()
    {
        _box = GetComponent<BoxCollider2D>();
        // Обновляем размер BoxCollider2D
        _box.size = new Vector2(1, 1);
        // Получаем позицию мыши в мировых координатах
        startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startMousePosition.z = 0; // Устанавливаем Z в 0 для 2D

        transform.position = startMousePosition;
        transform.localScale = Vector3.zero;
    }


    private void Update()
    {
        if (_isMoving)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0; // Устанавливаем Z в 0 для 2D

            // Вычисляем размер выделяющего квадрата
            Vector3 size = new Vector3(currentMousePosition.x - startMousePosition.x,
                                        currentMousePosition.y - startMousePosition.y,
                                        1);

            // Обновляем размер выделяющего квадрата
            transform.localScale = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), 1);

            // Устанавливаем позицию квадрата в центр выделения
            transform.position = startMousePosition + size / 2;
        }
    }

    public List<Collider2D> GetSelectedObjects()
    {
        return _selectedObjects;
    }


    public void StopMove()
    {
        _selectedObjects.Clear();
        UpdateValues();
        _isMoving = false;
    }

    public void StartMove()
    {
        UpdateValues();
        _isMoving = true;
    }
}

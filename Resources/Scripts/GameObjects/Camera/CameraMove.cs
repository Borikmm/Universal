using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    [Header("Zoom settings")]
    [SerializeField] private uint _zoomSpeed = 1;
    [SerializeField] private uint _zoomMax = 25;
    [SerializeField] private uint _zoomMin = 0;
    [Header("------------")]
    [Header("Movement settings")]
    [SerializeField] private float _maxSpeed = 5f; // Максимальная скорость движения камеры
    [SerializeField] private float _acceleration = 2f; // Ускорение камеры
    [SerializeField] private float _deceleration = 2f; // Замедление камеры
    [SerializeField] private float _borderThickness = 30f; // Размер зоны у границ экрана для срабатывания движения

    private Vector3 velocity = Vector3.zero; // Текущая скорость камеры
    private Vector2 direction = Vector2.zero; // Текущее направление движения камеры

    private Camera cam;



    // Zoom math parametres
    private float _startAcceleration;
    private float _startDeceleration;
    private float _startMaxSpeed;

    void Start()
    {
        cam = Camera.main; // Получаем ссылку на основную камеру

        _startAcceleration = _acceleration;
        _startDeceleration = _deceleration;
        _startMaxSpeed = _maxSpeed;
    }

    private void CameraScrolling()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if ((Camera.main.orthographicSize - (Input.mouseScrollDelta.y * _zoomSpeed)) > _zoomMin && (Camera.main.orthographicSize - (Input.mouseScrollDelta.y * _zoomSpeed)) < _zoomMax)
            {
                Camera.main.orthographicSize -= Input.mouseScrollDelta.y * _zoomSpeed;

                _maxSpeed -= (Input.mouseScrollDelta.y) * (_zoomSpeed + 1); // speed changed
                //                                                      ^ scale modificator


                _acceleration = (_startAcceleration * _maxSpeed) / _startMaxSpeed;
                _deceleration = (_startDeceleration * _maxSpeed) / _startMaxSpeed;

                SpeedsControl();

            }
        }
    }

    private void SpeedsControl()
    {
        if (_acceleration < _startAcceleration)
        {
            _acceleration = _startAcceleration;
        }

        if (_deceleration < _startDeceleration)
        {
            _deceleration = _startDeceleration;
        }

        if (_maxSpeed < 4)
        {
            _maxSpeed = 4;
        }

    }

    private void CheckMouse()
    {
        // Получаем положение мыши на экране
        Vector3 mousePosition = Input.mousePosition;

        // Проверяем положение мыши относительно границ экрана
        direction = Vector2.zero;

        // Проверка верхней границы
        if (mousePosition.y >= Screen.height - _borderThickness)
        {
            direction.y = 1;
        }
        // Проверка нижней границы
        else if (mousePosition.y <= _borderThickness)
        {
            direction.y = -1;
        }

        // Проверка правой границы
        if (mousePosition.x >= Screen.width - _borderThickness)
        {
            direction.x = 1;
        }
        // Проверка левой границы
        else if (mousePosition.x <= _borderThickness)
        {
            direction.x = -1;
        }
    }

    private void Move()
    {
        // Если есть направление движения
        if (direction != Vector2.zero)
        {
            // Плавное увеличение скорости до максимальной
            velocity = Vector3.Lerp(velocity, direction * _maxSpeed, _acceleration * Time.deltaTime);
        }
        else
        {
            // Плавное замедление до полной остановки
            velocity = Vector3.Lerp(velocity, Vector3.zero, _deceleration * Time.deltaTime);
        }

        // Перемещаем камеру
        cam.transform.position += velocity * Time.deltaTime;
    }

    void Update()
    {
        CameraScrolling();

        CheckMouse();

        Move();
    }
}


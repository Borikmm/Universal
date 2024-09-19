using System.Collections;
using System.Collections.Generic;
using Patterns.EventBus;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefub;
    [SerializeField] GameObject _selectingSquarePrefub;

    

    private PlayerInput _playerInput;

    public static PlayerInput PlayerInput;

    public static EventBus EventBus;

    public static TheDarkGlobalMechanic TheDarkGlobalMechanic;

    public static GameManager GameManager;

    public void StartGame()
    {
        CreateEventBus();
        CreatePlayer();
        CreateServices();
    }



    private void CreateServices()
    {
        TheDarkGlobalMechanic = GetComponentInChildren<TheDarkGlobalMechanic>();
        GameManager = GetComponentInChildren<GameManager>();
    }

    private void CreatePlayer()
    {
        var player = Instantiate(_playerPrefub);
        _playerInput = player.GetComponent<PlayerInput>();
        _playerInput.Init(CreateSelectingSquare());
        PlayerInput = _playerInput;
    }

    private GameObject CreateSelectingSquare()
    {
        return GameObject.Instantiate(_selectingSquarePrefub, Input.mousePosition, Quaternion.identity);
    }


    private void CreateEventBus()
    {
        EventBus = new EventBus();
    }

    void Awake()
    {
        StartGame();
    }
}

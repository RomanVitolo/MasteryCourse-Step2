using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    public int PlayerNumber { get { return playerNumber; } }
    public Controller Controller { get; private set; }
    public bool HasController { get { return Controller != null; } }
    //public bool HasController => _controller != null;  // Good Practice 
    
    private PlayerUI _uiPlayerText;
    
    private void Awake()
    {
        _uiPlayerText = GetComponentInChildren<PlayerUI>();
    }

    public void InitializeController(Controller controller)
    { 
        Controller = controller;
        //gameObject.name = "Player - " + _controller.gameObject.name;
        
        gameObject.name = string.Format("Player {0} - {1}", playerNumber, controller.gameObject.name); // More performance
        //gameObject.name = $"Player {playerNumber} - {controller.gameObject.name}"; 

        _uiPlayerText.HandlePlayerInitialized();
    }
}

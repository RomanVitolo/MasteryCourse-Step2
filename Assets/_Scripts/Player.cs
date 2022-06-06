using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller _controller;
    //public bool HasController => _controller != null;  // Good Practice 
    public bool HasController { get { return _controller != null; } }
    
    [SerializeField] private int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }

    public void InitializeController(Controller controller)
    { 
        _controller = controller;
        //gameObject.name = "Player - " + _controller.gameObject.name;
        
        gameObject.name = string.Format("Player {0} - {1}", playerNumber, controller.gameObject.name);
        //gameObject.name = $"Player {playerNumber} - {controller.gameObject.name}"; More performance
    }
}

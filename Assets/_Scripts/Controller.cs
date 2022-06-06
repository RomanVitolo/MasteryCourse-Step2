using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private bool attack;
    
    [field: SerializeField] public int Index { get; private set; }
    public bool IsAssigned { get; set; }
    public bool AttackPressed { get; set; }
    public string horizontalAxis  { get; set; }
    public string verticalAxis  { get; set; }
    
    public float horizontal { get; set; }
    public float vertical { get; set; }

    private string attackButton;

    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
        {
            attack = Input.GetButton(attackButton);
            AttackPressed = Input.GetButtonDown(attackButton);
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        horizontalAxis = "Horizontal" + Index; 
        verticalAxis = "Vertical" + Index;
        gameObject.name = "Controller" + Index;
    }

    public bool PressingAnyButton()
    {
        return attack;
    }
}

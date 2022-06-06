using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [field: SerializeField] public int Index { get; private set; }
    public bool IsAssigned { get; set; }

    [SerializeField] private bool attack;

    private string attackButton;
    
    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
        {
            attack = Input.GetButton(attackButton);
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        gameObject.name = "Controller" + Index;
    }

    public bool PressingAnyButton()
    {
        return attack;
    }
}

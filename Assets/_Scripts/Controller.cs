using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [field: SerializeField] public int Index { get; private set; }

    [SerializeField] private bool attack;

    private string attackButton;
    void Start()
    {
        attackButton = "Attack" + Index;
    }

    private void Update()
    {
        attack = Input.GetButton(attackButton);
    }
}

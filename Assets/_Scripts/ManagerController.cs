using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerController : MonoBehaviour
{
    private List<Controller> _controllers;

    private void Awake()
    {
        _controllers = FindObjectsOfType<Controller>().ToList();
    }
}

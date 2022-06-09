using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] private float forceAmount = 10f;
    
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void TakeHit(Character hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);
        //direction.Normalize();
        
        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private int maxHealth = 100;
    
    private Animator _animator;
    private int currentHealth;
    

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeHit(Character hitBy)
    {
        if (currentHealth <= 0 )
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("Hit");
            currentHealth -= 10;
            Instantiate(impactParticle, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
        Destroy(gameObject, 5);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Character : MonoBehaviour, ITakeHit
{
    public static List<Character> All = new List<Character>();
    
    [SerializeField] private CharacterSetupSO _characterSetupSo;
    [SerializeField] private float moveSpeed = 5f;

    private Controller _controller;
    private Animator _animator;
    private Attacker _attacker;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _attacker = GetComponent<Attacker>();

    }

    private void OnEnable()
    {
        _characterSetupSo.CurrentHealth = _characterSetupSo.MaxHealth;
        if (All.Contains(this) == false) All.Add(this);
    }

    private void OnDisable()
    {
        if(All.Contains(this)) All.Remove(this);
    }

    private void Update()
    {
        Vector3 direction = _controller.GetDirection();
        if (direction.magnitude > 0.25f)
        {
            transform.position += direction * (moveSpeed * Time.deltaTime);
            transform.forward = direction * 360f;
            _animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
        
        if (_controller.AttackPressed)
        {
            if (_attacker.CanAttack)
            {
                _animator.SetTrigger("Attack");
            }
        }
    }
    
    public void SetController(Controller controller)
    {
        _controller = controller;
    }
    
    public void TakeHit(IAttack hitBy)
    {
        if (_characterSetupSo.CurrentHealth >= 0)
        {
            _characterSetupSo.CurrentHealth -= hitBy.Damage;
        }
        else
        {
            Debug.Log("DIE");
        }
    }
}

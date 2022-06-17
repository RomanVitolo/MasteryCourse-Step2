using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, ITakeHit
{
    public bool EnemyDie { get; private set; }
    
    [SerializeField] private GameObject impactParticle;
    [SerializeField] private Character _target;
    [SerializeField] private EnemySetupSO _enemySetupSo;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Attacker _attacker;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_collider = GetComponent<CapsuleCollider>();
        _target = FindObjectOfType<Character>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _enemySetupSo.CurrentHealth = _enemySetupSo.MaxHealth;
    }
   
    private void Update()
    {
        if (EnemyDie) return;
        
        if (_target == null)
        {
            AquireTarget();
        }
        else
        {
            var distance = Vector3.Distance(transform.position, _target.transform.position);
            if (distance > 2f)
            {
                ChasingTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    private void AquireTarget()
    {
        _target = Character.All.OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
        _animator.SetFloat("Speed", 0f);
    }

    private void TryAttack()
    {
        _animator.SetFloat("Speed", 0f);
        _navMeshAgent.isStopped = true;
        if (_attacker.CanAttack)
        {
            _animator.SetTrigger("Attack");
            _attacker.Attack(_target);
        }
    }

    private void ChasingTarget()
    {
        if (EnemyDie == false)
        {
            _animator.SetFloat("Speed", 1f);
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_target.transform.position);
        }
    }
    
    public void TakeHit(IAttack hitBy)
    {
        if(_enemySetupSo.CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            _animator.SetTrigger("Hit");
            _enemySetupSo.CurrentHealth -= 10;
            Instantiate(impactParticle, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
        }
    }

    public void Die()
    {
        EnemyDie = true;
        _navMeshAgent.isStopped = true;
        _animator.SetTrigger("Die");
        Destroy(gameObject, 5);
    }
}

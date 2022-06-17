using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    public int Damage { get { return damage; } }
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }
    
    [SerializeField] private float attackRefreshSpeed = 1.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackOffset = 1f;
    [SerializeField] private float attackRadius = 1f;
   
    private float attackTimer;
    private AnimationImpactWatcher _animationImpactWatcher;
    private Collider[] _attackResults;

    private void Awake()
    {
        _animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (_animationImpactWatcher != null)
        {
            _animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
        }
        _attackResults = new Collider[10];
    }

    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        target.TakeHit(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
    
    //Call by animation event via AnimationImpactWatcher
    private void AnimationImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, _attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = _attackResults[i].GetComponent<ITakeHit>();
            //_attackResults[i].TryGetComponent(out _box);
            if (takeHit != null)
            {
                takeHit.TakeHit(this);
            }
        }
    }
}

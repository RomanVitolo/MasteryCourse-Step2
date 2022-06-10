using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackOffset = 1f;
    [SerializeField] private float attackRadius = 1f;
    
    private Controller _controller;
    private Animator _animator;
    private Collider[] _attackResults;
    //private Box _box;
    private AnimationImpactWatcher _animationImpactWatcher;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _attackResults = new Collider[10];
        //_box = FindObjectOfType<Box>();

        _animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        _animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
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
            _animator.SetTrigger("Attack");
        }
    }
    
    public void SetController(Controller controller)
    {
        _controller = controller;
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

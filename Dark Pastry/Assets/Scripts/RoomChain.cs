using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChain : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _chainCollider;
    private static readonly int EnemyLockdown = Animator.StringToHash("enemyLockdown");
    private static readonly int EnemyUnlock = Animator.StringToHash("enemyUnlock");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _chainCollider = GetComponent<BoxCollider2D>();
    }

    public void Enable()
    {
        _animator.SetTrigger(EnemyLockdown);
        _chainCollider.enabled = true;
    }

    public void Disable()
    {
        _animator.SetTrigger(EnemyUnlock);
        _chainCollider.enabled = false;
    }
}

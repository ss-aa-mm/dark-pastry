using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChain : MonoBehaviour
{
    private Animator _animator;
    private static readonly int KeyLocked = Animator.StringToHash("keyLocked");
    private static readonly int KeyUnlock = Animator.StringToHash("keyUnlock");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(KeyLocked);
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lock : HighlightableObject
{
    protected static MonoBehaviour Instance;
    protected Animator Animator;
    protected BoxCollider2D ChainCollider;
    private static readonly int KeyUnlock = Animator.StringToHash("keyUnlock");

    public override void Interact()
    {
        if (AgataNew.GetItem() == DropItems.Key)
        {
            AgataNew.SetItem(DropItems.None);
            Animator.SetTrigger(KeyUnlock);
            ChainCollider.enabled = false;
            Destroy(Animator.gameObject.GetComponent<LockChain>());
        }
        else
        {
            Instance.StartCoroutine(WrongAnimation());
        }
    }

}

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
    public AudioClip key;
    public AudioClip chain;

    public override void Interact()
    {
        if (AgataNew.GetItem() == DropItems.Key)
        {
            AgataNew.SetItem(DropItems.None);
            Animator.SetTrigger(KeyUnlock);
            ChainCollider.enabled = false;
            Destroy(Animator.gameObject.GetComponent<LockChain>());
            SoundManager.Instance.PlaySingle(key);
            SoundManager.Instance.PlaySingle(chain);
            
        }
        else
        {
            Instance.StartCoroutine(WrongAnimation());
        }
    }

}

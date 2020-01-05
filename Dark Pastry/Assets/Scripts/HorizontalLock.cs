using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLock : Lock
{
    protected override void AssignReferences()
    {
        Instance = this;
        HighlightedSprite = Resources.Load<Sprite>("Lock/SelectedH");
        NormalSprite = Resources.Load<Sprite>("Lock/NormalH");
        WrongSprite = Resources.Load<Sprite>("Lock/WrongH");
        Animator = GetComponentInParent<Animator>();
        ChainCollider = GetComponentInParent<BoxCollider2D>();
    }
}

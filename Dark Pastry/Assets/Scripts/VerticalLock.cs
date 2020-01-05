using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLock : Lock
{
    protected override void AssignReferences()
    {
        Instance = this;
        HighlightedSprite = Resources.Load<Sprite>("Lock/SelectedV");
        NormalSprite = Resources.Load<Sprite>("Lock/NormalV");
        WrongSprite = Resources.Load<Sprite>("Lock/WrongV");
        Animator = GetComponentInParent<Animator>();
        ChainCollider = GetComponentInParent<BoxCollider2D>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : HighlightableObject
{
    private string _hint;
    public override void Interact()
    {
        Debug.Log(_hint);
        Destroy(gameObject);
    }

    protected override void AssignReferences()
    {
        NormalSprite = Resources.Load<Sprite>("Page/Normal");
        HighlightedSprite = Resources.Load<Sprite>("Page/Selected");
    }

    private void Start()
    {
        _hint = LevelData.AssignHint();
    }
}

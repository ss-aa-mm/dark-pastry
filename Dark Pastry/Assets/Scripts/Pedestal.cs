using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pedestal : HighlightableObject
{

    private SpriteRenderer _objectRenderer;
    private DropItems _object;
    private static Dictionary<DropItems, Sprite> _pedestalSprites;
    
    public override void Interact()
    {
        var agataObject = AgataNew.GetItem();
        if(agataObject==DropItems.None && _object==DropItems.None) return;
        if(agataObject!=DropItems.None && _object!=DropItems.None) return;
        _objectRenderer.sprite = _pedestalSprites[agataObject];
        AgataNew.SetItem(_object);
        _object = agataObject;
        if(agataObject!=DropItems.None)
            Pentagram.ObjectAdded();
        else
        {
            Pentagram.ObjectRemoved();
        }
    }

    protected override void AssignReferences()
    {
        HighlightedSprite = Resources.Load<Sprite>("Pedestal/Selected");
        NormalSprite = Resources.Load<Sprite>("Pedestal/Normal");
        WrongSprite = Resources.Load<Sprite>("Pedestal/Wrong");
        _objectRenderer = GetComponentsInChildren<SpriteRenderer>().ToList().Find(go => go.CompareTag("PedestalPlaceholder"));
        _object = DropItems.None;
        _pedestalSprites = new Dictionary<DropItems, Sprite>();
        InitializeDictionary();
    }

    private static void InitializeDictionary()
    {
        _pedestalSprites.Add(DropItems.None,null);
        _pedestalSprites.Add(DropItems.Egg, Resources.Load<Sprite>("PedestalItems/Egg"));
    }

    public DropItems GetObject()
    {
        return _object;
    }
    
}

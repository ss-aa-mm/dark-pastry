using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tablet : HighlightableObject
{

    private SpriteRenderer _objectRenderer;
    private DropItems _object;
    private static Dictionary<DropItems, Sprite> _pedestalSprites;
    private static MonoBehaviour _instance;
    
    public override void Interact()
    {
        var agataObject = AgataNew.GetItem();
        if (agataObject == DropItems.None && _object == DropItems.None || agataObject!=DropItems.None && _object!=DropItems.None || !agataObject.CanBePlaced())
        {
            _instance.StartCoroutine(WrongAnimation());
            return;
        }
        _objectRenderer.sprite = _pedestalSprites[agataObject];
        AgataNew.SetItem(_object);
        _object = agataObject;
    }

    protected override void AssignReferences()
    {
        HighlightedSprite = Resources.Load<Sprite>("Table/Selected");
        NormalSprite = Resources.Load<Sprite>("Table/Normal");
        WrongSprite = Resources.Load<Sprite>("Table/Wrong");
        _objectRenderer = GetComponentsInChildren<SpriteRenderer>().ToList().Find(go => go.CompareTag("PedestalPlaceholder"));
        _object = DropItems.None;
        _instance = this;
        _pedestalSprites = new Dictionary<DropItems, Sprite>();
        InitializeDictionary();
    }
    
    private static void InitializeDictionary()
    {
        _pedestalSprites.Add(DropItems.None,null);
        _pedestalSprites.Add(DropItems.Egg, Resources.Load<Sprite>("PedestalItems/Egg"));
        _pedestalSprites.Add(DropItems.Biscuit, Resources.Load<Sprite>("PedestalItems/Biscuit"));
        _pedestalSprites.Add(DropItems.Blueberry, Resources.Load<Sprite>("PedestalItems/Blueberry"));
        _pedestalSprites.Add(DropItems.Butter, Resources.Load<Sprite>("PedestalItems/Butter"));
        _pedestalSprites.Add(DropItems.Carrot, Resources.Load<Sprite>("PedestalItems/Carrot"));
        _pedestalSprites.Add(DropItems.Cheese, Resources.Load<Sprite>("PedestalItems/Cheese"));
        _pedestalSprites.Add(DropItems.Cherry, Resources.Load<Sprite>("PedestalItems/Cherry"));
        _pedestalSprites.Add(DropItems.Chocolate, Resources.Load<Sprite>("PedestalItems/Chocolate"));
        _pedestalSprites.Add(DropItems.Coffee, Resources.Load<Sprite>("PedestalItems/Coffee"));
        _pedestalSprites.Add(DropItems.Flour, Resources.Load<Sprite>("PedestalItems/Flour"));
        _pedestalSprites.Add(DropItems.Jellybean, Resources.Load<Sprite>("PedestalItems/Jellybean"));
        _pedestalSprites.Add(DropItems.Ladyfinger, Resources.Load<Sprite>("PedestalItems/Ladyfinger"));
        _pedestalSprites.Add(DropItems.Lemon, Resources.Load<Sprite>("PedestalItems/Lemon"));
        _pedestalSprites.Add(DropItems.Milk, Resources.Load<Sprite>("PedestalItems/Milk"));
        _pedestalSprites.Add(DropItems.Raspberry, Resources.Load<Sprite>("PedestalItems/Raspberry"));
        _pedestalSprites.Add(DropItems.Sugar, Resources.Load<Sprite>("PedestalItems/Sugar"));
        _pedestalSprites.Add(DropItems.WhippedCream, Resources.Load<Sprite>("PedestalItems/WhippedCream"));
        _pedestalSprites.Add(DropItems.Dough, Resources.Load<Sprite>("PedestalItems/Dough"));
        _pedestalSprites.Add(DropItems.Blood, Resources.Load<Sprite>("PedestalItems/Blood"));
    }
}

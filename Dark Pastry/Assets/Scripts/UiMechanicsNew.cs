using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public enum DropItems
{
    None,
    Egg,
    Key,
    WhippedCream,
    Biscuit,
    Lemon,
    Almond,
    Apple,
    Coconut,
    Milk,
    Sugar,
    Butter,
    Ladyfinger,
    Cherry,
    Carrot,
    Flour,
    Chocolate,
    Jellybean,
    Coffee,
    Cheese,
    Blueberry,
    Raspberry,
    Blood,
    Dough
}

public static class Extensions
{
    public static bool CanBePlaced (this DropItems item)
    {
        return item != DropItems.Key;
    }
    
}

public class UiMechanicsNew : MonoBehaviour
{
    private static List<Image> _lifeBar;
    private static Image _pocketItem;
    private static TextMeshProUGUI _itemLabel;
    
    private static Sprite _fullHeart;
    private static Sprite _halfHeart;
    private static Sprite _emptyHeart;
    private static Dictionary<DropItems, Sprite> _itemsSprites;

    private void Awake()
    {
        _lifeBar = GetComponentsInChildren<Image>().ToList();
        _pocketItem = _lifeBar.Find(img => img.CompareTag("UIItem"));
        _lifeBar.RemoveAll(img => !img.CompareTag("UIHeart"));
        _itemLabel = GetComponentInChildren<TextMeshProUGUI>();
        
        
        _fullHeart = Resources.Load<Sprite>("HealthBar/Full");
        _halfHeart = Resources.Load<Sprite>("HealthBar/Half");
        _emptyHeart = Resources.Load<Sprite>("HealthBar/Empty");

        _itemsSprites = new Dictionary<DropItems, Sprite>();
        InitializeDictionary();
    }

    private void Start()
    {
        UpdatePocket();
        
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdatePocket();
    }

    private static void UpdateHealthBar()
    {
        var life = AgataNew.GetLife();
        foreach (var heart in _lifeBar)
        {
            if (life >= 1f)
            {
                heart.sprite = _fullHeart;
                life--;
            }
            else if (life >= 0.5f)
            {
                heart.sprite = _halfHeart;
                life -= 0.5f;
            }
            else
            {
                heart.sprite = _emptyHeart;
            }
        }
    }

    private static void UpdatePocket()
    {
        var agataItem = AgataNew.GetItem();
        _pocketItem.sprite = _itemsSprites[agataItem];
        _itemLabel.text = agataItem.ToString();

    }

    private static void InitializeDictionary()
    {
        _itemsSprites.Add(DropItems.None,null);
        _itemsSprites.Add(DropItems.Egg, Resources.Load<Sprite>("PocketItems/Egg"));
        _itemsSprites.Add(DropItems.Key, Resources.Load<Sprite>("PocketItems/Key"));
        _itemsSprites.Add(DropItems.Biscuit, Resources.Load<Sprite>("PocketItems/Biscuit"));
        _itemsSprites.Add(DropItems.Blueberry,Resources.Load<Sprite>("PocketItems/Blueberry"));
        _itemsSprites.Add(DropItems.Butter,Resources.Load<Sprite>("PocketItems/Butter"));
        _itemsSprites.Add(DropItems.Carrot,Resources.Load<Sprite>("PocketItems/Carrot"));
        _itemsSprites.Add(DropItems.Cheese,Resources.Load<Sprite>("PocketItems/Cheese"));
        _itemsSprites.Add(DropItems.Cherry,Resources.Load<Sprite>("PocketItems/Cherry"));
        _itemsSprites.Add(DropItems.Chocolate,Resources.Load<Sprite>("PocketItems/Chocolate"));
        _itemsSprites.Add(DropItems.Coffee,Resources.Load<Sprite>("PocketItems/Coffee"));
        _itemsSprites.Add(DropItems.Flour,Resources.Load<Sprite>("PocketItems/Flour"));
        _itemsSprites.Add(DropItems.Jellybean,Resources.Load<Sprite>("PocketItems/Jellybean"));
        _itemsSprites.Add(DropItems.Ladyfinger,Resources.Load<Sprite>("PocketItems/Ladyfinger"));
        _itemsSprites.Add(DropItems.Lemon,Resources.Load<Sprite>("PocketItems/Lemon"));
        _itemsSprites.Add(DropItems.Milk,Resources.Load<Sprite>("PocketItems/Milk"));
        _itemsSprites.Add(DropItems.Raspberry,Resources.Load<Sprite>("PocketItems/Raspberry"));
        _itemsSprites.Add(DropItems.Sugar,Resources.Load<Sprite>("PocketItems/Sugar"));
        _itemsSprites.Add(DropItems.WhippedCream,Resources.Load<Sprite>("PocketItems/WhippedCream"));
        _itemsSprites.Add(DropItems.Dough,Resources.Load<Sprite>("PocketItems/Dough"));
        _itemsSprites.Add(DropItems.Blood,Resources.Load<Sprite>("PocketItems/Blood"));
    }
    
}

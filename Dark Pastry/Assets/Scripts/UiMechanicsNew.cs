using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public enum DropItems
{
    None,
    Egg,
    Key
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
        _itemsSprites.Add(DropItems.Egg, Resources.Load<Sprite>("PocketItems/Egg"));
        _itemsSprites.Add(DropItems.Key, Resources.Load<Sprite>("PocketItems/Key"));
        _itemsSprites.Add(DropItems.None,null);
    }
    
}

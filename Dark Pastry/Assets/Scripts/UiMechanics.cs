using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mime;
using LevelScripts;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UiMechanics : MonoBehaviour
{
    private enum DropItems
    {
        None,
        Egg
    }
    
    private static float _hearts;
    private static DropItems _pocketItem = DropItems.Egg;
    private static bool _hoveringPedestal;
    private static bool _hoveringItem;
    private static SpriteRenderer _currentPedestal;
    private static SpriteRenderer _currentItem;
    private static MonoBehaviour _instance;

    public static GameObject Heart;
    public static GameObject DropEgg;
    
    private static Sprite _fullHeart;
    private static Sprite _halfHeart;
    private static Sprite _emptyHeart;
    private static Sprite _eggDropItem;
    private static Sprite _eggDropItemHighlighted;
    private static Sprite _eggPocketItem;
    private static Sprite _selectedPedestal;
    private static Sprite _normalPedestal;
    private static Sprite _wrongPedestal;
    private static Sprite _eggOnPedestal;

    public void Awake()
    {
        _fullHeart = Resources.Load<Sprite>("Healthbar_full@4x");
        _halfHeart = Resources.Load<Sprite>("Healthbar_half@4x");
        _emptyHeart = Resources.Load<Sprite>("Healthbar_empty@4x");
        _eggDropItem = Resources.Load<Sprite>("Egg_deadbody_4@4x");
        _eggDropItemHighlighted = Resources.Load<Sprite>("highlight_dead egg@4x");
        _eggPocketItem = Resources.Load<Sprite>("Egg_body@4x");
        _selectedPedestal = Resources.Load<Sprite>("PedestalSelected");
        _normalPedestal = Resources.Load<Sprite>("Pedestal");
        _wrongPedestal = Resources.Load<Sprite>("Environment/Pentagram/pedestalWrong");
        _eggOnPedestal = Resources.Load<Sprite>("Egg_deadbody_4@4x");
        _instance = this;
        Heart = Resources.Load<GameObject>("Heart");
        DropEgg = Resources.Load<GameObject>("DroppedEgg");
        _hearts = 3f; 
        UpdateHealthBarUi(gameObject);
    }

    public static void GainHeart(GameObject ui)
    {
        if (!_fullHeart || !_emptyHeart || !_halfHeart)
        {
            Debug.Log("Failed to load resource");
        }
        _hearts += 0.5f;
        if (_hearts > 3f)
            _hearts = 3f;
        
        Debug.Log("You now have "+_hearts+" hearts");
        UpdateHealthBarUi(ui);
    }

    public static void LoseHeart(GameObject ui)
    {
        if (!_fullHeart || !_emptyHeart || !_halfHeart)
        {
            Debug.Log("Failed to load resource");
        }
        _hearts -= 0.5f;

        Debug.Log("You now have "+_hearts+" hearts");
        UpdateHealthBarUi(ui);
        if (_hearts <= 0f)
        { 
            _instance.StartCoroutine(Die(ui));
        }
    }
    
    private static void UpdateHealthBarUi(GameObject ui)
    {
        var heartsVector = ui.GetComponentsInChildren<Image>();
        
        
        switch (_hearts)
        {
            case 0f:
            {
                heartsVector[1].sprite = _emptyHeart;
                heartsVector[2].sprite = _emptyHeart;
                heartsVector[3].sprite = _emptyHeart;
                break;
            }
            case 0.5f:
            {
                heartsVector[1].sprite = _halfHeart;
                heartsVector[2].sprite = _emptyHeart;
                heartsVector[3].sprite = _emptyHeart;
                break;
            }
            case 1f:
            {
                heartsVector[1].sprite = _fullHeart;
                heartsVector[2].sprite = _emptyHeart;
                heartsVector[3].sprite = _emptyHeart;
                break;
            }
            case 1.5f:
            {
                heartsVector[1].sprite = _fullHeart;
                heartsVector[2].sprite = _halfHeart;
                heartsVector[3].sprite = _emptyHeart;
                break;
            }
            case 2f:
            {
                heartsVector[1].sprite = _fullHeart;
                heartsVector[2].sprite = _fullHeart;
                heartsVector[3].sprite = _emptyHeart;
                break;
            }
            case 2.5f:
            {
                heartsVector[1].sprite = _fullHeart;
                heartsVector[2].sprite = _fullHeart;
                heartsVector[3].sprite = _halfHeart;
                break;
            }
            case 3f:
            {
                heartsVector[1].sprite = _fullHeart;
                heartsVector[2].sprite = _fullHeart;
                heartsVector[3].sprite = _fullHeart;
                break;
            }
        }
    }

    public static void CollectItemInPocket(GameObject item, GameObject ui)
    {
        if(_pocketItem!=DropItems.None) return;
        var pocketSprite = ui.GetComponentsInChildren<Image>()[9];
        var pocketLabel = ui.GetComponentsInChildren<TextMeshProUGUI>()[0];
        Debug.Log(item.name);
        if (item.name.Contains("DroppedEgg"))
        {
            _pocketItem = DropItems.Egg;
            pocketSprite.sprite = _eggPocketItem;
            pocketLabel.text = "Egg";
            Destroy(item);
        }
    }

    public static bool PlaceItemOnPedestal(GameObject pedestal, GameObject ui)
    {
        switch (_pocketItem)
        {
            case DropItems.None:
            {
                if(pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite!=null) return false;
                _instance.StartCoroutine(PedestalFlashing(pedestal));
                return false;
            }
            case DropItems.Egg:
            {
                if(pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite!=null) return false;
                pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite = _eggOnPedestal;
                ConsumePocket(ui);
                ModifySpeedPentagram(pedestal,0.3f);
                ui.GetComponent<GenericLevel>().ItemPlaced();
                return true;
            }
        }

        return false;
    }

    public static void RetakeItemFromPedestal(GameObject pedestal, GameObject ui)
    {
        if(pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite==null) return;
        if (_pocketItem != DropItems.None)
        {
            _instance.StartCoroutine(PedestalFlashing(pedestal));
            return;
        }
        else
        {
            if (pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite == _eggOnPedestal)
            {
                var egg = new GameObject {name = "DroppedEgg"};
                CollectItemInPocket(egg,ui);
                ModifySpeedPentagram(pedestal,-0.3f);
                pedestal.GetComponentsInChildren<SpriteRenderer>()[1].sprite = null;
                ui.GetComponent<GenericLevel>().ItemTaken();
            }
        }
    }

    private static void ModifySpeedPentagram(GameObject pedestal,float increment)
    {
        pedestal.transform.parent.gameObject.transform.parent.gameObject.GetComponentsInChildren<ParticleSystem>()[0]
            .playbackSpeed += increment;
    }
    
    public static void ConsumePocket(GameObject ui)
    {
        var pocketSprite = ui.GetComponentsInChildren<Image>()[9];
        var pocketLabel = ui.GetComponentsInChildren<TextMeshProUGUI>()[0];
        _pocketItem = DropItems.None;
        pocketSprite.sprite = null;
        pocketLabel.text = "Empty";
    }

    private static IEnumerator Die(GameObject ui)
    {
        Agata.AgataAnimator.SetTrigger("death");
        Agata.Dead = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(ui.GetComponentInChildren<GenericLevel>().GetCurrent());
        _hearts = 2f;
        UpdateHealthBarUi(ui);
    }

    private static IEnumerator PedestalFlashing(GameObject pedestal)
    {
        pedestal.GetComponent<SpriteRenderer>().sprite = _wrongPedestal;
        yield return new WaitForSeconds(0.1f);
        pedestal.GetComponent<SpriteRenderer>().sprite = _hoveringPedestal ? _selectedPedestal : _normalPedestal;
        yield return new WaitForSeconds(0.1f);
        pedestal.GetComponent<SpriteRenderer>().sprite = _wrongPedestal;
        yield return new WaitForSeconds(0.1f);
        pedestal.GetComponent<SpriteRenderer>().sprite = _hoveringPedestal ? _selectedPedestal : _normalPedestal;
    }

    public static void PedestalHovering(GameObject pedestal)
    {
        if (_hoveringPedestal) return;
        _currentPedestal = pedestal.GetComponent<SpriteRenderer>();
        _currentPedestal.sprite = _selectedPedestal;
        _hoveringPedestal = true;
    }

    public static void PedestalRemoveHovering()
    {
        if (!_hoveringPedestal) return;
        _currentPedestal.sprite = _normalPedestal;
        _currentPedestal = null;
        _hoveringPedestal = false;
    }
    
    public static void ItemHovering(GameObject item)
    {
        if (_hoveringItem) return;
        _currentItem = item.GetComponent<SpriteRenderer>();
        _currentItem.sprite = _eggDropItemHighlighted;
        _hoveringItem = true;
    }
    
    public static void ItemRemoveHovering()
    {
        if (!_hoveringItem) return;
        try
        {
            _currentItem.sprite = _eggDropItem;
        }
        catch (MissingReferenceException e)
        {
            
        }
        _currentItem = null;
        _hoveringItem = false;
    }

}

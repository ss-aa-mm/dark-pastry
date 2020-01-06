using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    private static List<Pedestal> _pedestals;
    private static string[] _itemsVector;
    private static int _objectsPlaced;
    private static bool _unlocked;
    private static bool _globeEnabled;
    private static Animator _animator;
    private static Animator _chainAnimator;
    private static BoxCollider2D _chainBody;
    private static Dictionary<List<string>, GameObject> _mixingDictionary;
    private static readonly int ItemPlaced = Animator.StringToHash("itemPlaced");
    private static readonly int Ready = Animator.StringToHash("ready");
    private static readonly int NotReady = Animator.StringToHash("notReady");
    private static readonly int PedestalsActive = Animator.StringToHash("pedestalsActive");
    private static readonly int Activated = Animator.StringToHash("activated");
    private static readonly int KeyUnlock = Animator.StringToHash("keyUnlock");

    private void Awake()
    {
        _pedestals = GetComponentsInChildren<Pedestal>().ToList();
        _objectsPlaced = 0;
        _unlocked = false;
        _itemsVector = new string[5];
        _animator = GetComponent<Animator>();
        _mixingDictionary = new Dictionary<List<string>, GameObject>();
        _chainAnimator = GetComponentInChildren<LockChain>().gameObject.GetComponent<Animator>();
        _chainBody = GetComponentInChildren<LockChain>().gameObject.GetComponent<BoxCollider2D>();
        InitializeDictionary();
        LevelData.DataLoad();
        _globeEnabled = LevelData.GetInfo().globeEnabled;
        UpdateItemsVector();
    }
    
    private void Update()
    {
        if(_unlocked) return;
        if (_objectsPlaced == 5)
        {
            if (!_itemsVector.SequenceEqual(LevelData.GetInfo().ingredients)) return;

            // ReSharper disable once Unity.PreferNonAllocApi
            var collisions = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (var coll in collisions)
            {
                if (!coll.CompareTag("Agata") || !Input.GetButtonDown("Dance")) continue;
                _unlocked = true;
                _chainAnimator.SetTrigger(KeyUnlock);
                _chainBody.enabled = false;
                Book.UpdateBookRevealRecipe(LevelData.GetCurrentLevel());
                Book.Render();
                /*AgataNew.PauseManager();
                Book.Spawn();*/
                foreach (var pedestal in _pedestals)
                {
                    Destroy(pedestal);
                }
            }
        }
        else if (_objectsPlaced > 1 && _globeEnabled)
        {
            if (!Input.GetButtonDown("Dance")) return;
            // ReSharper disable once Unity.PreferNonAllocApi
            var collisions = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (var coll in collisions)
            {
                if (!coll.CompareTag("Agata")) continue;
                var list = _itemsVector.ToList();
                list.RemoveAll(s => s == "None");
                var valid = false;
                GameObject obj = null;
                foreach (var recipe in _mixingDictionary.Where(recipe =>
                    recipe.Key.OrderBy(t => t).SequenceEqual(list.OrderBy(s => s))))
                {
                    //Debug.Log(recipe.Value.ToString());
                    valid = true;
                    obj = recipe.Value;
                }

                if (valid)
                {
                    foreach (var pedestal in _pedestals)
                    {
                        pedestal.Initialize();
                    }

                    _objectsPlaced = 0;
                    _animator.SetTrigger(Activated);
                    Instantiate(obj, transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Invalid recipe");
                }
            }
        }
        
    }

    public static void ObjectAdded()
    {
        _objectsPlaced++;
        UpdateItemsVector();
        if(_objectsPlaced == 1)
            _animator.SetTrigger(ItemPlaced);
        if(_objectsPlaced > 1 && _globeEnabled)
            _animator.SetTrigger(Ready);
    }

    public static void ObjectRemoved()
    {
        _objectsPlaced--;
        UpdateItemsVector();
        switch (_objectsPlaced)
        {
            case 1 when _globeEnabled:
                _animator.SetTrigger(NotReady);
                break;
            case 0:
                _animator.SetTrigger(PedestalsActive);
                break;
            default:
                return;
        }
    }

    private static void UpdateItemsVector()
    {
        var i = 0;
        foreach (var pedestal in _pedestals)
        {
            _itemsVector[i] = pedestal.GetObject().ToString();
            i++;
        }
    }

    public static bool IsUnlocked()
    {
        return _unlocked;
    }

    private static void InitializeDictionary()
    {
        _mixingDictionary.Add(new List<string> {DropItems.Egg.ToString(),DropItems.Flour.ToString()}, Resources.Load<GameObject>("Prefabs/Items/DroppedDough") );
    }

}

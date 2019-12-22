using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct PentagramData
{
    public string[] ingredients;
}

public class Pentagram : MonoBehaviour
{
    private PentagramData _pentagramRules;
    private static List<Pedestal> _pedestals;
    private static string[] _itemsVector;
    private static int _objectsPlaced;
    private static bool _unlocked;

    private void Awake()
    {
        _pedestals = GetComponentsInChildren<Pedestal>().ToList();
        _pentagramRules = JsonUtility.FromJson<PentagramData>(Resources.Load<TextAsset>("data").text);
        _objectsPlaced = 0;
        _unlocked = false;
        _itemsVector = new string[5];
        UpdateItemsVector();
    }
    
    private void Update()
    {
        if(_unlocked) return;
        if(_objectsPlaced != 5) return;
        if(!_itemsVector.SequenceEqual(_pentagramRules.ingredients)) return;

        var collisions = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (var coll in collisions)
        {
            if (!coll.CompareTag("Agata") || !Input.GetButtonDown("Dance")) continue;
            _unlocked = true;
            foreach (var pedestal in _pedestals)
            {
                Destroy(pedestal);
            }
        }

    }

    public static void ObjectAdded()
    {
        _objectsPlaced++;
        UpdateItemsVector();
    }

    public static void ObjectRemoved()
    {
        _objectsPlaced--;
        UpdateItemsVector();
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
    
}

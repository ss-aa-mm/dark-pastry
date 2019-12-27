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
    private static ParticleSystem _particleSystem;
    private static MonoBehaviour _instance;

    private void Awake()
    {
        _pedestals = GetComponentsInChildren<Pedestal>().ToList();
        _objectsPlaced = 0;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _unlocked = false;
        _itemsVector = new string[5];
        _instance = this;
        LevelData.DataLoad();
        UpdateItemsVector();
    }
    
    private void Update()
    {
        if(_unlocked) return;
        if(_objectsPlaced != 5) return;
        _instance.StartCoroutine(LightEffect());
        if(!_itemsVector.SequenceEqual(LevelData.GetInfo().ingredients)) return;

        // ReSharper disable once Unity.PreferNonAllocApi
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
        _instance.StopAllCoroutines();
        var main = _particleSystem.main;
        main.startColor = Color.white;
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

    // ReSharper disable once FunctionRecursiveOnAllPaths
    private static IEnumerator LightEffect()
    {
        var main = _particleSystem.main;
        main.startColor = Color.green;
        yield return new WaitForSeconds(0.3f);
        main.startColor = Color.red;
        yield return new WaitForSeconds(0.3f);
        main.startColor = Color.blue;
        yield return new WaitForSeconds(0.3f);
        _instance.StartCoroutine(LightEffect());
    }
    
}

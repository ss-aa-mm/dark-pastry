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
    private List<Pedestal> _pedestals;
    private static bool _unlocked;
    private static bool _enabled;
    
    private void Awake()
    {
        _pedestals = GetComponentsInChildren<Pedestal>().ToList();
        _pentagramRules = JsonUtility.FromJson<PentagramData>(Resources.Load<TextAsset>("data").text);
        _unlocked = false;
        _enabled = false;
    }
    
    private void Update()
    {
        if(_unlocked) return;
        var i = 0;
        var comparisonArray = new string[5];
        foreach (var pedestal in _pedestals)
        {
            comparisonArray[i] = pedestal.GetObject().ToString();
            i++;
        }

        _enabled = comparisonArray.SequenceEqual(_pentagramRules.ingredients);
        
        if(!_enabled) return;

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

    public static bool IsUnlocked()
    {
        return _unlocked;
    }
    
}

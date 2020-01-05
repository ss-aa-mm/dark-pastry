using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int _enemies;

    private List<RoomChain> _chains;

    private bool _chainEnabled;

    private void Awake()
    {
        RefreshEnemyCount();
        _chains = GetComponentsInChildren<RoomChain>().ToList();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Agata")) return;
        RefreshEnemyCount();
        if (_enemies == 0 && _chainEnabled)
        {
            DisableAllChains();
            _chainEnabled = false;
        }
        else if (_enemies != 0 && !_chainEnabled)
        {
            EnableAllChains();
            _chainEnabled = true;
        }
    }

    private void DisableAllChains()
    {
        foreach (var chain in _chains)
        {
            chain.Disable();
        }
    }

    private void EnableAllChains()
    {
        foreach (var chain in _chains)
        {
            chain.Enable();
        }
    }

    private void RefreshEnemyCount()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList().Count;
    }
}

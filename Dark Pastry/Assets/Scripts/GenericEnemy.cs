using System;
using System.Collections;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine;
using Random = System.Random;

public abstract class GenericEnemy : MonoBehaviour
{
    public bool dropsHeart;

    public bool dropsPocketItem;

    private const float Speed = 4f;

    private float _direction;

    private float _iterationCounter;

    private readonly Random _rng = new Random();
    
    private void Update()
    {
        var unit = Speed * Time.deltaTime;
        _iterationCounter++;
        if (_iterationCounter > 10)
        {
            _iterationCounter = 0;
            _direction = _rng.Next(0, 5);
        }
        switch (_direction)
        {
          case 1:
              transform.Translate(unit,0,0);
              break;
          case 2:
              transform.Translate(0,unit,0);
              break;
          case 3:
              transform.Translate(-unit,0,0);
              break;
          case 4:
              transform.Translate(0,-unit,0);
              break;
          default:
              break;
        }

    }

    public abstract void Collapse(GenericLevel level);
    
}

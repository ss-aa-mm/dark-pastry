using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    private Transform _tr;

    private Vector3Int _pos;

    private float _unit = .5f;

    public float movementTime = .5f;

    private float _timeLeft;

    // Update is called once per frame
    void Update()
    {
        _tr = transform;

        _timeLeft -= Time.deltaTime;
        if (_timeLeft > 0)
            return;

        var dir = Random.Range(0, 4);

        switch (dir)
        {
            case 0:
                transform.Translate(_unit, 0f, 0f);
                break;
            case 1:
                transform.Translate(0f, _unit, 0f);
                break;
            case 2:
                transform.Translate(-_unit, 0f, 0f);
                break;
            case 3:
                transform.Translate(0f, -_unit, 0f);
                break;
        }

        _timeLeft += movementTime;
    }
}
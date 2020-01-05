using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private static Animator _animator;
    private static GameObject _gameObject;
    private static readonly int BookOpen = Animator.StringToHash("bookOpen");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>(true);
        _gameObject = gameObject;
    }

    public static void Open()
    {
        _animator.SetTrigger(BookOpen);
        _gameObject.SetActive(true);
    }
}

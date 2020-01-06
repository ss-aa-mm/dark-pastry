using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Book : MonoBehaviour
{
    private static Animator _animator;
    private static Tuple<Image , Image> _pages;
    private static Sprite _0L0;
    private static Sprite _0L1;
    private static Sprite _0L2;
    private static Sprite _0R0;
    private static Sprite _0R1;
    private static Sprite _1L0;
    private static Sprite _1L1;
    private static Sprite _1L2;
    private static Sprite _1R0;
    private static Sprite _1R1;
    private static Sprite _2L0;
    private static Sprite _2L1;
    private static Sprite _2L2;
    private static Sprite _2R0;
    private static Sprite _2R1;
    private static Sprite _3L0;
    private static Sprite _3L1;
    private static Sprite _3L2;
    private static Sprite _3R0;
    private static Sprite _3R1;
    private static Sprite _dR;
    private static Sprite _dL;
    private static int _position;
    private static bool _paused;
    private static readonly int BookFlip = Animator.StringToHash("bookFlip");

    private void Awake()
    {
        var animators = GetComponentsInChildren<Animator>(true).ToList();
        animators.RemoveAll(a => !a.CompareTag("Book"));
        _animator = animators.First();
        var images = GetComponentsInChildren<Image>(true).ToList();
        images.RemoveAll(a => !a.CompareTag("RenderingPage"));
        _pages = new Tuple<Image , Image>(images.First(),images.Last());
        _0L0 = Resources.Load<Sprite>("Book/0L0");
        _0L1 = Resources.Load<Sprite>("Book/0L1");
        _0L2 = Resources.Load<Sprite>("Book/0L2");
        _0R0 = Resources.Load<Sprite>("Book/0R0");
        _0R1 = Resources.Load<Sprite>("Book/0R1");
        _1L0 = Resources.Load<Sprite>("Book/1L0");
        _1L1 = Resources.Load<Sprite>("Book/1L1");
        _1L2 = Resources.Load<Sprite>("Book/1L2");
        _1R0 = Resources.Load<Sprite>("Book/1R0");
        _1R1 = Resources.Load<Sprite>("Book/1R1");
        _2L0 = Resources.Load<Sprite>("Book/2L0");
        _2L1 = Resources.Load<Sprite>("Book/2L1");
        _2L2 = Resources.Load<Sprite>("Book/2L2");
        _2R0 = Resources.Load<Sprite>("Book/2R0");
        _2R1 = Resources.Load<Sprite>("Book/2R1");
        _3L0 = Resources.Load<Sprite>("Book/3L0");
        _3L1 = Resources.Load<Sprite>("Book/3L1");
        _3L2 = Resources.Load<Sprite>("Book/3L2");
        _3R0 = Resources.Load<Sprite>("Book/3R0");
        _3R1 = Resources.Load<Sprite>("Book/3R1");
        _dL = _pages.Item1.sprite;
        _dR = _pages.Item2.sprite;
        if (LevelData.BookStructure == null)
        {
            LevelData.BookStructure = new[]
            {
                Tuple.Create(_dL,_dR),
                Tuple.Create(_0L1,_0R0),
                Tuple.Create(_1L0,_1R0),
                Tuple.Create(_2L0,_2R0),
                Tuple.Create(_3L0,_3R0) 
            };
        }
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!_paused)
            {
                Spawn();
            }
            else
            {
                _animator.gameObject.SetActive(false);
                _paused = false;
            }
        }
        if(!_paused) return;
        if (Input.GetButtonDown("RightScroll"))
        {
            _animator.SetTrigger(BookFlip);
            _position++;
            if (_position == LevelData.BookStructure.Length)
                _position = 0;
            Render();
        }
        else if (Input.GetButtonDown("LeftScroll"))
        {
            _animator.SetTrigger(BookFlip);
            _position--;
            if (_position == -1)
                _position = LevelData.BookStructure.Length - 1;
            Render();
        }
        
    }

    public static void Render()
    {
        _pages.Item1.sprite = LevelData.BookStructure[_position].Item1;
        _pages.Item2.sprite = LevelData.BookStructure[_position].Item2;
    }

    public static void UpdateBookUnlockLevel(int level)
    {
        Sprite sp;
        switch (level)
        {
            case 0:
                sp = _0L1;
                break;
            case 1:
                sp = _1L1;
                break;
            case 2:
                sp = _2L1;
                break;
            case 3:
                sp = _3L1;
                break;
            default:
                return;
        }

        LevelData.BookStructure[level + 1] = Tuple.Create(sp,LevelData.BookStructure[level+1].Item2);
    }

    public static void UpdateBookUnlockHint(int level)
    {
        Sprite sp;
        switch (level)
        {
            case 0:
                sp = _0R1;
                break;
            case 1:
                sp = _1R1;
                break;
            case 2:
                sp = _2R1;
                break;
            case 3:
                sp = _3R1;
                break;
            default:
                return;
        }

        LevelData.BookStructure[level + 1] = Tuple.Create(LevelData.BookStructure[level+1].Item1,sp);
    }
    
    public static void UpdateBookRevealRecipe(int level)
    {
        Sprite sp;
        switch (level)
        {
            case 0:
                sp = _0L2;
                break;
            case 1:
                sp = _1L2;
                break;
            case 2:
                sp = _2L2;
                break;
            case 3:
                sp = _3L2;
                break;
            default:
                return;
        }

        LevelData.BookStructure[level + 1] = Tuple.Create(sp,LevelData.BookStructure[level + 1].Item2);
    }

    public static void Spawn()
    {
        _animator.gameObject.SetActive(true);
        _paused = true;
    }

}

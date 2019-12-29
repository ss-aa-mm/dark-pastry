using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgataNew : MonoBehaviour
{
    private const float Speed = 3f;
    private const float MaxLife = 3f;
    private float _unit;
    private static float _life;
    private static bool _isAttacking;
    private static bool _isDancing;
    private static bool _isActive;
    private static bool _paused;
    private static DropItems _item;
    private static HighlightableObject _interactiveObject;
    private static Animator _animator;
    private static MonoBehaviour _instance;
    private static List<SpriteRenderer> _agataParts;
    private static readonly int HorizontalAxis = Animator.StringToHash("HorizontalAxis");
    private static readonly int VerticalAxis = Animator.StringToHash("VerticalAxis");
    private static readonly int IsMovingH = Animator.StringToHash("isMovingH");
    private static readonly int IsMovingV = Animator.StringToHash("isMovingV");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("death");
    private static readonly int Dance = Animator.StringToHash("dance");

    private void Awake()
    {
        _life = 3f;
        _item = DropItems.None;
        _isAttacking = false;
        _paused = false;
        _isActive = true;
        _animator = GetComponent<Animator>();
        _agataParts = GetComponentsInChildren<SpriteRenderer>().ToList();
        _instance = this;
    }

    private void Update()
    {
        //Death verification
        if (!_isActive) return;
        //End of death verification

        //Movement calculation
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        _unit = Speed * Time.deltaTime;
        transform.Translate(h * _unit, v * _unit, 0);
        //End of movement calculation

        //Interaction with objects
        if (Input.GetButtonDown("Objects Interaction") && _interactiveObject)
        {
            _interactiveObject.Interact();
        }
        //End of interaction with objects
        
        // Pausing - resuming game
        if (Input.GetButtonDown("Pause"))
            PauseManager();
        //End of pause function

        //Attack
        _isAttacking = Input.GetButtonDown("Attack");
        //End of Attack
        
        //Dance
        _isDancing = Input.GetButtonDown("Dance");
        //End of Dance

        //DEBUG COMMANDS
        
        //END OF DEBUG COMMANDS

        //Animation
        if(!_paused)
            UpdateAnimator(h, v, _isAttacking,_isDancing);
        //End of Animation
    }

    public static HighlightableObject GetInteractiveObject()
    {
        return _interactiveObject;
    }

    public static bool IsInteractiveObjectSet()
    {
        return _interactiveObject;
    }

    public static void SetInteractiveObject(HighlightableObject iObject)
    {
        _interactiveObject = iObject;
    }

    public static float GetLife()
    {
        return _life;
    }

    public static void SetLife(float increment)
    {
        if (increment > 0 && _life >= MaxLife) return;
        if (_life <= 0) return;
        if (increment < 0)
            _instance.StartCoroutine(DamageAnimation());
        _life += increment;
        if (_life > MaxLife)
            _life = MaxLife;
        if (_life <= 0)
            _instance.StartCoroutine(Die());
    }

    public static DropItems GetItem()
    {
        return _item;
    }

    public static void SetItem(DropItems item)
    {
        _item = item;
    }

    private static IEnumerator Die()
    {
        _isActive = false;
        _animator.SetTrigger(Death);
        yield return new WaitForSeconds(1f);
    }

    private static IEnumerator DamageAnimation()
    {
        for (var i = 0; i < 2; i++)
        {
            foreach (var part in _agataParts)
            {
                part.color = Color.red;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (var part in _agataParts)
            {
                part.color = Color.white;
            }
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    public static int GetAnimatorHash()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).tagHash;
    }

    private static void PauseManager()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            _paused = true;
        }
        else
        {
            Time.timeScale = 1;
            _paused = false;
        }
    }

    private static void UpdateAnimator(float h, float v, bool isAttacking, bool isDancing)
    {
        const float tolerance = 0.000000000000001f;
        if (Math.Abs(h) < tolerance && Math.Abs(v) < tolerance)
        {
            _animator.SetBool(IsMovingH,false);
            _animator.SetBool(IsMovingV,false);
        }
        else if (Math.Abs(h) > Math.Abs(v))
        {
            _animator.SetBool(Dance,false);
            _animator.SetBool(IsMovingH,true);
            _animator.SetBool(IsMovingV,false);
            _animator.SetFloat(HorizontalAxis,h);
            _animator.SetFloat(VerticalAxis,v);
        }
        else
        {
            _animator.SetBool(Dance,false);
            _animator.SetBool(IsMovingH,false);
            _animator.SetBool(IsMovingV,true);
            _animator.SetFloat(HorizontalAxis,h);
            _animator.SetFloat(VerticalAxis,v);
        }
        
        if (isAttacking)
        { 
            _animator.SetBool(Dance,false);
            _animator.SetTrigger(Attack);
        }

        if (isDancing)
        {
            _animator.SetBool(Dance, true);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Agata : MonoBehaviour
{

    public static bool Dead;
    
    private Transform _tr;

    private Animator _agataAnimator;
    
    private Vector3Int _pos;

    private float _unit;

    private bool _isAttacking;

    private const float Speed=3f;
    
    public GameObject userInterface;

    private static Collider2D _solidObject;

    private void Awake()
    {
        UiMechanics.ConsumePocket(userInterface);
        _agataAnimator = gameObject.GetComponent<Animator>();
        _solidObject = null;
        Dead = false;
    }

    private void Update()
    {
        if(Dead) return;
        _isAttacking = false;
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _tr = transform;
        _unit = Speed * Time.deltaTime;
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(_unit * Input.GetAxis("Horizontal"), 0f, 0f);
            /*if (Input.GetAxis("Horizontal") < 0)
            {
                _agataAnimator.ResetTrigger("WalkRight");
                _agataAnimator.ResetTrigger("WalkUp");
                _agataAnimator.ResetTrigger("WalkDown");
                _agataAnimator.SetTrigger("WalkLeft");
            }
            else
            {
                _agataAnimator.ResetTrigger("WalkLeft");
                _agataAnimator.ResetTrigger("WalkUp");
                _agataAnimator.ResetTrigger("WalkDown");
                _agataAnimator.SetTrigger("WalkRight");
            }*/
                
        }

        if (Input.GetButton("Vertical"))
        {
            transform.Translate(0f, _unit * Input.GetAxis("Vertical"), 0f);
            /*if (Input.GetAxis("Vertical") < 0)
            {
                _agataAnimator.ResetTrigger("WalkUp");
                _agataAnimator.ResetTrigger("WalkRight");
                _agataAnimator.ResetTrigger("WalkLeft");
                _agataAnimator.SetTrigger("WalkDown");
            }
            else
            {
                _agataAnimator.ResetTrigger("WalkDown");
                _agataAnimator.ResetTrigger("WalkRight");
                _agataAnimator.ResetTrigger("WalkLeft");
                _agataAnimator.SetTrigger("WalkUp");
            }*/
        }

        //Stub of the attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var armPosition = gameObject.GetComponentsInChildren<Transform>()[8].position;
            Debug.DrawLine(new Vector2(armPosition.x+0.3f,armPosition.y),armPosition+transform.right*10,Color.red);
            var hit = Physics2D.Raycast(new Vector2(armPosition.x+0.3f,armPosition.y), armPosition+transform.right*100,0.3f);
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
                hit.collider.gameObject.GetComponent<GenericEnemy>().Collapse();
            }
        }

        try
        {
            _solidObject = Physics2D.OverlapCircleAll(position, 0.01f)[0];
        }
        catch (IndexOutOfRangeException e)
        {
            UiMechanics.PedestalRemoveHovering();
            return;
        }

        if (_solidObject.gameObject.CompareTag("DropItem") && Input.GetMouseButton(0))
            UiMechanics.CollectItemInPocket(_solidObject.gameObject, userInterface);

        if (_solidObject.gameObject.CompareTag("Pedestal"))
        {
            UiMechanics.PedestalHovering(_solidObject.gameObject);
            if (Input.GetMouseButton(0))
            {
                UiMechanics.PlaceItemOnPedestal(_solidObject.gameObject, userInterface);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hearts"))
        {
            UiMechanics.GainHeart(userInterface);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if(_isAttacking)
                Destroy(other.gameObject);
            else
                UiMechanics.LoseHeart(userInterface);
        }
    }
}

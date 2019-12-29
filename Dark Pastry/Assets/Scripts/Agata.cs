﻿using System;
using System.Collections;
using System.Collections.Generic;
using LevelScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Agata : MonoBehaviour
{

    public static bool Dead;
    
    private Transform _tr;

    public static Animator AgataAnimator;
    
    private Vector3Int _pos;

    private float _unit;

    private const float Speed=3f;
    
    public GameObject userInterface;

    private GenericLevel _currentLevel;

    private static Collider2D _solidObject;

    private void Awake()
    {
        UiMechanics.ConsumePocket(userInterface);
        //UiMechanics.UpdateHealthBarUi(userInterface);
        AgataAnimator = gameObject.GetComponent<Animator>();
        _solidObject = null;
        _currentLevel = userInterface.GetComponent<GenericLevel>();
        Dead = false; 
        var obj = FindObjectsOfType(typeof(MonoBehaviour));
        foreach (var objc in obj)
        {
            Debug.Log(objc.name);
        }
    }

    private void Update()
    {
        if(Dead) return;
        //ResetAnimator();
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _tr = transform;
        _unit = Speed * Time.deltaTime;
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(_unit * Input.GetAxis("Horizontal"), 0f, 0f);
            if (Input.GetAxis("Horizontal") < 0)
            {
                AgataAnimator.SetBool("WalkLeft",true);
            }
            else
            {
                AgataAnimator.SetBool("WalkRight",true);
            }
                
        }

        if (Input.GetButton("Vertical"))
        {
            transform.Translate(0f, _unit * Input.GetAxis("Vertical"), 0f);
            if (Input.GetAxis("Vertical") < 0)
            {
                AgataAnimator.SetBool("WalkDown",true);
            }
            else
            {
                AgataAnimator.SetBool("WalkUp",true);
            }
        }

        //Stub of the attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimatedAttack();
            var (pos, dir) = FireCoordinates();
            Debug.DrawLine(pos,pos+dir,Color.red);
            var hit = Physics2D.Raycast(pos, dir,0.3f);
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
                hit.collider.gameObject.GetComponent<GenericEnemy>().Collapse(_currentLevel);
            }
        }

        try
        {
            _solidObject = Physics2D.OverlapCircleAll(position, 0.01f)[0];
        }
        catch (IndexOutOfRangeException e)
        {
            UiMechanics.PedestalRemoveHovering();
            UiMechanics.ItemRemoveHovering();
            return;
        }

        if (_solidObject.gameObject.CompareTag("DropItem") && Input.GetMouseButton(0))
            UiMechanics.CollectItemInPocket(_solidObject.gameObject, userInterface);

        if (_solidObject.gameObject.CompareTag("Pedestal"))
        {
            UiMechanics.PedestalHovering(_solidObject.gameObject);
            if (Input.GetMouseButton(0))
            {
                var placed = UiMechanics.PlaceItemOnPedestal(_solidObject.gameObject, userInterface);
                if (!placed)
                {
                    UiMechanics.RetakeItemFromPedestal(_solidObject.gameObject,userInterface);
                }
            }
        }
        else if(_solidObject.gameObject.CompareTag("DropItem"))
        {
            UiMechanics.ItemHovering(_solidObject.gameObject);
        }
    }

    private void ResetAnimator()
    {
        AgataAnimator.SetBool("WalkUp",false);
        AgataAnimator.SetBool("WalkRight",false);
        AgataAnimator.SetBool("WalkDown",false);
        AgataAnimator.SetBool("WalkLeft",false);
        AgataAnimator.SetBool("AttackUp",false);
        AgataAnimator.SetBool("AttackRight",false);
        AgataAnimator.SetBool("AttackDown",false);
        AgataAnimator.SetBool("AttackLeft",false);
    }

    private void AnimatedAttack()
    {
        if (AgataAnimator.GetBool("WalkUp"))
        {
            AgataAnimator.SetBool("AttackUp",true);
        }
        else if (AgataAnimator.GetBool("WalkLeft"))
        {
            AgataAnimator.SetBool("AttackLeft",true);
        }
        else if (AgataAnimator.GetBool("WalkRight"))
        {
            AgataAnimator.SetBool("AttackRight",true);
        }
        else
        {
            AgataAnimator.SetBool("AttackDown",true);
        }
    }

    private Tuple<Vector3,Vector3> FireCoordinates()
    {
        Vector3 firePosition;
        Vector3 fireDirection;
        if (AgataAnimator.GetBool("AttackUp"))
        {
            firePosition = gameObject.GetComponentsInChildren<Head>()[0].transform.position;
            fireDirection = Vector3.up;
        }
        else if (AgataAnimator.GetBool("AttackRight"))
        {
            firePosition = gameObject.GetComponentsInChildren<RightArm>()[0].transform.position;
            fireDirection = Vector3.right;
        }
        else if (AgataAnimator.GetBool("AttackLeft"))
        {
            firePosition = gameObject.GetComponentsInChildren<LeftArm>()[0].transform.position;
            fireDirection = Vector3.left;
        }
        else
        {
            firePosition = gameObject.GetComponentsInChildren<Body>()[0].transform.position+new Vector3(0,-0.2f,0);
            fireDirection = Vector3.down;
        }

        return Tuple.Create(firePosition, fireDirection);
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
            UiMechanics.LoseHeart(userInterface);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LevelTransition(other));
        
    }

    private IEnumerator LevelTransition(Component other)
    {
        if (other.gameObject.CompareTag("TriggerZone") && _currentLevel.IsClear())
        {
            Dead = true;
            userInterface.GetComponentsInChildren<Animator>(true)[0].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(_currentLevel.GetNext());
        }
    }
}

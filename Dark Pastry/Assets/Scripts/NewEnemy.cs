using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewEnemy : MonoBehaviour
{
    protected float DamageInflicted;
    protected GameObject ItemDropped;
    private GameObject _heart;
    public bool dropsHeart;
    public bool dropsItem;


    private void Awake()
    {
        _heart = Resources.Load<GameObject>("Prefabs/Heart");
        AssignReferences();
    }

    private void Update()
    {
        MovementPattern();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Agata")) return;
        AgataNew.SetLife(-DamageInflicted);
    }

    public void OnDeath()
    {
        if (dropsHeart)
            Instantiate(_heart, transform.position, Quaternion.identity);
        if (dropsItem)
            Instantiate(ItemDropped, transform.position, Quaternion.identity);
    }

    protected abstract void AssignReferences();

    protected abstract void MovementPattern();

}

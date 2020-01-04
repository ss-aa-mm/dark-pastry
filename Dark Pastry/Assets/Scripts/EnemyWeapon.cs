using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Agata")) //|| AgataNew.GetAnimatorHash() != AttackHash)
            return;

        var damage = other.GetComponent<NewEnemy>().GetDamage();
        AgataNew.SetLife(-damage);

        other.GetComponent<Rigidbody>().AddForce(transform.position * -50);
    }
}
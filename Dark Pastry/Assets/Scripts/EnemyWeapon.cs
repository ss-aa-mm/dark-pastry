using Enemies;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Agata") || Enemy.GetAnimatorHash() != AttackHash)
            return;

        var enemy = GetComponentInParent<Enemy>();
        var agata = other.GetComponentInParent<AgataNew>();
        var enemyBody = agata.transform.GetComponentInParent<Rigidbody2D>();
        var agataBody = other.transform.GetComponentInParent<Rigidbody>(); 

        AgataNew.SetLife(-enemy.GetDamage());
        agataBody.AddForce(enemyBody.position * -10, ForceMode.Impulse);
    }
}
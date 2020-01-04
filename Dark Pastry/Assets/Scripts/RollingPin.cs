using Enemies;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        var enemy = other.GetComponentInParent<Enemy>();
        var agata = GetComponentInParent<AgataNew>();
        var enemyBody = other.transform.GetComponentInParent<Rigidbody2D>();
        var agataBody = agata.transform.GetComponentInParent<Rigidbody2D>();
        
        enemy.OnHit();
        enemyBody.AddForce(agataBody.position * -5, ForceMode2D.Impulse);
    }
}
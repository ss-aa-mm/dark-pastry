using Enemies;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        other.GetComponentInParent<Enemy>().OnHit();
        HitEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        other.GetComponentInParent<Enemy>().OnHit();
        HitEnemy(other);
    }
    
    private void HitEnemy(Component enemy)
    {
        var enemyBody = enemy.transform.GetComponentInParent<Rigidbody2D>();
        var agataBody = GetComponentInParent<AgataNew>().transform.GetComponentInParent<Rigidbody2D>();
        var newPosition = new Vector2();

        if (agataBody.position.x > enemyBody.position.x) //Agata is to the right of the enemy
        {
            if (enemyBody.position.x >= 0)
                newPosition.x = enemyBody.position.x * -5;
            else
                newPosition.x = enemyBody.position.x * 5;
        }
        else //Agata is to the left of the enemy
        {
            if (enemyBody.position.x >= 0)
                newPosition.x = enemyBody.position.x * 5;
            else
                newPosition.x = enemyBody.position.x * -5;
        }

        if (agataBody.position.y > enemyBody.position.y) //Agata is above the enemy
        {
            if (enemyBody.position.y >= 0)
                newPosition.y = enemyBody.position.y * -5;
            else
                newPosition.y = enemyBody.position.y * 5;
        }
        else //Agata is below the enemy
        if (enemyBody.position.y >= 0)
            newPosition.y = enemyBody.position.y * 5;
        else
            newPosition.y = enemyBody.position.y * -5;

        //Push enemy away after hit
        enemyBody.AddForce(newPosition, ForceMode2D.Impulse);
    }
}
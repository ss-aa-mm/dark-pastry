using Enemies;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Agata") )//|| GenericEnemy.GetAnimatorHash() != AttackHash)
            return;

        var enemy = GetComponentInParent<Enemy>();
        var agata = other.GetComponentInParent<AgataNew>();
        var enemyBody = agata.transform.GetComponentInParent<Rigidbody2D>();
        var agataBody = other.transform.GetComponentInParent<Rigidbody2D>();
        var newPosition = new Vector2();

        AgataNew.SetLife(-enemy.GetDamage());

        if (enemyBody.position.x > agataBody.position.x) //The enemy is to the right of Agata
        {
            if (agataBody.position.x >= 0)
                newPosition.x = agataBody.position.x * -5;
            else
                newPosition.x = agataBody.position.x * 5;
        }
        else //The enemy is to the left of Agata
        {
            if (agataBody.position.x >= 0)
                newPosition.x = agataBody.position.x * 5;
            else
                newPosition.x = agataBody.position.x * -5;
        }

        if (enemyBody.position.y > agataBody.position.y) //The enemy is above Agata
        {
            if (agataBody.position.y >= 0)
                newPosition.y = agataBody.position.y * -5;
            else
                newPosition.y = agataBody.position.y * 5;
        }
        else //The enemy is below Agata
        if (agataBody.position.y >= 0)
            newPosition.y = agataBody.position.y * 5;
        else
            newPosition.y = agataBody.position.y * -5;

        agataBody.AddForce(newPosition, ForceMode2D.Impulse);
    }
}
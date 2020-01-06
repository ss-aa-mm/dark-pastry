using Enemies;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static AudioClip _enemyHit;

    private void Awake()
    {
        _enemyHit = Resources.Load<AudioClip>("Assets/Resources/SOUND/sicuri/pain25_1.wav");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        HitEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        HitEnemy(other);
    }

    private void HitEnemy(Component other)
    {
        var enemy = other.GetComponentInParent<Enemy>();
        var enemyBody = other.transform.GetComponentInParent<Rigidbody2D>();
        var agataBody = GetComponentInParent<AgataNew>().transform.GetComponent<Rigidbody2D>();
        var newPosition = new Vector2();

        //SoundManager.instance.PlaySingle(_enemyHit);

        enemy.health--;
        if (enemy.health <= 0)
        {
            Fungus.Flowchart.BroadcastFungusMessage("Boss_01_died");
            enemy.OnDeath();
            enemy.isActive = false;
            enemy.animator.SetTrigger(enemy.Death);
            Destroy(other.gameObject);
            return;
        }

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
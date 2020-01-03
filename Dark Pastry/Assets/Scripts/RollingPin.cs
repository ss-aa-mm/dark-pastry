using Enemies;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") || AgataNew.GetAnimatorHash() != AttackHash)
            return;

        other.GetComponent<NewEnemy>().OnHit();
        other.GetComponent<Rigidbody>().AddForce(transform.position * -50);
    }
}
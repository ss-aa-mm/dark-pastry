using UnityEngine;

public abstract class HealingObject : MonoBehaviour
{
    protected float HealingFactor;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.CompareTag("Agata")) return;
        AgataNew.SetLife(HealingFactor);
        Destroy(gameObject);
    }
}

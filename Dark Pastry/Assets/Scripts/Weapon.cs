using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Agata"))
            return;

        AgataNew.SetLife(-1);
        HitAgata(other);
    }

    private void HitAgata(Collision2D agata)
    {
        var enemyBody = transform.GetComponent<Rigidbody2D>();
        var agataBody = agata.transform.GetComponent<Rigidbody2D>();
        var newPosition = new Vector2();

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

        //Push Agata away after hit
        agataBody.AddForce(newPosition, ForceMode2D.Impulse);
    }
}

using UnityEngine;

public abstract class SmartEnemy : MonoBehaviour
{
    public bool dropsHeart;

    public bool dropsPocketItem;

    private const float Speed = 4f;

    private float _direction;

    private float _iterationCounter;

    public GameObject agata;

    private void Update()
    {
        var unit = Speed * Time.deltaTime;
        _iterationCounter++;
        if (_iterationCounter > 10)
        {
            _iterationCounter = 0;
            transform.position = Vector2.MoveTowards(transform.position, agata.transform.position, unit);
        }
    }

    public abstract void Collapse();
}
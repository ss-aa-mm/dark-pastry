using System;
using System.Collections;
using UnityEngine;

public abstract class HighlightableObject : MonoBehaviour
{
    protected Sprite HighlightedSprite;
    protected Sprite NormalSprite;
    protected Sprite WrongSprite;
    private bool _highlighted;
    private Collider2D[] _collisions;
    private SpriteRenderer _renderer;

    protected void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        AssignReferences();
    }

    public bool IsHighlighted()
    {
        return _highlighted;
    }

    private void Highlight()
    {
        _highlighted = true;
        _renderer.sprite = HighlightedSprite;
    }

    public void Normal()
    {
        _highlighted = false;
        _renderer.sprite = NormalSprite;
    }

    private void DetectAgata()
    {
        var position = transform.position;
        try
        {
            var agataFound = false;
            _collisions = Physics2D.OverlapCircleAll(position, 0.5f);
            foreach (var coll in _collisions)
            {
                if (!coll.gameObject.CompareTag("Agata")) continue;
                agataFound = true;
                if (AgataNew.IsInteractiveObjectSet()) continue;
                Highlight();
                AgataNew.SetInteractiveObject(this);
            }

            if (agataFound || AgataNew.GetInteractiveObject() != this) return;
            Normal();
            AgataNew.SetInteractiveObject(null);
        }
        catch (ArgumentNullException e)
        {
            Normal();
            if (AgataNew.GetInteractiveObject() == this)
            {
                AgataNew.SetInteractiveObject(null);
            }
        }
    }

    private void Update()
    {
        DetectAgata();
    }
    protected IEnumerator WrongAnimation()
    {
        for (var i = 0; i < 2; i++)
        {
            _renderer.sprite = WrongSprite;
            yield return new WaitForSeconds(0.1f);
            _renderer.sprite = _highlighted ? HighlightedSprite : NormalSprite;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public abstract void Interact();

    protected abstract void AssignReferences();
}

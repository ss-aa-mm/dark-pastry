using LevelScripts;
using UnityEngine;

public class EnemyEgg : GenericEnemy
{
    public override void Collapse(GenericLevel level)
    {
        if (dropsHeart)
            Instantiate(UiMechanics.Heart, transform.position, Quaternion.identity);
        if (dropsPocketItem)
            Instantiate(UiMechanics.DropEgg, transform.position, Quaternion.identity);
        Destroy(gameObject);
        level.EnemyKilled();
    }
}

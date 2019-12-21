using UnityEngine;

public class NewEgg : NewEnemy
{
    protected override void AssignReferences()
    {
        DamageInflicted = 0.5f;
        ItemDropped = Resources.Load<GameObject>("Prefabs/DroppedEgg");
    }

    protected override void MovementPattern()
    {
        
    }
}

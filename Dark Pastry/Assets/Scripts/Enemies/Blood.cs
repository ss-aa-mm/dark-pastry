using UnityEngine;

namespace Enemies
{
    public class Blood : Enemy
    {
        protected override void AssignReferences()
        {
            health = 1f;
            DamageInflicted = 0.5f;
            MovementTime = 0.1f;
            Speed = 1f;
            ItemDropped = Resources.Load<GameObject>("Prefabs/Items/DroppedBlood");
        }

        protected override void MovementPattern()
        {
            CrossMovement();
        }
    }
}

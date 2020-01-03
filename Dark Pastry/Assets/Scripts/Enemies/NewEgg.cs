using UnityEngine;

namespace Enemies
{
    public class NewEgg : NewEnemy
    {
        private int _frames;
        private int _xAxis;
        private int _yAxis;

        protected override void AssignReferences()
        {
            Health = 1f;
            DamageInflicted = 0.5f;
            MovementTime = 0.5f;
            Unit = 0.25f;
            ItemDropped = Resources.Load<GameObject>("Prefabs/DroppedEgg");
        }

        protected override void MovementPattern()
        {
            MoveRandomly();
        }
    }
}
using UnityEngine;

namespace Enemies
{
    public class Coffee : NewEnemy
    {
        private int _frames;
        private int _xAxis;
        private int _yAxis;

        protected override void AssignReferences()
        {
            Health = 1f;
            DamageInflicted = 0.5f;
            MovementTime = 0.2f;
            Speed = 3f;
            ItemDropped = Resources.Load<GameObject>("Prefabs/DroppedEgg");
        }

        protected override void MovementPattern()
        {
            MoveRandomly();
        }
    }
}
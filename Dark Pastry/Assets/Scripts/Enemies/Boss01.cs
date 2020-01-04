using UnityEngine;

namespace Enemies
{
    public class Boss01 : Enemy
    {
        private int _frames;
        private int _xAxis;
        private int _yAxis;

        protected override void AssignReferences()
        {
            Health = 5f;
            DamageInflicted = 0.5f;
            MovementTime = 0.1f;
            Speed = 1f;
            ItemDropped = Resources.Load<GameObject>("Prefabs/DroppedEgg");
        }

        protected override void MovementPattern()
        {
            Chase();
        }
    }
}
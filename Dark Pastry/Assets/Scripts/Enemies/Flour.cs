﻿using UnityEngine;

namespace Enemies
{
    public class Flour : Enemy
    {
        private int _frames;
        private int _xAxis;
        private int _yAxis;

        protected override void AssignReferences()
        {
            health = 1f;
            DamageInflicted = 0.5f;
            MovementTime = 0.1f;
            Speed = 1f;
            ItemDropped = Resources.Load<GameObject>("Prefabs/Items/DroppedFlour");
        }

        protected override void MovementPattern()
        {
            MoveRandomly();
        }
    }
}
using System;
using UnityEngine;

namespace LevelScripts
{
    public class Level0 : GenericLevel
    {
        public GameObject door;

        public void Awake()
        {
            NextLevel = "Level_1";
        }

        public override void EnemyKilled()
        {
            OpenDoor(door);
        }

        public override void ItemPlaced()
        {
            throw new NotImplementedException();
        }

        public override void ItemTaken()
        {
            throw new NotImplementedException();
        }
    }
}

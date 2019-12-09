using UnityEngine;

namespace LevelScripts
{
    public class Level1 : GenericLevel
    {
        public GameObject door;

        private float _eggPlaced;

        public void Awake()
        {
            NextLevel = "Level_2";
        }


        public override void EnemyKilled()
        {
            throw new System.NotImplementedException();
        }

        public override void ItemPlaced()
        {
            _eggPlaced++;
            if (_eggPlaced.Equals(5))
            {
                OpenDoor(door);
            }
        }

        public override void ItemTaken()
        {
            _eggPlaced--;
            if (_eggPlaced < 5)
            {
                CloseDoor(door);
            }
        }
    }
}

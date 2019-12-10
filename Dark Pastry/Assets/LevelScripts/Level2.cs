using UnityEngine;

namespace LevelScripts
{
    public class Level2 : GenericLevel
    {
        public GameObject door;
        private GameObject _boss;
        private int _killsCounter;
        private int _eggPlaced;
        private bool _bossBeaten;
        public void Awake()
        {
            NextLevel = "FinishGame";
            CurrentLevel = "Level_2";
            _boss = Resources.Load<GameObject>("Prefabs/Relatives/Boss_01");
        }

        public override void EnemyKilled()
        {
            _killsCounter++;
            if (_killsCounter == 5)
                BossFight();
            if (_killsCounter == 6)
            {
                _bossBeaten = true;
                if (_eggPlaced == 5)
                {
                    OpenDoor(door);
                    Agata.AgataAnimator.SetTrigger("dance");
                }
            }
                    
        }

        public override void ItemPlaced()
        {
            _eggPlaced++;
            if (_eggPlaced.Equals(5) && _bossBeaten)
            {
                OpenDoor(door);
                Agata.AgataAnimator.SetTrigger("dance");
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

        private void BossFight()
        {
            Instantiate(_boss, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}

using UnityEngine;

namespace LevelScripts
{
    public abstract class GenericLevel : MonoBehaviour
    {

        protected bool LevelCleared;
        protected string NextLevel;
        public abstract void EnemyKilled();
        public abstract void ItemPlaced();
        public abstract void ItemTaken();

        public bool IsClear()
        {
            return LevelCleared;
        }

        public string GetNext()
        {
            return NextLevel;
        }

        protected void OpenDoor(GameObject door)
        {
            LevelCleared = true;
            var states = door.GetComponentsInChildren<EasyNotesGO>(true);
            states[0].gameObject.SetActive(false);
            states[1].gameObject.SetActive(true);
        }

        protected void CloseDoor(GameObject door)
        {
            LevelCleared = false;
            var states = door.GetComponentsInChildren<EasyNotesGO>(true);
            states[0].gameObject.SetActive(true);
            states[1].gameObject.SetActive(false);
        }
    }
}

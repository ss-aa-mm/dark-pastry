using UnityEngine;

namespace Items
{
    public class Key : DropItem
    {
        public Color color;
        protected override void AssignReferences()
        {
            Type = DropItems.Key;
            NormalSprite = Resources.Load<Sprite>("Key/Normal");
            HighlightedSprite = Resources.Load<Sprite>("Key/Selected");
            WrongSprite = Resources.Load<Sprite>("Key/Wrong");
        }
    }
}

using UnityEngine;

namespace Items
{
    public class DropBlueberry : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropBlueberry/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropBlueberry/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropBlueberry/Wrong");
            Type = DropItems.Blueberry;
        }
    }
}

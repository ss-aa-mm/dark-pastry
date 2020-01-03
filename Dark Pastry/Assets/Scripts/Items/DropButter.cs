using UnityEngine;

namespace Items
{
    public class DropButter : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropButter/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropButter/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropButter/Wrong");
            Type = DropItems.Butter;
        }
    }
}

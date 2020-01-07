using UnityEngine;

namespace Items
{
    public class DropBlood : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropBlood/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropBlood/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropBlood/Wrong");
            Type = DropItems.Blood;
        }
    }
}

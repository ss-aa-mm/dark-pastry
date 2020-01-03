using UnityEngine;

namespace Items
{
    public class DropMilk : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropMilk/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropMilk/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropMilk/Wrong");
            Type = DropItems.Milk;
        }
    }
}

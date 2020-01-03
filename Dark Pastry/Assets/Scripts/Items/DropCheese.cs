using UnityEngine;

namespace Items
{
    public class DropCheese : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropCheese/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropCheese/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropCheese/Wrong");
            Type = DropItems.Cheese;
        }
    }
}

using UnityEngine;

namespace Items
{
    public class DropBiscuit : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropBiscuit/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropBiscuit/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropBiscuit/Wrong");
            Type = DropItems.Biscuit;
        }
    }
}

using UnityEngine;

namespace Items
{
    public class DropDough : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropDough/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropDough/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropDough/Wrong");
            Type = DropItems.Dough;
        }
    }
}

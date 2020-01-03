using UnityEngine;

namespace Items
{
    public class DropSugar : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropSugar/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropSugar/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropSugar/Wrong");
            Type = DropItems.Sugar;
        }
    }
}

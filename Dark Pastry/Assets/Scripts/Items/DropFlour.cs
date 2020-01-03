using UnityEngine;

namespace Items
{
    public class DropFlour : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropFlour/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropFlour/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropFlour/Wrong");
            Type = DropItems.Flour;
        }
    }
}

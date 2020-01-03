using UnityEngine;

namespace Items
{
    public class DropWhippedCream : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropWhippedCream/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropWhippedCream/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropWhippedCream/Wrong");
            Type = DropItems.WhippedCream;
        }
    }
}

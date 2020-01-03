using UnityEngine;

namespace Items
{
    public class DropChocolate : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropChocolate/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropChocolate/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropChocolate/Wrong");
            Type = DropItems.Chocolate;
        }
    }
}

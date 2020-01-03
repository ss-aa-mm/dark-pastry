using UnityEngine;

namespace Items
{
    public class DropCoffee : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropCoffee/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropCoffee/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropCoffee/Wrong");
            Type = DropItems.Coffee;
        }
    }
}

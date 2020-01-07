using UnityEngine;

namespace Items
{
    public class DropEgg : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropEgg/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropEgg/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropEgg/Wrong");
            Type = DropItems.Egg;
        }
    }
}

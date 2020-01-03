using UnityEngine;

namespace Items
{
    public class DropCherry : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropCherry/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropCherry/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropCherry/Wrong");
            Type = DropItems.Cherry;
        }
    }
}

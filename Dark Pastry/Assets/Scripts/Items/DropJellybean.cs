using UnityEngine;

namespace Items
{
    public class DropJellybean : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropJellybean/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropJellybean/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropJellybean/Wrong");
            Type = DropItems.Jellybean;
        }
    }
}

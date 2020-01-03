using UnityEngine;

namespace Items
{
    public class DropCarrot : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropCarrot/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropCarrot/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropCarrot/Wrong");
            Type = DropItems.Carrot;
        }
    }
}

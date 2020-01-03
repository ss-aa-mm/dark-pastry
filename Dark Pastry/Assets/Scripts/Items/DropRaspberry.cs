using UnityEngine;

namespace Items
{
    public class DropRaspberry : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropRaspberry/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropRaspberry/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropRaspberry/Wrong");
            Type = DropItems.Raspberry;
        }
    }
}

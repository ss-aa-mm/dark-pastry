using UnityEngine;

namespace Items
{
    public class DropLadyfinger : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropLadyfinger/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropLadyfinger/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropLadyfinger/Wrong");
            Type = DropItems.Ladyfinger;
        }
    }
}

using UnityEngine;

namespace Items
{
    public class DropLemon : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("Items/DropLemon/Selected");
            NormalSprite = Resources.Load<Sprite>("Items/DropLemon/Normal");
            WrongSprite = Resources.Load<Sprite>("Items/DropLemon/Wrong");
            Type = DropItems.Lemon;
        }
    }
}

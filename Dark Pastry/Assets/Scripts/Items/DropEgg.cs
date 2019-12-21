using UnityEngine;

namespace Items
{
    public class DropEgg : DropItem
    {
        protected override void AssignReferences()
        {
            HighlightedSprite = Resources.Load<Sprite>("DropEgg/Selected");
            NormalSprite = Resources.Load<Sprite>("DropEgg/Normal");
            //WrongSprite = Resources.Load<Sprite>("");
            Type = DropItems.Egg;
        }
    }
}

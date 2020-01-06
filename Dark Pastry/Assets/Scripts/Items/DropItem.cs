
namespace Items
{
    public abstract class DropItem : HighlightableObject
    {
        protected DropItems Type;
        public override void Interact()
        {
            var agataObject = AgataNew.GetItem();
            if (agataObject!=DropItems.None)
            {
                StartCoroutine(WrongAnimation());
                return;
            }
            AgataNew.SetItem(Type);
            Destroy(gameObject);
        }
    }
}

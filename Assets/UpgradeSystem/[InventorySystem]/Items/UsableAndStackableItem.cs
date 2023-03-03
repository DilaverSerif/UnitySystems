namespace InventorySystem.Items
{
    public class UsableAndStackableItem : StackableItem, IUseble
    {
        public void Use()
        {
            
        }

        public override void AddCount(int count)
        {
            
        }

        public UsableAndStackableItem(ItemData data) : base(data)
        {
        }
    }
}
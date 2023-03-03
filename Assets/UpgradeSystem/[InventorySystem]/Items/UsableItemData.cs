namespace InventorySystem.Items
{
    public class UsableItemData : Item, IUseble
    {
        public void Use()
        {
            
        }

        public UsableItemData(ItemData data) : base(data)
        {
            
        }
    }
}
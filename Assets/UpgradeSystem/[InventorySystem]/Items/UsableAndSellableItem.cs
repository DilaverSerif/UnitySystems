namespace InventorySystem.Items
{
    public class UsableAndSellableItem : SellableItem, IUseble
    {
        public void Use()
        {
            
        }

        public UsableAndSellableItem(ItemData data) : base(data)
        {
            
        }
    }
}
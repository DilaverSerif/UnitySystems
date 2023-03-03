namespace InventorySystem.Items
{
    public class UsableAndSellableAndStackableItem : UsableAndSellableItem, IStackable
    {
        public void AddCount(int count)
        {
            
        }

        public UsableAndSellableAndStackableItem(ItemData data) : base(data)
        {
        }
    }
}
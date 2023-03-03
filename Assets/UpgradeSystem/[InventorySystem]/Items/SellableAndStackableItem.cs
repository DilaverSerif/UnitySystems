namespace InventorySystem.Items
{
    public class SellableAndStackableItem : StackableItem, ISellable
    {
        public int Price;
        
        public void Sell()
        {
            
        }

        public override void AddCount(int count)
        {
            
        }

        public SellableAndStackableItem(ItemData data) : base(data)
        {
            Price = data.Price;
        }
    }
}
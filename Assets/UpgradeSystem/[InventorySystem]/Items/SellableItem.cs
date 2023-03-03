using Sirenix.OdinInspector;

namespace InventorySystem.Items
{
    public class SellableItem : Item, ISellable
    {
        [BoxGroup("Sellable Item")]
        public int Price;

        public void Sell()
        {
            
        }

        public SellableItem(ItemData data) : base(data)
        {
            Price = data.Price;
        }
    }
}
using Sirenix.OdinInspector;
using UpgradeSystem._InventorySystem_;

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
            Price = data.price;
        }
    }
}
using Sirenix.OdinInspector;
using UpgradeSystem._InventorySystem_;

namespace InventorySystem.Items
{
    public class StackableItem : Item, IStackable
    {
        [BoxGroup("Stackable Item")]
        public int CurrentCount;
        [BoxGroup("Stackable Item")]
        public int MaxCount;
        public virtual void AddCount(int count)
        {
            
        }

        public StackableItem(ItemData data) : base(data)
        {
            MaxCount = data.maxCount;
        }
    }
}
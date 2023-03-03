using InventorySystem.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    public class PlayerBag : SerializedMonoBehaviour
    {
        [FormerlySerializedAs("InventorySize")] [BoxGroup("Size")]
        public InventorySlot inventorySize;
        [FormerlySerializedAs("StatSize")] [BoxGroup("Size")]
        public InventorySlot statSize;
        
        [Title("Inventory")]
        public Inventory inventory;
        [Title("Stats")]
        public Inventory statInventory;
        [FormerlySerializedAs("Stats")] public ItemData[] stats;
        private void Awake()
        {
            inventory = ScriptableObject.CreateInstance<Inventory>();
            statInventory = ScriptableObject.CreateInstance<Inventory>();

            foreach (var stat in stats)
            {
                statInventory.AddItem(new Item(stat),statInventory.GetNullSlot());
            }
            
        }
        
        public bool GetStat(Item[] statData)
        {
            foreach (var item in statInventory.InventoryArray)
            {
                if (((StatItem)item).CurrentCount == 0) 
                    return false;
                
                foreach (var iData in statData)
                {
                    if (item.name != iData.name) continue;
                    ((StatItem)item).CurrentCount--;
                    return true;
                }
            }

            return false;
        }
        
        public bool GetItem(Item[] itemData)
        {
            foreach (var item in itemData)
            {
                var foundItem = inventory.SearchItem(item);
                if (foundItem != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
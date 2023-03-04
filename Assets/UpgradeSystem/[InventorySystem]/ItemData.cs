using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
namespace UpgradeSystem._InventorySystem_
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class ItemData: ScriptableObject
    {
        public int id;
        public string Name; 
        
        [ShowIf("@Type != ItemType.Stat")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        public Sprite icon;
        
        [ShowIf("@Type != ItemType.Stat")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        public GameObject prefab;
        
        [FormerlySerializedAs("MaxCount")] [ShowIf("@Type == ItemType.StackableItem || Type == ItemType.UsableAndStackableItem || Type == ItemType.SellableAndStackableItem || Type == ItemType.UsableAndSellableAndStackableItem")]
        public int maxCount;
        
        [ShowIf("@Type == ItemType.SellableItem || Type == ItemType.SellableAndStackableItem || Type == ItemType.UsableAndSellableItem || Type == ItemType.UsableAndSellableAndStackableItem")]
        public int price;
        
        public ItemType type;
    }

    public enum ItemType
    {
        Stat,
        StackableItem,
        UsableItem,
        SellableItem,
        UsableAndSellableItem,
        UsableAndStackableItem,
        SellableAndStackableItem,
        UsableAndSellableAndStackableItem
    }
}
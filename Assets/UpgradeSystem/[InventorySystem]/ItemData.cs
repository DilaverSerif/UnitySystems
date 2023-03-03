using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class ItemData: ScriptableObject
    {
        public int ID;
        public string Name; 
        
        [ShowIf("@Type != ItemType.Stat")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        public Sprite Icon;
        [ShowIf("@Type != ItemType.Stat")]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        public GameObject Prefab;
        
        [ShowIf("@Type == ItemType.StackableItem || Type == ItemType.UsableAndStackableItem || Type == ItemType.SellableAndStackableItem || Type == ItemType.UsableAndSellableAndStackableItem")]
        public int MaxCount;
        
        [ShowIf("@Type == ItemType.SellableItem || Type == ItemType.SellableAndStackableItem || Type == ItemType.UsableAndSellableItem || Type == ItemType.UsableAndSellableAndStackableItem")]
        public int Price;
        
        public ItemType Type;
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
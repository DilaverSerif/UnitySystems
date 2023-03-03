using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Items
{
    [System.Serializable]
    public class Item
    {
        [BoxGroup("Item Data")]
        public int Id;
        [BoxGroup("Item Data")]
        public string Name;
        
        [PreviewField(50,ObjectFieldAlignment.Left)]
        [BoxGroup("Item Data")]
        public Sprite Icon;
        [PreviewField(25,ObjectFieldAlignment.Left)]
        [BoxGroup("Item Data")]
        public GameObject Prefab;
        
        public Item(ItemData data)
        {
            Id = data.ID;
            Name = data.Name;
            Icon = data.Icon;
            Prefab = data.Prefab;
        }
        
        public T GetItem<T>() where T : Item
        {
            return (T) this;
        }
        
    }
}
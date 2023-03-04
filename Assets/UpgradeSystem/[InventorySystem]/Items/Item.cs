using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UpgradeSystem._InventorySystem_;

namespace InventorySystem.Items
{
    [System.Serializable]
    public class Item
    {
        [BoxGroup("Item Data")]
        public int id;
        [BoxGroup("Item Data")]
        public string name;
        
        [PreviewField(50,ObjectFieldAlignment.Left)]
        [BoxGroup("Item Data")]
        public Sprite Icon;
        [PreviewField(25,ObjectFieldAlignment.Left)]
        [BoxGroup("Item Data")]
        public GameObject Prefab;
        
        public Item(ItemData data)
        {
            id = data.id;
            name = data.Name;
            Icon = data.icon;
            Prefab = data.prefab;
        }
        
        public T GetItem<T>() where T : Item
        {
            return (T) this;
        }
        
    }
}
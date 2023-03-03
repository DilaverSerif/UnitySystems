using System;
using InventorySystem.Items;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    [Serializable]
    public struct InventorySlot
    {
        public int X;
        public int Y;
        
        public InventorySlot(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    [CreateAssetMenu(fileName = "Inventory", menuName = "InventorySystem/Inventory", order = 0)]
    public class Inventory: SerializedScriptableObject
    {
        private InventorySlot inventorySize;
        [TableMatrix(HorizontalTitle = "Inventory Array", SquareCells = true, DrawElementMethod = "DrawElement")]
        public Item[,] InventoryArray;

        [ShowInInspector,ReadOnly]
        private InventorySlot currentSize;

        static Item DrawElement(Rect rect, Item item)
        {
            if (item == null)
            {
                GUI.DrawTexture(rect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/UpgradeSystem/Icons/NoIcon.png"));
                return null;
            }
            if(item.Icon.texture != null)
                GUI.DrawTexture(rect, item.Icon.texture);
            else GUI.DrawTexture(rect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/UpgradeSystem/Icons/NoIcon.png"));

            return item;
        }
        
        public Inventory(InventorySlot slot)
        {
            CreateInventory(ref slot);
        }

        [Button]
        private void CreateInventory(ref InventorySlot slot)
        {
            inventorySize = slot;
            InventoryArray = new Item[slot.X, slot.Y];
            currentSize = slot;
            Debug.Log("Inventory Created Size:" + inventorySize.X + "x" + inventorySize.Y + "y");
            for (var x = 0; x < inventorySize.X; x++)
            {
                for (var y = 0; y < inventorySize.Y; y++)
                {
                    InventoryArray[x,y] = null;
                }
            }
            
        }
        
        [Button]
        private void AddItemEditor(ItemData itemData, InventorySlot slot)
        {
            if (slot.X >= inventorySize.X || slot.Y >= inventorySize.Y)
            {
                Debug.LogError("Slot is out of range");
                return;
            }
            InventoryArray[slot.X, slot.Y] = new Item(itemData);
        }
        
        //Search item in inventory
        public Item SearchItem(Item itemData)
        {
            for (var x = 0; x < inventorySize.X; x++)
            {
                for (var y = 0; y < inventorySize.Y; y++)
                {
                    if (InventoryArray[x, y] == null)
                        continue;
                    
                    if (InventoryArray[x, y].name == itemData.name)
                    {
                        return InventoryArray[x, y];
                    }
                }
            }
            return null;
        }
        
        public InventorySlot GetNullSlot()
        {
            for (var x = 0; x < inventorySize.X; x++)
            {
                for (var y = 0; y < inventorySize.Y; y++)
                {
                    if (InventoryArray[x, y] == null)
                    {
                        return new InventorySlot(x,y);
                    }
                }
            }
            return new InventorySlot(-1,-1);
        }
        
        public void AddItem(Item item, InventorySlot slot)
        {
            if(!SlotIsEmpty(slot))
                return;
        
            InventoryArray[slot.X,slot.Y] = item;
        }
    
        public void RemoveItem(ItemData ıtemData, InventorySlot slot)
        {
            if(SlotIsEmpty(slot))
                return;
        
            InventoryArray[slot.X,slot.Y] = null;
        }
    
        private bool SlotIsEmpty(InventorySlot slot)
        {
            if (InventoryArray[slot.X,slot.Y] == null)
                return true;
            return false;
        }
    
        //All clear inventory
        public void ClearInventory()
        {
            for (var x = 0; x < inventorySize.X; x++)
            {
                for (var y = 0; y < inventorySize.Y; y++)
                {
                    InventoryArray[x,y] = null;
                }
            }
        }
    
    }
}
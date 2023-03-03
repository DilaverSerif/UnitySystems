using InventorySystem.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class Inventory
    {
        public Item SelectedItem;
        
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
        
        public Item[,] InventoryArray;
        public InventorySlot InventorySize;

        [TableMatrix(SquareCells = true)]
        public Sprite[,] EditorInventory;
        public Inventory(InventorySlot slot)
        {
            InventorySize = slot;
            InventoryArray = new Item[slot.X, slot.Y];
            Debug.Log("Inventory Created Size:" + InventorySize.X + "x" + InventorySize.Y + "y");
            for (var x = 0; x < InventorySize.X; x++)
            {
                for (var y = 0; y < InventorySize.Y; y++)
                {
                    InventoryArray[x,y] = null;
                }
            }
            
            EditorInventory = new Sprite[slot.X, slot.Y];
            
            for (var x = 0; x < InventorySize.X; x++)
            {
                for (var y = 0; y < InventorySize.Y; y++)
                {
                    if (InventoryArray[x, y] == null)
                    {
                        EditorInventory[x, y] = null;
                        continue;
                    }
                    
                    EditorInventory[x,y] = InventoryArray[x,y].Icon;
                }
            }
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
            for (var x = 0; x < InventorySize.X; x++)
            {
                for (var y = 0; y < InventorySize.Y; y++)
                {
                    InventoryArray[x,y] = null;
                }
            }
        }
    
    }
}
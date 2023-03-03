using InventorySystem.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem
{
    public class PlayerBag : SerializedMonoBehaviour
    {
        public ItemData testdata;
        public Inventory Inventory;

        [TableMatrix(SquareCells = true)]
        public Sprite[,] ItemIcon;
        
        public static PlayerBag Instance;
        private void Start()
        {
            Inventory = new Inventory(new Inventory.InventorySlot(3,3));
        }

        [Button]
        public void CreateData()
        {
            var testItem = new Item(testdata);
            
            if(testdata.Type == ItemType.StackableItem)
                testItem = new Items.StackableItem(testdata);
            
            if (testItem.GetItem<Items.StackableItem>() != null)
            {
                Debug.Log("Stackable Item");
            }
            else Debug.Log("Not Stackable Item");
            
            Inventory = new Inventory(new Inventory.InventorySlot(3,3));
            ItemIcon = new Sprite[3,3];
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ItemIcon[i, j] = Inventory.InventoryArray[i, j].Icon;
                }
            }
        }
        
    }
}
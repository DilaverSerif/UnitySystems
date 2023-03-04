using System;
using InventorySystem;
using InventorySystem.Items;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace UpgradeSystem._InventorySystem_
{
	[Serializable]
	public struct InventorySlot
	{
		public int x;
		public int y;

		public InventorySlot(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	[CreateAssetMenu(fileName = "Inventory", menuName = "InventorySystem/Inventory", order = 0)]
	public class Inventory : SerializedScriptableObject
	{
		[ShowInInspector, ReadOnly]
		private Vector2 InventorySize
		{
			get {
				return new Vector2(InventoryArray.GetLength(0), InventoryArray.GetLength(1));
			}
		}

		[TableMatrix(HorizontalTitle = "Inventory Array", SquareCells = true, DrawElementMethod = "DrawElement")]
		public Item[,] InventoryArray;
		
		[Button]
		private void CreateInventory(ref InventorySlot slot)
		{
			InventoryArray = new Item[slot.x, slot.y];
			Debug.Log("Inventory Created Size:" + InventorySize.x + "x" + InventorySize.y + "y");
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					InventoryArray[x, y] = null;
				}
			}

		}

		#region Editor

		private static Item DrawElement(Rect rect, Item item)
		{
			var nullIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/UpgradeSystem/Icons/NoIcon.png");
			var text     = item == null ? "Empty" : item.name;
			
			if(item != null)
				if(item.GetItem<StackableItem>() != null)
					text += " X" + item.GetItem<StackableItem>().CurrentCount;
			
			var image = item == null ? nullIcon : item.Icon.texture;

			GUIContent content = new GUIContent(text, image);
			if (item == null)
			{
				GUI.Label(rect, content);
				return null;
			}
			
			GUI.Label(rect, content);
			return item;
		}
		
		[Button]
		public void ClearInventoryEditor()
		{
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					InventoryArray[x, y] = null;
				}
			}
		}
		
		[Button]
		private void AddItemEditor(InventorySlot slot,int amount)
		{
			if (slot.x >= InventorySize.x || slot.y >= InventorySize.y)
			{
				Debug.LogError("Slot is out of range");
				return;
			}

			InventoryArray[slot.x, slot.y].GetItem<StackableItem>().CurrentCount += amount;
		}

		[Button]
		private void AddItemEditor(ItemData itemData, InventorySlot slot)
		{
			if (slot.x >= InventorySize.x || slot.y >= InventorySize.y)
			{
				Debug.LogError("Slot is out of range");
				return;
			}

			switch (itemData.type)
			{
				case ItemType.Stat:
					InventoryArray[slot.x, slot.y] = new StatItem(itemData);
					return;
				case ItemType.SellableItem:
					InventoryArray[slot.x, slot.y] = new SellableItem(itemData);
					return;
				case ItemType.UsableItem:
					InventoryArray[slot.x, slot.y] = new UsableItemData(itemData);
					return;
				case ItemType.StackableItem:
					InventoryArray[slot.x, slot.y] = new StackableItem(itemData);
					return;
				case ItemType.SellableAndStackableItem:
					InventoryArray[slot.x, slot.y] = new SellableAndStackableItem(itemData);
					return;
				case ItemType.UsableAndSellableItem:
					InventoryArray[slot.x, slot.y] = new UsableAndSellableItem(itemData);
					return;
				case ItemType.UsableAndStackableItem:
					InventoryArray[slot.x, slot.y] = new UsableAndStackableItem(itemData);
					return;
				case ItemType.UsableAndSellableAndStackableItem:
					InventoryArray[slot.x, slot.y] = new UsableAndSellableAndStackableItem(itemData);
					return;

			}

			Debug.LogError("Item type not found");
		}

		#endregion

		//Search item in inventory
		public InventorySlot GetNullSlot()
		{
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					if (InventoryArray[x, y] == null)
					{
						return new InventorySlot(x, y);
					}
				}
			}
			return new InventorySlot(-1, -1);
		}

		public void AddItem(Item item, InventorySlot slot)
		{
			if (!SlotIsEmpty(slot))
				return;

			InventoryArray[slot.x, slot.y] = item;
		}

		public void RemoveItem(Item ítemData)
		{
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					if (InventoryArray[x, y] == null)
						continue;

					if (InventoryArray[x, y].id == ítemData.id)
					{
						InventoryArray[x, y] = null;
						return;
					}
				}
			}
		}
		
		private bool SlotIsEmpty(InventorySlot slot)
		{
			if (InventoryArray[slot.x, slot.y] == null)
				return true;
			return false;
		}

		//All clear inventory
		public void ClearInventory()
		{
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					InventoryArray[x, y] = null;
				}
			}
		}
		
		public StackableItem UseStackableItemByID(int id, int amount = 1)
		{
			var item = GetItemByID(id) as StackableItem;
			
			if (item == null)
				return null;

			item.CurrentCount -= amount;
			return item;
		}
		
		public StackableItem UseStackableItemByID(int[] id, int amount = 1)
		{
			foreach (var theID in id)
			{
				var item = GetItemByID(theID) as StackableItem;
				
				if (item == null)
					continue;

				if (item.CurrentCount == 0)
					return null;
				
				item.CurrentCount -= amount;
				if(item.CurrentCount == 0)
					RemoveItem(item);
				return item;
			}
			
			Debug.Log("Item not found");
			return null;
		}

		private Item GetItemByID(int id)
		{
			for (var x = 0; x < InventorySize.x; x++)
			{
				for (var y = 0; y < InventorySize.y; y++)
				{
					if (InventoryArray[x, y] == null)
						continue;
					
					if (InventoryArray[x, y].id == id)
					{
						return InventoryArray[x, y];
					}
				}
			}
			
			Debug.Log("Item not found");
			return null;
		}

	}
}
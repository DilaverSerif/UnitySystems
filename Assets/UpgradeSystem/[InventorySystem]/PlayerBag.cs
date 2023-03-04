using System;
using System.Linq;
using InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UpgradeSystem._InventorySystem_
{
	public class PlayerBag : MonoBehaviour
	{
		[Title("Inventory")]
		public Inventory inventory;
		[Title("Stats")]
		public Inventory statInventory;

		// public InventoryShower inventoryShower;

		private void Awake()
		{
			inventory = Instantiate(inventory);
			statInventory = Instantiate(statInventory);
		}
		
		public bool HaveItem(ItemData[] items)
		{
			var id = items.Select(x => x.ID).ToArray();
			
			if(items[0].Type == ItemType.Stat)
				return statInventory.UseStackableItemByID(id) != null;
	 
			return inventory.UseStackableItemByID(id) != null;
		}
		
		public bool HaveItem(ItemData item)
		{
			if(item.Type == ItemType.Stat)
				return statInventory.UseStackableItemByID(item.ID) != null;
	 
			return inventory.UseStackableItemByID(item.ID) != null;
		}
	}

	[Serializable]
	public class InventoryShower
	{
		public Inventory[] inventory;
		public string[] inventoryList;
		public InventoryShower(Inventory[] inventory)
		{
			this.inventory = inventory;
		}

		public void UpdateInventory()
		{
			foreach (var inv in inventory)
			{
				foreach (var i in inv.InventoryArray) { }
			}
		}
	}
}
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
		
		private void Awake()
		{
			inventory = Instantiate(inventory);
			statInventory = Instantiate(statInventory);
		}
		
		public bool HaveItem(ItemData[] items)
		{
			var id = items.Select(x => x.id).ToArray();
			
			if(items[0].type == ItemType.Stat)
				return statInventory.UseStackableItemByID(id) != null;
	 
			return inventory.UseStackableItemByID(id) != null;
		}
		
		public bool HaveItem(ItemData item)
		{
			if(item.type == ItemType.Stat)
				return statInventory.UseStackableItemByID(item.id) != null;
	 
			return inventory.UseStackableItemByID(item.id) != null;
		}
	}
	
}
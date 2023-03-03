using System;
using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Items;
using UnityEngine;

namespace _GAME_.Scripts.UpgradeSystem
{
	public class UpgradeManager : MonoBehaviour
	{
		public List<RequirementsForUpgradeData> RequirementsForUpgradeData;
		public List<Upgrade> Upgrades;

		private void Awake()
		{
			if (RequirementsForUpgradeData == null) return;
			
			Upgrades = new List<Upgrade>();
		
			foreach (var data in RequirementsForUpgradeData)
			{
				Upgrades.Add(new Upgrade(data));
			}
		}

		#region Search 

		public Upgrade GetUpgradeWithID(int upgradeID)
		{
			return Upgrades.Find(upgrade => upgrade.UpgradeID == upgradeID);
		}
		
		public Upgrade GetUpgradeWithName(string upgradeName)
		{
			return Upgrades.Find(upgrade => upgrade.Name == upgradeName);
		}

		#endregion
		
		#region Level
		
		public bool ResetUpgradeLevel(Upgrade upgrade)
		{
			if (upgrade == null) return false;
			upgrade.UpgradeCurrentLevel = 0;
			
			return true;
		}
		
		#endregion

		#region Checker
		
		private bool CurrentUpgradeLevelIsMax(Upgrade upgrade)
		{
			return upgrade.UpgradeCurrentLevel >= upgrade.MaxLevel;
		}
		
		public bool IsNeedItem(Upgrade upgrade,ItemsItemNames itemName)
		{
			foreach (var requirementLevelArray in upgrade.RequirementsForUpgrade)
			{
				foreach (var requirementLevel in requirementLevelArray.RequirementsForUpgrade)
				{
					if (requirementLevel.ItemName == itemName)
						return requirementLevel.IsFinish;
				}
			}

			return false;
		}

		#endregion
		
		#region Data

		private void AddCount(ref Upgrade upgradeData,ref ItemsItemNames itemName,ref int count)
		{
			if (upgradeData.IsEmpty()) return;

			switch (upgradeData.AddItem(itemName, count))
			{
				case UpgradeState.FinishLevel:
					// upgradeData.GetCurrentRequirementsForUpgrade().InvokeEffect();
					Debug.Log("Level complete - upgrade");
					return;
				case UpgradeState.AddedItem:
					Debug.Log("Item added");
					break;
				case UpgradeState.NotFound:
					Debug.Log("Item not found");
					break;
				case UpgradeState.FinishUpgrade:
					Debug.Log("Upgrade complete");
					Upgrades.Remove(upgradeData);
					break;
				case UpgradeState.WrongItem:
					Debug.LogWarning("Wrong item");
					break;
				default:
					Debug.LogError("Upgrade state not found");
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void AddCount(ref Upgrade upgradeData,ref Item itemName,ref int count)
		{
			if (upgradeData.IsEmpty()) return;
			var item = itemName.GetItemEnum();
			switch (upgradeData.AddItem(item, count))
			{
				case UpgradeState.FinishLevel:
					// upgradeData.GetCurrentRequirementsForUpgrade().InvokeEffect();
					Debug.Log("Level complete - upgrade");
					return;
				case UpgradeState.AddedItem:
					Debug.Log("Item added");
					break;
				case UpgradeState.NotFound:
					Debug.Log("Item not found");
					break;
				case UpgradeState.FinishUpgrade:
					Debug.Log("Upgrade complete");
					Upgrades.Remove(upgradeData);
					break;
				case UpgradeState.WrongItem:
					Debug.LogWarning("Wrong item");
					break;
				default:
					Debug.LogError("Upgrade state not found");
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public void AddCountToUpgrade(Upgrade upgradeData,ItemsItemNames itemName,int count = 1)
		{
			if (upgradeData.IsEmpty()) return;
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}
		
		public void AddCountToUpgrade(int upgrade,ItemsItemNames itemName,int count = 1)
		{
			var upgradeData = GetUpgradeWithID(upgrade);
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}
		
		public void AddCountToUpgrade(string upgrade,ItemsItemNames itemName,int count = 1)
		{
			var upgradeData = GetUpgradeWithName(upgrade);
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}


		#region ByItem

		public void AddCountToUpgrade(Upgrade upgradeData,Item itemName,int count = 1)
		{
			if (upgradeData.IsEmpty()) return;
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}
		
		public void AddCountToUpgrade(int upgrade,Item itemName,int count = 1)
		{
			var upgradeData = GetUpgradeWithID(upgrade);
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}
		
		public void AddCountToUpgrade(string upgrade,Item itemName,int count = 1)
		{
			var upgradeData = GetUpgradeWithName(upgrade);
			
			AddCount(ref upgradeData,ref itemName,ref count);
		}

		#endregion
		
		#endregion
	}

	public static class UpgradeSystemExtension
	{
		public static ItemsItemNames GetItemEnum(this ItemData itemData)
		{
			return (ItemsItemNames) Enum.Parse(typeof(ItemsItemNames), itemData.Name);
		}
		
		public static ItemsItemNames GetItemEnum(this Item itemData)
		{
			return (ItemsItemNames) Enum.Parse(typeof(ItemsItemNames), itemData.name);
		}
		
		public static bool IsNeedItem(this Upgrade upgrade,ItemsItemNames itemName)
		{
			//return UpgradeManager.Instance.IsNeedItem(upgrade, itemName);
			return true;
		}
	}
}
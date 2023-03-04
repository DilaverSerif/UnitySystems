using System;
using System.Collections.Generic;
using _GAME_.Scripts.UpgradeSystem;
using InventorySystem.Items;
using UnityEngine;
using UpgradeSystem._InventorySystem_;
using UpgradeSystem._InventorySystem_.Resources.EnumStorage;

namespace UpgradeSystem
{
	public class UpgradeManager : MonoBehaviour
	{
		public List<RequirementsForUpgradeData> requirementsForUpgradeData;
		public List<Upgrade> upgrades;

		private void Awake()
		{
			if (requirementsForUpgradeData == null) return;
			
			upgrades = new List<Upgrade>();
		
			foreach (var data in requirementsForUpgradeData)
			{
				upgrades.Add(new Upgrade(data));
			}
		}

		#region Search 

		public Upgrade GetUpgradeWithID(int upgradeID)
		{
			var upgrade = upgrades.Find(upgrade => upgrade.upgradeID == upgradeID);
			if (upgrade != null) return upgrade;
			
			Debug.LogError("Upgrade with ID: " + upgradeID + " not found");
			return null;
		}
		
		public Upgrade GetUpgradeWithName(string upgradeName)
		{
			return upgrades.Find(upgrade => upgrade.name == upgradeName);
		}

		#endregion
		
		#region Level
		
		public bool ResetUpgradeLevel(Upgrade upgrade)
		{
			if (upgrade == null) return false;
			upgrade.upgradeCurrentLevel = 0;
			
			return true;
		}
		
		#endregion

		#region Checker
		
		private bool CurrentUpgradeLevelIsMax(Upgrade upgrade)
		{
			return upgrade.upgradeCurrentLevel >= upgrade.maxLevel;
		}

		#endregion
		
		#region Data

		
		private void AddCount(ref Upgrade upgradeData,ref ItemsItemNames itemName,ref int count)
		{
			if (upgradeData.IsEmpty())
			{
				Debug.LogWarning("Upgrade is empty");
				return;
			}

			switch (upgradeData.AddItemToUpgrade(itemName, count))
			{
				case UpgradeState.FinishLevel:
					// upgradeData.GetCurrentRequirementsForUpgrade().InvokeEffect();
					Debug.Log("Level complete - upgrade");
					return;
				case UpgradeState.AddedItem:
					Debug.Log( itemName+" Item added");
					break;
				case UpgradeState.NotFound:
					Debug.Log(itemName+" Item not found");
					break;
				case UpgradeState.FinishUpgrade:
					Debug.Log(itemName+" Upgrade complete");
					//upgrades.Remove(upgradeData);
					break;
				case UpgradeState.WrongItem:
					Debug.LogWarning(itemName+" Wrong item");
					break;
				case UpgradeState.NotNecessary:
					Debug.LogWarning(itemName+" Item not necessary");
					break;
				default:
					Debug.LogError("Upgrade state not found");
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void AddCount(ref Upgrade upgradeData,ref Item itemName,ref int count)
		{
			if (upgradeData.IsEmpty())
			{
				Debug.LogWarning("Upgrade is empty");
				return;
			}
			
			var item = itemName.GetItemEnum();
			AddCount(ref upgradeData,ref item,ref count);
		}
		
		public void AddCountToUpgrade(int upgrade,ItemData[] itemName,int count = 1)
		{
			var upgradeData = GetUpgradeWithID(upgrade);

			foreach (var item in itemName)
			{
				var itemEnum = item.GetItemEnum();
				AddCount(ref upgradeData,ref itemEnum,ref count);
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
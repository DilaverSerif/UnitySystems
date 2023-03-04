using System;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.UpgradeSystem;
using InventorySystem;
using InventorySystem.Items;
using UnityEngine;
using UnityEngine.Serialization;
using UpgradeSystem._InventorySystem_.Resources.EnumStorage;

namespace UpgradeSystem
{
	[DefaultExecutionOrder(1)]
	public class UpgradeManager : MonoBehaviour
	{
		[FormerlySerializedAs("RequirementsForUpgradeData")] public List<RequirementsForUpgradeData> requirementsForUpgradeData;
		[FormerlySerializedAs("Upgrades")] public List<Upgrade> upgrades;

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
			var upgrade = upgrades.Find(upgrade => upgrade.UpgradeID == upgradeID);
			if (upgrade != null) return upgrade;
			
			Debug.LogError("Upgrade with ID: " + upgradeID + " not found");
			return null;
		}
		
		public Upgrade GetUpgradeWithName(string upgradeName)
		{
			return upgrades.Find(upgrade => upgrade.Name == upgradeName);
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
					if (requirementLevel.itemName == itemName)
						return requirementLevel.IsFinish;
				}
			}

			return false;
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
					Debug.Log("Item added");
					break;
				case UpgradeState.NotFound:
					Debug.Log("Item not found");
					break;
				case UpgradeState.FinishUpgrade:
					Debug.Log("Upgrade complete");
					upgrades.Remove(upgradeData);
					break;
				case UpgradeState.WrongItem:
					Debug.LogWarning("Wrong item");
					break;
				case UpgradeState.NotNecessary:
					Debug.LogWarning("Item not necessary");
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
			switch (upgradeData.AddItemToUpgrade(item, count))
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
					upgrades.Remove(upgradeData);
					break;
				case UpgradeState.WrongItem:
					Debug.LogWarning("Wrong item");
					break;
				default:
					Debug.LogError("Upgrade state not found");
					throw new ArgumentOutOfRangeException();
			}
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
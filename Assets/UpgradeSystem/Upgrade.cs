using System;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.UpgradeSystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;
using UpgradeSystem._InventorySystem_.Resources.EnumStorage;
using Object = UnityEngine.Object;


namespace UpgradeSystem
{
	[Serializable]
	public class Upgrade
	{
		public string Name;
		public int UpgradeID;
		
		public int UpgradeCurrentLevel;
		public int MaxLevel;
	
		public List<RequirementLevelArray> RequirementsForUpgrade;
		public UnityEvent<int> UpgradeEffect;

		public Upgrade(RequirementsForUpgradeData data)
		{
			if (data == null) return;

			var duplicate = Object.Instantiate(data);
			Name = duplicate.Name;
			UpgradeID = duplicate.UpgradeID;
			MaxLevel = duplicate.RequirementsForUpgrade.Count;
			RequirementsForUpgrade = duplicate.RequirementsForUpgrade;
		}

		public UpgradeState ThisIsNeed(ItemsItemNames itemName)
		{
			var currentReq = GetCurrentRequirementsForUpgrade();
			if (currentReq == null) return UpgradeState.NotFound;
			
			foreach (var upgradeItem in currentReq.RequirementsForUpgrade)
			{
				if (upgradeItem.itemName == itemName)
				{
					if (upgradeItem.IsFinish) 
						return UpgradeState.NotNecessary;
					return UpgradeState.Necessary;
				}
			}
			
			return UpgradeState.NotNecessary;
		}
		
		public UpgradeState ThisIsNeed(ItemsItemNames[] itemName)
		{
			var currentReq = GetCurrentRequirementsForUpgrade();
			if (currentReq == null) return UpgradeState.NotFound;
			
			foreach (var upgradeItem in itemName)
			{
				return ThisIsNeed(upgradeItem);
			}
			
			return UpgradeState.NotNecessary;
		}
		
		public UpgradeState ThisIsNeed(ItemData[] itemName)
		{
			var currentReq = GetCurrentRequirementsForUpgrade();
			if (currentReq == null) return UpgradeState.NotFound;
			
			foreach (var upgradeItem in itemName)
			{
				return ThisIsNeed(upgradeItem.GetItemEnum());
			}
			
			return UpgradeState.NotNecessary;
		}
		
		public UpgradeState AddItemToUpgrade(ItemsItemNames itemName,int amount)
		{
			var currentRequirements = GetCurrentRequirementsForUpgrade();
			if (currentRequirements == null) 
				return UpgradeState.NotFound;
			
			foreach (var upgradeItem in currentRequirements.RequirementsForUpgrade)
			{
				switch (upgradeItem.AddItemRequirement(ref itemName, amount))
				{
					case RequirementLevel.RequirementType.AddedItem:
						return UpgradeState.AddedItem;
					
					case RequirementLevel.RequirementType.NotAdded:
						if(currentRequirements.RequirementsForUpgrade.Last() == upgradeItem)
							return UpgradeState.WrongItem;
						return UpgradeState.NotNecessary;
					
					case RequirementLevel.RequirementType.FinishRequirement:
						if (AllRequirementsFinish())
							return UpgradeState.FinishUpgrade;
						return UpgradeState.FinishLevel;
					
					default:
						return UpgradeState.NotFound;
				}
			}

			Debug.Log("Not Found");
			return UpgradeState.NotFound;
		}
		
		
		
		private bool AllRequirementsFinish() //Upgrade Bitti mi
		{
			var currentRequirements = GetRequirementsForUpgrade(UpgradeCurrentLevel);
			
			if (currentRequirements == null) 
				return false;
			
			foreach (var upgradeItem in currentRequirements.RequirementsForUpgrade)
			{
				if (!upgradeItem.IsFinish) 
					return false;
			}
			
			UpgradeEffect.Invoke(UpgradeCurrentLevel);
			UpgradeCurrentLevel++;

			if (UpgradeCurrentLevel >= MaxLevel)
				return true;
			
			return false;
		}

		public bool IsEmpty()
		{
			return RequirementsForUpgrade.Count == 0;
		}
		
		public RequirementLevelArray GetCurrentRequirementsForUpgrade()
		{
			return GetRequirementsForUpgrade(UpgradeCurrentLevel);
		}
		
		private RequirementLevelArray GetRequirementsForUpgrade(int level)
		{
			return RequirementsForUpgrade[level];
		}
		
	}

	public enum UpgradeState
	{
		FinishLevel,
		FinishUpgrade,
		AddedItem,
		NotFound,
		NotNecessary,
		Necessary,
		WrongItem
	}
}
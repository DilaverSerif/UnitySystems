using System;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.UpgradeSystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UpgradeSystem._InventorySystem_;
using UpgradeSystem._InventorySystem_.Resources.EnumStorage;
using Object = UnityEngine.Object;


namespace UpgradeSystem
{
	[Serializable]
	public class Upgrade
	{
		public string name;
		public int upgradeID;
		
		public int upgradeCurrentLevel;
		public int maxLevel;
	
		public List<RequirementLevelArray> requirementsForUpgrade;
		public UnityEvent<int> upgradeEffect;

		public Upgrade(RequirementsForUpgradeData data)
		{
			if (data == null) return;

			var duplicate = Object.Instantiate(data);
			name = duplicate.requirementName;
			upgradeID = duplicate.upgradeID;
			maxLevel = duplicate.requirementsForUpgrade.Count;
			requirementsForUpgrade = duplicate.requirementsForUpgrade;
		}

		public UpgradeState ThisIsNeed(ItemsItemNames itemName)
		{
			var currentReq = GetCurrentRequirementsForUpgrade();
			if (currentReq == null) return UpgradeState.NotFound;

			foreach (var upgradeItem in currentReq.RequirementsForUpgrade)
			{
				if (upgradeItem.IsFinish)
					continue;
				
				if (upgradeItem.itemName == itemName)
				{
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
				var check = ThisIsNeed(upgradeItem.GetItemEnum());
				if(check == UpgradeState.Necessary)
					return UpgradeState.Necessary;
					
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
						continue;
					
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
			var currentRequirements = GetRequirementsForUpgrade(upgradeCurrentLevel);
			if (currentRequirements == null) 
				return false;

			foreach (var upgradeItem in currentRequirements.RequirementsForUpgrade)
			{
				if (!upgradeItem.IsFinish) 
					return false;
			}
			
			upgradeEffect.Invoke(upgradeCurrentLevel);
			upgradeCurrentLevel++;

			if (upgradeCurrentLevel >= maxLevel)
				return true;
			
			return false;
		}

		public bool IsEmpty()
		{
			return requirementsForUpgrade.Count == 0;
		}
		
		public RequirementLevelArray GetCurrentRequirementsForUpgrade()
		{
			return GetRequirementsForUpgrade(upgradeCurrentLevel);
		}
		
		private RequirementLevelArray GetRequirementsForUpgrade(int level)
		{
			if(level < requirementsForUpgrade.Count)
				return requirementsForUpgrade[level];
			return null;
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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;


namespace _GAME_.Scripts.UpgradeSystem
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
		
		public UpgradeState AddItem(ItemsItemNames itemName,int amount)
		{
			var currentRequirements = GetCurrentRequirementsForUpgrade();
			if (currentRequirements == null) 
				return UpgradeState.NotFound;
			
			foreach (var upgradeItem in currentRequirements.RequirementsForUpgrade)
			{
				switch (upgradeItem.IsRequirementMet(ref itemName, amount))
				{
					case RequirementLevel.RequirementType.Necessary:
						return UpgradeState.AddedItem;
					
					case RequirementLevel.RequirementType.NotNecessary:
						if(currentRequirements.RequirementsForUpgrade.Last() == upgradeItem)
							return UpgradeState.WrongItem;
						break;
					
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
		WrongItem
	}
}
using System;

namespace _GAME_.Scripts.UpgradeSystem
{
    [Serializable]
    public class RequirementLevel
    {
        public ItemsItemNames ItemName;
        public int RequiredAmount;
        public int CurrentAmount;
        public RequirementType IsRequirementMet(ref ItemsItemNames item,int count = 1)
        {
            if(ItemName != item | CurrentAmount >= RequiredAmount) 
                return RequirementType.NotNecessary;
			
            CurrentAmount += count;
			
            if (CurrentAmount >= RequiredAmount)
                return RequirementType.FinishRequirement;
			
            return RequirementType.Necessary;
        }
		
        public bool IsFinish => CurrentAmount >= RequiredAmount;
		
        public enum RequirementType
        {
            FinishRequirement,
            NotNecessary,
            Necessary
        }
    }
}
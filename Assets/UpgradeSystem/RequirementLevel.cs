using System;
using UnityEngine.Serialization;
using UpgradeSystem._InventorySystem_.Resources.EnumStorage;
namespace UpgradeSystem
{
    [Serializable]
    public class RequirementLevel
    {
        [FormerlySerializedAs("ItemName")] public ItemsItemNames itemName;
        [FormerlySerializedAs("RequiredAmount")] public int requiredAmount;
        [FormerlySerializedAs("CurrentAmount")] public int currentAmount;
        public RequirementType AddItemRequirement(ref ItemsItemNames item,int count = 1)
        {
            if(itemName != item | currentAmount >= requiredAmount) 
                return RequirementType.NotAdded;
			
            currentAmount += count;
			
            if (currentAmount >= requiredAmount)
                return RequirementType.FinishRequirement;
			
            return RequirementType.AddedItem;
        }
        
        public bool IsFinish
        {
            get {
                return currentAmount >= requiredAmount;
            }
        }

        public enum RequirementType
        {
            FinishRequirement,
            NotAdded,
            AddedItem
        }
    }
}
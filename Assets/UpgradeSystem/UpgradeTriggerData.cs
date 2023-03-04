using InventorySystem;
using UnityEngine;

namespace UpgradeSystem
{
    [CreateAssetMenu(fileName = "UpgradeTriggerData", menuName = "Upgrade System/Data/UpgradeTriggerData", order = 0)]
    public class UpgradeTriggerData: ScriptableObject
    {
        public int upgradeID;
        public float dropDelay;
        public ItemData[] needItems;
    }
}
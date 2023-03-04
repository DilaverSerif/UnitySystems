using UnityEngine;
using UpgradeSystem._InventorySystem_;

namespace UpgradeSystem.Example
{
    public class Building : UpgradeTrigger
    {
        public Transform[] upgradeParts;
        public PlayerBag playerBag;

        protected override void UpgradeFinished(int level)
        {
            Debug.Log(level);
            //upgradeParts[level].gameObject.SetActive(true);
        }
        
    }
}
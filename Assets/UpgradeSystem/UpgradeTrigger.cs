using System.Collections;
using UnityEngine;
using UpgradeSystem._InventorySystem_;

namespace UpgradeSystem
{
    public abstract class UpgradeTrigger: MonoBehaviour
    {
        private Coroutine upgradeCoroutine;
        public UpgradeTriggerData upgradeTriggerData;
        private WaitForSeconds waitForSDelay;
    
        private Upgrade targetUpgrade;
        public UpgradeManager upgradeManager;

        [SerializeField] 
        private bool finished = false;
        private void Awake()
        {
            waitForSDelay = new WaitForSeconds(upgradeTriggerData.dropDelay);
        }
        
        protected virtual void OnEnable()
        {
            if(finished) return;
            targetUpgrade = upgradeManager.GetUpgradeWithID(upgradeTriggerData.upgradeID);
            targetUpgrade.upgradeEffect.AddListener(UpgradeFinished);
        }

        protected virtual void OnDisable()
        {
            targetUpgrade.upgradeEffect.RemoveListener(UpgradeFinished);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBag playerBag))
            {
                Debug.Log("Player bag found");
                if(!CheckItem(ref playerBag)) return;
                if(upgradeCoroutine != null) return;
            }

            upgradeCoroutine = StartCoroutine(Upgrade(playerBag));
        }
    
        protected void OnTriggerExit(Collider other)
        {
            if(upgradeCoroutine != null)
                StopCoroutine(upgradeCoroutine);
        }

        protected virtual bool CheckItem(ref PlayerBag playerBag)
        {
            var check =playerBag.HaveItem(upgradeTriggerData.needItems) & targetUpgrade.ThisIsNeed(upgradeTriggerData.needItems) == UpgradeState.Necessary;
                
            if (check)
                return true;
            
            return false;
        }
    
        protected virtual IEnumerator Upgrade(PlayerBag playerBag)
        {
            while (CheckItem(ref playerBag)) //Gerekli itemler var mı?
            {
                upgradeManager.AddCountToUpgrade(upgradeTriggerData.upgradeID, upgradeTriggerData.needItems);
                yield return waitForSDelay;
            }
        
            upgradeCoroutine = null;
        }

        protected abstract void UpgradeFinished(int level);
    }
}
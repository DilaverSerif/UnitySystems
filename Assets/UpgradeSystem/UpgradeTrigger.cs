using System.Collections;
using _GAME_.Scripts.UpgradeSystem;
using InventorySystem;
using InventorySystem.Items;
using UnityEngine;
using UnityEngine.Serialization;

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
        targetUpgrade = upgradeManager.GetUpgradeWithID(upgradeTriggerData.upgradeID);
    }

    protected virtual void OnEnable()
    {
        if (finished) return;
        targetUpgrade.UpgradeEffect.AddListener(UpgradeFinished);
    }

    protected virtual void OnDisable()
    {
        targetUpgrade.UpgradeEffect.RemoveListener(UpgradeFinished);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerBag playerBag))
            if(!CheckItem(ref playerBag)) return;
        
        upgradeCoroutine = StartCoroutine(Upgrade(playerBag));
    }
    
    protected void OnTriggerExit(Collider other)
    {
        if(upgradeCoroutine != null)
            StopCoroutine(upgradeCoroutine);
    }

    protected virtual bool CheckItem(ref PlayerBag playerBag)
    {
        foreach (var itemData in upgradeTriggerData.needItems)
        {
            if (itemData.GetItem<StatItem>() != null)
                return playerBag.GetStat(upgradeTriggerData.needItems);
            return playerBag.GetItem(upgradeTriggerData.needItems);
        }

        return false;
    }
    
    protected virtual IEnumerator Upgrade(PlayerBag playerBag)
    {
        while (!CheckItem(ref playerBag))
        {
            foreach (var needItem in upgradeTriggerData.needItems)
            {
                upgradeManager.AddCountToUpgrade(upgradeTriggerData.upgradeID, 
                    needItem.GetItemEnum());
            }
            yield return waitForSDelay;
        }
        
        upgradeCoroutine = null;
    }
    
    public abstract void UpgradeFinished(int level);
}
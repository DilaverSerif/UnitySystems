using _GAME_.Scripts.UpgradeSystem;
using InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class Building : UpgradeTrigger
{
    public Transform[] upgradeParts;
    public override void UpgradeFinished(int level)
    {
        upgradeParts[level].gameObject.SetActive(true);
    }
    
    public PlayerBag playerBag;
    [Button]
    public void TestButton()
    {
        if(!CheckItem(ref playerBag)) return;
        StartCoroutine(Upgrade(playerBag));
    }
}
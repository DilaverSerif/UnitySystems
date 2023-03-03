using _GAME_.Scripts.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;

public class ExampleUpgradeTrigger : MonoBehaviour
{
   public int upgradeID;
   public ItemsItemNames itemName;
   private UpgradeManager upgradeManager;

   private void Awake()
   {
      upgradeManager = FindObjectOfType<UpgradeManager>();
   }

   [Button]
   public void Triger()
   {
      upgradeManager.AddCountToUpgrade(upgradeID,itemName);
   }
   
   private IEnumerator Start()
   {
      yield return new WaitForSeconds(1f);
      Sub();
   }

   private void Sub()
   {
      upgradeManager.GetUpgradeWithID(0).UpgradeEffect.AddListener(LevelUpdates);
   }

   private void LevelUpdates(int arg0)
   {
     Debug.Log("Level "+arg0);
   }
}

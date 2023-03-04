using System.Collections.Generic;
using UnityEngine;

namespace _GAME_.Scripts.UpgradeSystem
{
	[CreateAssetMenu(fileName = "RequirementsForUpgradeData", menuName = "UpgradeSystem/RequirementsForUpgradeData", order = 0)]
	public class RequirementsForUpgradeData : ScriptableObject
	{
		public string requirementName;
		public int upgradeID;
		public List<RequirementLevelArray> requirementsForUpgrade;
	}
}
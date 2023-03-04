using System;
using System.Collections.Generic;
using UpgradeSystem;

namespace _GAME_.Scripts.UpgradeSystem
{
    [Serializable]
    public class RequirementLevelArray
    {
        public int Level;
        public List<RequirementLevel> RequirementsForUpgrade;
    }
}
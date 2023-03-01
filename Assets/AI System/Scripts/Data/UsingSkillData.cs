using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AI_System.Scripts.Data
{
    [CreateAssetMenu(fileName = "AttackerWithSkill", menuName = "AI System/Attack System/AttackerWithSkill", order = 0)]
    public class UsingSkillData: ScriptableObject
    {
        [BoxGroup("Skill Data")]
        public GameObject SkillPrefab;
    
        [BoxGroup("Skill Data")]
        public string SkillName;
        [BoxGroup("Skill Data")]
        public float SkillDelay;
        [BoxGroup("Skill Data")]
        public float SkillRange;
        [BoxGroup("Skill Data")]
        public float SkillAngle;
    
        [BoxGroup("Skill Event")]
        public UnityEvent OnSpawnSkill;
        [BoxGroup("Skill Event")]
        public UnityEvent OnEndSkill;
    }
}
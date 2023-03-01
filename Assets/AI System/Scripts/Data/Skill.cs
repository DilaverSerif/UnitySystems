
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;

namespace Character
{
    [System.Serializable]
    public class Skill
    {
        public enum SkillStat
        {
            IsUsing,
            IsCoolTime,
            Ready
        }
        
        public string SkillName;
        
        [ProgressBar(0, @"SkillDelay")]
        public float CurrentCoolTime;
        
        public float SkillDelay;
        public float SkillRange;
        public float SkillAngle;
        public SkillStat _skillStat;
        
        private UsingSkillData skillData;
        private MonoBehaviour _monoBehaviour;
        public Skill(UsingSkillData skillData, MonoBehaviour monoBehaviour)
        {
            this.skillData = skillData;
            _monoBehaviour = monoBehaviour;
            SkillName = skillData.SkillName;
            SkillDelay = skillData.SkillDelay;
            SkillRange = skillData.SkillRange;
            SkillAngle = skillData.SkillAngle;
            
            CurrentCoolTime = SkillDelay;
            _skillStat = SkillStat.Ready;
        }
        
        public void SpawnSkill(Transform target,Quaternion rotation = default,Transform parent = null)
        {
            if(_skillStat == SkillStat.IsCoolTime) 
                return;

            Object.Instantiate(skillData.SkillPrefab, target.position, rotation,parent);
            skillData.OnSpawnSkill?.Invoke();
            _monoBehaviour.StartCoroutine(SkillCoolTime());
        }
        
        private IEnumerator SkillCoolTime()
        {
            _skillStat = SkillStat.IsCoolTime;
            CurrentCoolTime = SkillDelay;
            
            while (CurrentCoolTime > 0)
            {
                CurrentCoolTime -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            
            CurrentCoolTime = 0;
            _skillStat = SkillStat.Ready;
        }
        
    }
}
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Character;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Character
{
    public class AttackerWithSkill : Attacker<EnemyAnimationList>,ISkillable
    {
        [BoxGroup("Skill Data")]
        public UsingSkillData[] usingSkillData;
        [BoxGroup("Skill Data")]
        public List<Skill> skills = new List<Skill>();
        
        public void SkillInit()
        {
            foreach (var skill in usingSkillData)
                skills.Add(new Skill(skill, this));
        }
        
        public Skill GetReadySkill()
        {
            return skills.FirstOrDefault(skill => skill._skillStat == Skill.SkillStat.Ready);
        }
        
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        
            if(skills == null) return;
            
            Handles.color = Color.magenta;
            foreach (var skill in skills)
            {
                var thisTransform = transform;
                
                var position = thisTransform.position;
                var forward = thisTransform.forward;
            
                Handles.Label(position + (forward * skill.SkillRange) ,
                    skill.SkillName + "CoolDown:" + skill.CurrentCoolTime.ToString("0.00"), EditorStyles.boldLabel);
                Handles.DrawWireArc(position, Vector3.up, forward, skill.SkillAngle, skill.SkillRange, 7.5f);
                Handles.DrawWireArc(position, Vector3.up, forward, -skill.SkillAngle, skill.SkillRange, 7.5f);
            }
        }
        
    }

}

public interface IStateAttacker
{
    void ChangeState(string stateName);
}
using AI_System.Scripts.Data;
namespace Character
{
    public interface ISkillable
    {
        Skill GetReadySkill();
        void SkillInit();
    }
}
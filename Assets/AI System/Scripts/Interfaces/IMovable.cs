using _GAME_.Scripts.Character.Abstracs;
namespace AI_System.Scripts.Interfaces
{
    public interface IMovable
    {
        void Move();
        void Stop();
        void ChangeState(CharacterStates state);
    }
}
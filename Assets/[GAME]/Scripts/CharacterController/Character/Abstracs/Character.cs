using Sirenix.OdinInspector;
using UnityEngine;
namespace _GAME_.Scripts.Character.Abstracs
{
    public enum CharacterTypes
    {
        Friendly,
        Enemy,
        None
    }
    
    public enum CharacterStates
    {
        Idle,
        Move,
        Attack,
        Die,
        None
    }
    
    public abstract class CharacterBase: MonoBehaviour
    {
        [BoxGroup("Character")]
        public CharacterTypes CharacterType;
        [BoxGroup("Character")]
        public CharacterStates CharacterState;
        protected virtual void OnEnable()
        {
            OnSpawn();
        }
        
        protected virtual void OnDisable()
        {
            OnDeath();
        }

        protected abstract void OnDeath();
        protected abstract void OnSpawn();
    }
}
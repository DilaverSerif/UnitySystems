using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Interfaces;
using AI_System.Scripts.VisualScripting;
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
        Patrol,
        Run,
        ImmediatelyRun,
        Die
    }
    
    public abstract class CharacterBase: MonoBehaviour
    {
        [BoxGroup("Modeling")]
        public Transform Model3D;
        
        [Button("Create Model"), BoxGroup("Modeling")]
        public virtual void CreateModel()
        {
            var model = Instantiate(Model3D, transform);
           
            model.localPosition = Vector3.zero;
            model.localRotation = Quaternion.identity;
        }
        
        [BoxGroup("Character")]
        public CharacterTypes CharacterType;
        [BoxGroup("Character")]
        public CharacterStates CharacterState;
        
        protected IMovable Movable;
        protected IAttacker<CharacterStates> Attacker;
        protected IFinder<IDamageable> Finder;
        protected IDamageable Damageable;
        protected IStateMachine[] StateMachines;

        protected virtual void OnEnable()
        {
            Movable = TryGetComponent(out IMovable movable) ? movable : null;
            Attacker = TryGetComponent(out IAttacker<CharacterStates> attacker) ? attacker : null;
            Finder = TryGetComponent(out IFinder<IDamageable> finder) ? finder : null;
            Damageable = TryGetComponent(out IDamageable damageable) ? damageable : null;
            StateMachines = GetComponents<IStateMachine>();
            OnSpawn();
        }
        
        protected virtual void OnDisable()
        {
            OnDeath();
        }
        
        public virtual void ChangeState(CharacterStates state)
        {
            if(CharacterState == CharacterStates.ImmediatelyRun) return;
            if(CharacterState == state) return;
            
            CharacterState = state;

            foreach (var stateMachine in StateMachines)
            {
                stateMachine.ChangeState(state);
            }
            
        }

        protected abstract void OnDeath();
        protected abstract void OnSpawn();
    }
}
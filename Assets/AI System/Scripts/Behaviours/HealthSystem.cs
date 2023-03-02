using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace AI_System.Scripts.Behaviours
{
    [System.Serializable]
    public class HealthSystem:MonoBehaviour,IDamageable
    {
        [BoxGroup("Events")]
        public UnityEvent OnHealthChange;
        [BoxGroup("Events")]
        public UnityEvent OnHit;
        [BoxGroup("Events")]
        public UnityEvent OnDeath;
    
        [BoxGroup("Current Datas")]
        public int MaxHealth;
        [BoxGroup("Current Datas")]
        public int CurrentHealth;
        
        [BoxGroup("Current Datas"),ShowInInspector]
        public CharacterTypes CharacterType
        {
            get => CharacterTypes.Enemy;
        }
        
        public bool isDead => CurrentHealth <= 0;
        
        public void TakeDamage(ref IDamageable healthSystem, int damage,ref CharacterTypes characterType)
        {
            if(isDead) return;
            
            if(characterType == CharacterType)
                return;
            
            CurrentHealth -= damage;
            OnHealthChange?.Invoke();

            if (isDead)
                OnDeath?.Invoke();
            else
                OnHit?.Invoke();
        }
        public int Health
        {
            get {
                return CurrentHealth;
            }
        }
        
    }
}
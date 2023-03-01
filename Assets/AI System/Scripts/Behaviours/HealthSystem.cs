using _GAME_.Scripts.Character.Abstracs;
using _GAME_.Scripts.Character.Interfaces;
using Character;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _GAME_.Scripts.Character
{
    [System.Serializable]
    public class HealthSystem:MonoBehaviour,IDamageable,IInitializable
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
        
        [BoxGroup("Current Datas"),ReadOnly]
        public CharacterTypes CharacterType { get; set; }
        
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
        

        public void Initialize()
        {
            CurrentHealth = MaxHealth;
            CharacterType = GetComponent<CharacterBase>().CharacterType;
        }
    }
}
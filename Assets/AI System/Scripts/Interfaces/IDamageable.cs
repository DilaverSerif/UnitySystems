using System;
using _GAME_.Scripts.Character.Abstracs;
using UnityEngine;
namespace AI_System.Scripts.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(ref IDamageable healthSystem, int damage,ref CharacterTypes characterType);
        Transform transform { get; }
        int Health { get; }
        CharacterTypes CharacterType { get;}
    }
    
    public interface ISpecificDamageable<T>:IDamageable where T : Enum
    {
        void TakeDamage(ref ISpecificDamageable<T> healthSystem, int damage,ref CharacterTypes characterType);
    }
}
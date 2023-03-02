using System;
using AI_System.Scripts.Data;
using UnityEngine;
namespace AI_System.Scripts.Interfaces
{
    public interface IAttacker<T> where T : Enum
    {
        void CheckForAttack(IDamageable damageable);
        void ChangeState(AttackState<T> state);
    }
}
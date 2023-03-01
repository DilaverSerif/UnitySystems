using System;
using System.Collections;
using _GAME_.Scripts.Character.Data;
using Character;
using UnityEngine;

namespace _GAME_.Scripts.Character.Interfaces
{
    public interface IAttacker<T> where T : Enum
    {
        void CheckForAttack(IDamageable damageable,Vector3 targetDistance);
        void ChangeState(AttackState<T> state);
    }
}
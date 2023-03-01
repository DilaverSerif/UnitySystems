﻿using System;
using _GAME_.Scripts.Character.Datas;
using AI_System.Scripts.Abstracts;
using AI_System.Scripts.Interfaces;
using UnityEngine;
namespace AI_System.Scripts.Data
{
	public class SwordAttackState<T> : AttackState<T> where T : Enum
	{
		public T AttackAnimation;
		protected override void Attack(ref IDamageable healthDamageable)
		{
			animationSystem.PlayAnimation(AttackAnimation);
			if(StateAttackData.StopWhenAttacking)
				movable.Stop();
		}
		protected override bool CanHit(ref Vector3 target)
		{
			return GetDistance(ref target) < StateAttackData.AttackRange & 
			       GetAngle(ref target) < StateAttackData.AttackAngle;
		}
		
		public SwordAttackState(Attacker<T>.RefData data, StateAttackData<T> stateAttackData) : base(data, stateAttackData) { }
	}
}
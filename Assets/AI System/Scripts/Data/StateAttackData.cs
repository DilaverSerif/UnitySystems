using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AI_System.Scripts.Data
{
	public class StateAttackData<T> : ScriptableObject where T: Enum
	{
		[BoxGroup("Attack Values")]
		public int Damage;
		[BoxGroup("Attack Values")]
		public bool StopWhenAttacking;
		[BoxGroup("Attack Values")]
		public AttackStateTypes AttackStateType;
		[BoxGroup("Attack Values")]
		public T AttackAnim;

		[BoxGroup("Attack Times")]
		public float AttackLoadTime;
		// [BoxGroup("Attack Times")]
		// public float AttackInvokeTime;
		[BoxGroup("Attack Times")]
		public float AttackDelay;

		[BoxGroup("Attack Requirements")]
		public float AttackRange;
		[BoxGroup("Attack Requirements"),Range(0,180)]
		public float AttackAngle;
	}

	public enum AttackStateTypes
	{
		EnemySword,
		Sword,
		Arrow,
		Spell
	}
}
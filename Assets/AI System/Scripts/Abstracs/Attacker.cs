using System;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Character.Abstracs;
using _GAME_.Scripts.Character.Data;
using _GAME_.Scripts.Character.Datas;
using _GAME_.Scripts.Character.Interfaces;
using Character;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _GAME_.Scripts.Character
{
	[Serializable]
	public abstract class Attacker<T> : SerializedMonoBehaviour, IAttacker<T> where T : Enum
	{
		public struct RefData
		{
			public IMovable movable;
			public CharacterAnimationSystem<T> animationSystem;
			public CharacterTypes characterType;
			public Transform transform;

			public RefData(MonoBehaviour monoBehaviour)
			{
				movable = monoBehaviour.GetComponent<IMovable>();
				animationSystem = monoBehaviour.GetComponent<CharacterAnimationSystem<T>>();

				if (monoBehaviour.TryGetComponent(out CharacterBase characterBase))
					characterType = characterBase.CharacterType;
				else characterType = CharacterTypes.None;
				
				transform = monoBehaviour.GetComponent<Transform>();
			}
		}

		public List<StateAttackData<T>> StateAttackDatas;
		protected List<AttackState<T>> attackStates;
		
		[ShowInInspector, ReadOnly]
		protected AttackState<T> currentState;

		protected RefData refData;
		protected virtual void Setup()
		{
			refData = new RefData(this);
			attackStates = StateAttackDatas.CreateStateByType(ref refData);
		}

		protected virtual void Awake()
		{
			Setup();
		}
		
		public virtual void CheckForAttack(IDamageable damageable, Vector3 targetDistance)
		{
			if (currentState.CanAttack(ref targetDistance,damageable.CharacterType))
				 StartCoroutine(currentState.AttackCoroutine(damageable));
		}

		public virtual void ChangeState(AttackState<T> state)
		{
			if (currentState != null)
				attackStates.Add(currentState);

			foreach (var attackState in attackStates.Where(attackState => attackState == state))
			{
				currentState = attackState;
				attackStates.Remove(attackState);
				break;
			}

		}

		public virtual void OnDrawGizmos()
		{
			if (currentState == null) return;
			Handles.color = Color.red;
			Handles.DrawWireDisc(transform.position, Vector3.up, currentState.StateAttackData.AttackRange, 10f);
			Handles.color = Color.magenta;
			Handles.DrawWireArc(transform.position, Vector3.up, transform.forward, currentState.StateAttackData.AttackAngle, currentState.StateAttackData.AttackRange, 7.5f);
			Handles.DrawWireArc(transform.position, Vector3.up, transform.forward, -currentState.StateAttackData.AttackAngle, currentState.StateAttackData.AttackRange, 7.5f);
		}
	}


}
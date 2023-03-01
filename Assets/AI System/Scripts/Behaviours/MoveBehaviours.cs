using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Abstracts;
using AI_System.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AI_System.Scripts.Behaviours
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class MoveBehaviours : SerializedMonoBehaviour,IMovable
	{
		private NavMeshAgent agent;
		[ShowInInspector]
		public Dictionary<CharacterStates,NavMoveState> States;
		
		[ShowInInspector, ReadOnly]
		private NavMoveState currentState;
		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
			
			foreach (var state in States)
			{
				state.Value.Initialize(agent); 
			}
		}

		private void Start()
		{
			ChangeState(CharacterStates.Idle);
		}

		public void Move()
		{
			agent.SetDestination(currentState.GetDestination());
		}
		public void Stop()
		{
			agent.isStopped = true;
		}

		public void ChangeState(CharacterStates state)
		{
			if (currentState != null)
				return;
			currentState = States[state];
		}

	}
}

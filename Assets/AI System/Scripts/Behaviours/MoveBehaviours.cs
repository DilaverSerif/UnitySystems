using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Abstracts;
using AI_System.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine.AI;

namespace AI_System.Scripts.Behaviours
{
	public class MoveBehaviours : SerializedMonoBehaviour,IMovable
	{
		private NavMeshAgent agent;
		[ShowInInspector]
		public Dictionary<CharacterStates,NavMoveState> States;
		
		[ShowInInspector, ReadOnly]
		private NavMoveState currentState;
		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
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

		private void ChangeState(CharacterStates state)
		{
			currentState = States[state];
		}

	}
}

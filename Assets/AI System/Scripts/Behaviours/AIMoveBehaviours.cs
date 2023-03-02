using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Abstracts;
using AI_System.Scripts.Interfaces;
using AI_System.Scripts.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AI_System.Scripts.Behaviours
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class AIMoveBehaviours : SerializedMonoBehaviour,IMovable,IStateMachine
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
			currentState = States[state];
			
			agent.stoppingDistance = currentState.moveData.stoppingDistance;
			agent.speed = currentState.moveData.speed;
			agent.angularSpeed = currentState.moveData.rotationSpeed;
		}
	}
}

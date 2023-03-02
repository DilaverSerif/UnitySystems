using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Abstracts;
using UnityEngine;
using UnityEngine.AI;

namespace AI_System.Scripts.Data
{
	[CreateAssetMenu(fileName = "NavRunState", menuName = "AI System/States/NavRunState")]
	public class NavRunState : NavMoveState
	{
		private Vector3 spawnPoint;
		public float backPointRadius;
		public float waitTimeForNormal;
		
		private float currentWaitTime;
		
		public override Vector3 GetDestination()
		{
			if (IsWaitTimeOver())
			{
				EnemyBase.CharacterState = CharacterStates.Idle;
				currentWaitTime = waitTimeForNormal;
			}	
			
			return spawnPoint + Random.insideUnitSphere * backPointRadius;
		}
		
		private bool IsWaitTimeOver()
		{
			if(currentWaitTime > 0)
				currentWaitTime -= Time.fixedDeltaTime;
			return currentWaitTime <= 0;
		}

		public override void Initialize(NavMeshAgent agent)
		{
			base.Initialize(agent);
			spawnPoint = Transform.position;
			currentWaitTime = waitTimeForNormal;
		}

		public override bool ReachedDestination()
		{
			return Agent.remainingDistance <= moveData.stoppingDistance;
		}
		
	}
}
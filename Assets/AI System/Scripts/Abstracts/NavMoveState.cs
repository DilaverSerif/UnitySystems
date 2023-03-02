using _GAME_.Scripts.Character;
using AI_System.Scripts.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AI_System.Scripts.Abstracts
{
	public abstract class NavMoveState: ScriptableObject
	{
		public AIMoveData moveData;
		
		private Transform transform;
		protected Transform Transform
		{
			get {
				if(transform == null)
					transform = Agent.transform;
				return  transform;
			}
		}

		private Enemy enemyBase;
		protected Enemy EnemyBase
		{
			get {
				if(enemyBase == null)
					enemyBase = Agent.GetComponent<Enemy>();
				return  enemyBase;
			}
		}
		
		protected NavMeshAgent Agent;
		public abstract Vector3 GetDestination();
		
		public virtual void Initialize(NavMeshAgent agent)
		{
			Agent = agent;
		}
		
		public abstract bool ReachedDestination();
		public virtual void OnGizmos()
		{
			
		}

	}


}

using Character;
using UnityEngine;
using UnityEngine.AI;

namespace AI_System.Scripts.Abstracts
{
	public abstract class NavMoveState: ScriptableObject
	{
		private Transform _transform;
		protected Transform transform
		{
			get {
				if(_transform == null)
					_transform = Agent.transform;
				return  _transform;
			}
		}
		
		protected NavMeshAgent Agent;
		public abstract Vector3 GetDestination();
		public void Initialize(NavMeshAgent agent)
		{
			Agent = agent;
		}
	}


}

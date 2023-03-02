using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Abstracts;
using AI_System.Scripts.Interfaces;
using UnityEngine;

namespace AI_System.Scripts.Data
{
	[CreateAssetMenu(fileName = "NavMoveToTargetState", menuName = "AI System/States/NavMoveToTargetState")]
	public class NavMoveToTargetState : NavMoveState
	{
		public float distanceToTarget = 0.1f;
		private Transform target;

		private Transform Target
		{
			get
			{
				if(!target)
					target = Transform.GetComponent<IFinder<IDamageable>>().GetTarget();
				return target;
			}
		}
		
		public override Vector3 GetDestination()
		{
			if(ReachedDestination())
				return Transform.position;
			
			return Target.position;
		}
		
		public override bool ReachedDestination()
		{
			if(Target == null)
				return true;
		
			return Agent.remainingDistance <= distanceToTarget;
		}
	}


}
using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Interfaces;
using UnityEngine;
namespace AI_System.Scripts.Abstracts
{
	[CreateAssetMenu(fileName = "NavMoveToTargetState", menuName = "AI System/States/NavMoveToTargetState")]
	public class NavMoveToTargetState : NavMoveState
	{
		public float DistanceToTarget = 0.1f;

		private Transform _target;

		private Transform Target
		{
			get
			{
				if(!_target)
					_target = transform.GetComponent<IFinder<IDamageable>>().GetTarget();
				return _target;
			}
		}
		
		public override Vector3 GetDestination()
		{
			if(ReachedTarget())
				return transform.position;
			
			return Target.position;
		}
		
		private bool ReachedTarget()
		{
			if(Target == null)
				return true;
		
			return Agent.remainingDistance <= DistanceToTarget;
		}
	}
}
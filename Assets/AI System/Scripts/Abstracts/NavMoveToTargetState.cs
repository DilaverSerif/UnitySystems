using UnityEngine;
namespace AI_System.Scripts.Abstracts
{
	[CreateAssetMenu(fileName = "NavMoveToTargetState", menuName = "AI System/States/NavMoveToTargetState")]
	public class NavMoveToTargetState : NavMoveState
	{
		public float DistanceToTarget = 0.1f;
		public Transform Target;
		public override Vector3 GetDestination()
		{
			return Target.position;
		}
		
		private bool ReachedTarget()
		{
			return Vector3.Distance(transform.position, Target.position) < DistanceToTarget;
		}
	}
}
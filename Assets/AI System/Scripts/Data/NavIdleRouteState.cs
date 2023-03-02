using AI_System.Scripts.Abstracts;
using UnityEngine;
namespace AI_System.Scripts.Data
{
	[CreateAssetMenu(fileName = "NavIdleRouteState", menuName = "AI System/States/NavIdleRouteState", order = 0)]
	public class NavIdleRouteState : NavMoveState
	{
		public override Vector3 GetDestination()
		{
			return Transform.position;
		}
		public override bool ReachedDestination()
		{
			return true;
		}
	}
}
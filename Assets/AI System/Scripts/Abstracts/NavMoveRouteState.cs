using System;
using UnityEngine;
namespace AI_System.Scripts.Abstracts
{
	[CreateAssetMenu(fileName = "NavMoveRouteState", menuName = "AI System/States/NavMoveRouteState", order = 0)]
	public class NavMoveRouteState: NavMoveState
	{
		private Transform _transform;
		private Transform transform
		{
			get {
				if(_transform == null)
					_transform = Agent.transform;
				return  _transform;
			}
		}
		
		private Vector3 CurrentDestination
		{
			get {
				return Routes[currentRouteIndex].Position;
			}
		}

		[Serializable]
		public struct Route
		{
			public Vector3 Position;
		}
		
		public Route[] Routes;
		private int currentRouteIndex;
		public float DistanceToNextRoute = 0.1f;
		public float WaitTime;
		
		public override Vector3 GetDestination()
		{
			if (ReachedDestination())
			{
				currentRouteIndex = Routes.Length - 1 == currentRouteIndex ? 0 : currentRouteIndex + 1;
			}
			return CurrentDestination;
		}
		
		private bool ReachedDestination()
		{
			return Vector3.Distance(transform.position, CurrentDestination) < DistanceToNextRoute;
		}
		
		
	}
}
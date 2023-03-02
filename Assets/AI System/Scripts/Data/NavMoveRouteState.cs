using System;
using AI_System.Scripts.Abstracts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace AI_System.Scripts.Data
{

	[CreateAssetMenu(fileName = "NavMoveRouteState", menuName = "AI System/States/NavMoveRouteState", order = 0)]
	public class NavMoveRouteState: NavMoveState
	{
		private Vector3 CurrentDestination
		{
			get 
			{
				return routes[currentRouteIndex].position;
			}
		}

		[Serializable]
		public struct Route
		{
			public Vector3 position;
		}
		
		public Route[] routes;
		[ShowInInspector, ReadOnly]
		private int currentRouteIndex;
		public float distanceToNextRoute = 0.1f;
		public float waitTime;
		
		public override Vector3 GetDestination()
		{
			if (ReachedDestination())
				currentRouteIndex = routes.Length - 1 == currentRouteIndex ? 0 : currentRouteIndex + 1;
			
			return CurrentDestination;
		}
		
		public override bool ReachedDestination()
		{
			return Agent.remainingDistance <= distanceToNextRoute;
		}

		public override void Initialize(NavMeshAgent agent)
		{
			base.Initialize(agent);
			currentRouteIndex = 0;
		}

	}
}
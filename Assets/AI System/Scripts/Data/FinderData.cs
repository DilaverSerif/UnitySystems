using Character;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace _GAME_.Scripts.Character
{
	[CreateAssetMenu(menuName = "Finder/Data/Create FinderData", fileName = "FinderData", order = 0)]
	public class FinderData : ScriptableObject
	{
		[BoxGroup("Find Data")]
		public float Radius = 10f;
		[BoxGroup("Find Data")]
		public float Angle = 45f;
		[BoxGroup("Find Data")]
		public LayerMask TargetMask;
		
		
	}
}
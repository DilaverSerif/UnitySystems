using System;
using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AI_System.Scripts.Data
{
	[CreateAssetMenu(menuName = "AI System/Attack System/Create AttackSystemData", fileName = "AttackSystemData", order = 0)]
	public class AttackSystemData: SerializedScriptableObject
	{
		[Serializable]
		public struct AttackData
		{
			public CharacterTypes CharacterType;
			public CharacterTypes TargetType;
			public bool CanAttack;
		}

		public static AttackSystemData Instance => Resources.Load<AttackSystemData>("AttackSystemData");

		public List<AttackData> AttackDatas;
	}
}
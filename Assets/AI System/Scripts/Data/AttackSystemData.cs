using System;
using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using _GAME_.Scripts.Character.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME_.Scripts.Character.Data
{
	[CreateAssetMenu(menuName = "Attack System/Create AttackSystemData", fileName = "AttackSystemData", order = 0)]
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

public static class AttackSystemExtension
{
	public static bool CanAttack(this CharacterTypes characterType, CharacterTypes targetType)
	{
		var attackDatas = AttackSystemData.Instance.AttackDatas;
		
		foreach (var attackData in attackDatas)
		{
			if (attackData.CharacterType == characterType && attackData.TargetType == targetType)
				return attackData.CanAttack;
		}

		return false;
	}
}
using System;
using System.Collections.Generic;
using _GAME_.Scripts.Character.Data;
using _GAME_.Scripts.Character.Datas;

namespace _GAME_.Scripts.Character
{
	public static class StateExtensions
	{
		public static List<AttackState<T>> CreateStateByType<T> (this List<StateAttackData<T>> datas,ref Attacker<T>.RefData data) where T : Enum
		{
			var stateList = new List<AttackState<T>>();
        
			foreach (var stateAttackData in datas)
			{
				switch (stateAttackData.AttackStateType)
				{
					case AttackStateTypes.Sword:
						stateList.Add(new SwordAttackState<T>(data,stateAttackData));
						break;
					case AttackStateTypes.EnemySword:
						break;
					case AttackStateTypes.Arrow:
						break;
					case AttackStateTypes.Spell:
						break;
					default:
						return null;
				}
			}
        
			return stateList;
		}
	}
}
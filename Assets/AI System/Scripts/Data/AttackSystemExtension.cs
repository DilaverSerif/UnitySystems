using _GAME_.Scripts.Character.Abstracs;

namespace AI_System.Scripts.Data
{
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
}
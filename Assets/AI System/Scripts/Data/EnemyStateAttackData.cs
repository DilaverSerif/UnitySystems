using UnityEngine;

namespace _GAME_.Scripts.Character.Datas
{
	[CreateAssetMenu(menuName = "Attack State/Create EnemyStateAttackData", fileName = "State EnemyStateAttackData", order = 0)]
	public class EnemyStateAttackData : StateAttackData<EnemyAnimationList>
	{
		
	}
}

public enum PlayerAnimationList
{
	Idle,
	Attack,
	Attack2,
}
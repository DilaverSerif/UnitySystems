using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.Character
{
    [CreateAssetMenu(menuName = "AI System/Move System/Data/Create AI MoveData", fileName = "MoveData", order = 0)]
    public class AIMoveData: ScriptableObject
    {
        public float speed;
        public float rotationSpeed;
        public float stoppingDistance;
    }
    
    public enum CharacterAIMoveState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Flee,
        Dead
    }
}
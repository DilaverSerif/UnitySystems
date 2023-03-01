using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME_.Scripts.Character
{
    [CreateAssetMenu(menuName = "AI/Create MoveData", fileName = "MoveData", order = 0)]
    public class AIMoveData: ScriptableObject
    {
        [FormerlySerializedAs("Speed")] public float speed;
        [FormerlySerializedAs("StoppingDistance")] public float stoppingDistance;
        [FormerlySerializedAs("RotationSpeed")] public float rotationSpeed;
        
        [ShowIf("moveState", CharacterAIMoveState.Patrol)]
        public Vector3[] routes;
        
        public CharacterAIMoveState moveState;
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
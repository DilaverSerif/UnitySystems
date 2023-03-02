using UnityEngine;
namespace AI_System.Scripts.Data
{
    [CreateAssetMenu(menuName = "AI System/Move System/Data/Create AI MoveData", fileName = "MoveData", order = 0)]
    public class AIMoveData: ScriptableObject
    {
        public float speed;
        public float rotationSpeed;
        public float stoppingDistance;
    }
}
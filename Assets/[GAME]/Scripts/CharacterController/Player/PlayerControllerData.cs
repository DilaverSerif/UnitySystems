using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [CreateAssetMenu(menuName = "Create PlayerControllerData", fileName = "PlayerControllerData", order = 0)]
    public class PlayerControllerData: ScriptableObject
    {
        public float moveSpeed = 10;
        public float rotationSpeed = 10;
        
        public float jumpForce = 10;
        public float gravity = -9.81f;
        
        public bool ForwardMovement = true;
        
        [BoxGroup("Locamation")]
        public bool LocamationEnabled = true;
        [BoxGroup("Locamation")]
        [ShowIf("LocamationEnabled")]
        public float LocamationSpeed = 10;

    }
}
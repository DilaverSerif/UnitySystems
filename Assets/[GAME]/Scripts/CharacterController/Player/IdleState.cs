using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Create IdleState", fileName = "IdleState", order = 0)]
    public class IdleState : CharacterControllerState
    {
        public override void Rotation(ref Vector3 position)
        {
            //do nothing
        }

        public override void Jump(Vector3 centerPoint)
        {
            //do nothing
        }

        public override Vector3 CalculateMovement(ref Vector3 input)
        {
            return Vector3.zero;
        }
    }
}
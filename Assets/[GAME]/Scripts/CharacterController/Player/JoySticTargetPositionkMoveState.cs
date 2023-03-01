using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Create JoySticTargetPositionkMoveState", fileName = "JoySticTargetPositionkMoveState", order = 0)]
    public class JoySticTargetPositionkMoveState : CharacterControllerState
    {
        public override void Move(Vector3 position)
        {
            TargetPosition = CalculateMovement(ref position);
            
            CharacterController.Move(TargetPosition);
        }

        public override void Rotation(ref Vector3 position)
        {
            CharacterController.transform.rotation = Quaternion.LookRotation(position);
        }

        public override void Jump(Vector3 centerPoint)
        {
            throw new System.NotImplementedException();
        }

        public override Vector3 CalculateMovement(ref Vector3 input)
        {
            return input * (playerControllerData.moveSpeed * Time.fixedDeltaTime);
        }
    }
}
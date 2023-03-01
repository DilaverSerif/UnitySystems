using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Create NormalMoveState", fileName = "NormalMoveState", order = 0)]
    public class JoystickMoveState : CharacterControllerState
    {
        public override void Rotation(ref Vector3 position)
        {
            //calculate angle by joystick input
            var angle = Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;
            if(angle == 0) return;
            
            //rotate character  
            CharacterController.transform.rotation = 
                Quaternion.RotateTowards(CharacterController.transform.rotation, 
                    Quaternion.Euler(0, angle, 0), 
                    playerControllerData.rotationSpeed);
        }

        public override void Jump(Vector3 centerPoint)
        {
            //calculate jump direction
            var jumpDirection = (centerPoint - CharacterController.transform.position).normalized;
            //add jump force
            CharacterController.Move(jumpDirection * playerControllerData.jumpForce * Time.fixedDeltaTime);
        }
        
        public override Vector3 CalculateMovement(ref Vector3 input)
        {
            return new Vector3(input.x, playerControllerData.gravity, input.y) * (playerControllerData.moveSpeed * Time.fixedDeltaTime);
        }
    }
}
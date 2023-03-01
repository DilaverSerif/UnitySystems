using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Character
{
    [Serializable]
    public abstract class CharacterControllerState : ScriptableObject
    {
        public string stateName;
        public PlayerControllerData playerControllerData;
        protected CharacterController CharacterController;

        protected Vector3 TargetPosition;

        public virtual void Move(Vector3 position)
        {
            if (position.magnitude > 0)
            {
                TargetPosition = CalculateMovement(ref position);
                Rotation(ref TargetPosition);
            }
            else
            {
                if (TargetPosition.magnitude > 0)
                    LocamationMovement();
            }

            CharacterController.Move(TargetPosition);
        }

        public abstract void Rotation(ref Vector3 position);
        public abstract void Jump(Vector3 centerPoint);

        public virtual void JumpBack(Vector3 centerPoint)
        {
        }

        public virtual bool ReachedDestination()
        {
            return Vector3.Distance(CharacterController.transform.position, TargetPosition) <= 0.1f;
        }

        public virtual void LocamationMovement()
        {
            var lerpPosition = Vector3.MoveTowards(
                TargetPosition,
                Vector3.zero,
                playerControllerData.LocamationSpeed * Time.fixedDeltaTime);

            lerpPosition.y = playerControllerData.gravity * Time.fixedDeltaTime;
            TargetPosition = lerpPosition;
        }

        public abstract Vector3 CalculateMovement(ref Vector3 input);

        public void Initialize(ref CharacterController characterController)
        {
            this.CharacterController = characterController;
        }

        public void OnGizmos()
        {
            //calculate destination positon by character controller velocity
            if (playerControllerData == null || CharacterController == null)
                return;
            var pos = CharacterController.velocity * Time.fixedDeltaTime * playerControllerData.moveSpeed;
            var position = CharacterController.transform.position;
            var destinationPosition = position + pos;


            Gizmos.color = Color.green;
            Gizmos.DrawSphere(destinationPosition, 0.25f);

            Gizmos.DrawLine(position, destinationPosition);
        }
    }
}
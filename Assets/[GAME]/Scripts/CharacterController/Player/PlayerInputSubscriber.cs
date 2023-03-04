using _GAME_.Scripts.Character.Abstracs;
using MaiGames.Scripts.Runtime.Base.InputSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    [System.Serializable]
    public class PlayerInputSubscriber: IUpdater
    {
        private PlayerJoystickController playerJoystickController;
        
        public PlayerInputSubscriber(ref PlayerJoystickController playerJoystickController)
        {
            this.playerJoystickController = playerJoystickController;
        }
        
        private void MovingInput()
        {
            playerJoystickController.ChangeState(Joystick.Instance.GetVector().magnitude > 0
                ? CharacterStates.Move
                : CharacterStates.Idle);
        }

        public void OnUpdate()
        {
            MovingInput();
        }
    }
}
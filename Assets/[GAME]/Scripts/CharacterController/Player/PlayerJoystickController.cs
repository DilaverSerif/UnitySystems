using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using MaiGames.Scripts.Runtime.Base.InputSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    [Serializable]
    public class PlayerJoystickController:IUpdater,IInitializable
    {
        [BoxGroup("References")]
        public CharacterController characterController;
        
        [BoxGroup("States")]
        public List<CharacterControllerState> characterControllerStates;
        [BoxGroup("States")]
        [ReadOnly]
        public CharacterControllerState currentState;

        public PlayerJoystickController(CharacterController characterController)
        {
            this.characterController = characterController;
        }

        public void ChangeState(CharacterControllerState state)
        {
            foreach (var characterControllerState in characterControllerStates.Where(controllerState => controllerState == state))
            {
                currentState = characterControllerState;
            }
        }
    
        public void OnUpdate()
        {
            if(!currentState) return;
                currentState.Move(Joystick.Instance.GetVector());
        }

        public void Initialize()
        {
            if(characterController == null)
                throw new Exception("PlayerJoystickController is not initialized properly");
            
            foreach (var state in characterControllerStates)
            {
                state.Initialize(ref characterController); 
            }
            
            currentState = characterControllerStates[0];
        }
    }
}
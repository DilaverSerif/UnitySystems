using System;
using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts.Character.Abstracs;
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
        
        [BoxGroup("States"),ShowInInspector,TableList]
        public List<CharacterControllerState> characterControllerStates;
        [BoxGroup("States")]
        [ReadOnly]
        public CharacterControllerState currentState;

        #region Player Property

        private Player Player
        {
            get
            {
                if (player == null)
                    player = characterController.GetComponent<Player>();
                return player;
            }
        }
        
        private Player player;
        
        #endregion
      
        public void ChangeState(CharacterStates state)
        {
            if(Player.CharacterState == state) 
                return;
            
            var newState = characterControllerStates.FirstOrDefault(x => x.characterState == state);
            
            if (newState == null)
                Debug.LogError("State not found");            
            
            if (currentState != null)
                currentState.OnExit();
            
            currentState = newState;
            if (currentState != null) 
                currentState.OnEnter();
            
            Player.CharacterState = state;
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
            
            ChangeState(CharacterStates.Idle);
        }
    }
}
using System.Collections;
using _GAME_.Scripts.Character.Abstracs;
using Sirenix.OdinInspector;
using UnityEngine;
namespace _GAME_.Scripts.Player
{
    public class Player : CharacterBase
    {
        [ShowInInspector]
        public PlayerJoystickController playerJoystickController;
        protected override void OnDeath()
        {
            
        }

        protected override void OnSpawn()
        {
            playerJoystickController.Initialize();
            StartCoroutine(OnUpdater());
        }

        private IEnumerator OnUpdater()
        {
            while (CharacterStates.Die != CharacterState)
            {
                playerJoystickController.OnUpdate();
                yield return new WaitForFixedUpdate();
            }
        }
        
        private void OnDrawGizmos()
        {
            playerJoystickController.currentState?.OnGizmos();
        }
    }


}
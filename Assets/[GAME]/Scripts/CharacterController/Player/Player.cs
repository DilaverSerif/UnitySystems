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
        private PlayerInputSubscriber playerInputSubscriber;
        protected override void OnDeath()
        {
            
        }

        protected override void OnSpawn()
        {
            playerInputSubscriber = new PlayerInputSubscriber(ref playerJoystickController);
            playerJoystickController.Initialize();
            StartCoroutine(OnUpdater());
        }

        private IEnumerator OnUpdater()
        {
            while (CharacterState != CharacterStates.Die)
            {
                playerInputSubscriber.OnUpdate();
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
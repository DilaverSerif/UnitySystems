using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace Character
{
    [System.Serializable]
    public class ParticleAnim<T> where T : Enum
    {
        public enum ParticleSpawnType
        {
            JustPlay,
            Instantiate,
        }

        public ParticleSystem ParticleSystem;
        public float Delay;
        public T AnimEnum;
        public ParticleSpawnType SpawnType;
        public UnityEvent OnSpawnParticle;

        public ParticleSystem SpawnParticle()
        {
            OnSpawnParticle?.Invoke();
            
            switch (SpawnType)
            {
                case ParticleSpawnType.JustPlay:
                    ParticleSystem.Play();
                    return ParticleSystem;
                case ParticleSpawnType.Instantiate:
                    var newParticleSystem = Object.Instantiate(ParticleSystem);
                    return newParticleSystem.GetComponent<ParticleSystem>();
                default:
                    Debug.LogError("Wrong SpawnType");
                    return null;
            }
        }
    }
}
using System;
using Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME_.Scripts.Character.Abstracs
{
    [Serializable]
    public class CharacterAnimationSystem<T>:MonoBehaviour,IAnimable<T> where T:Enum
    {
        [BoxGroup("Current Datas")]
        public Animator Anim;
        [BoxGroup("Current Datas")]
        public ParticleAnim<T>[] ParticleAnims;
        private bool _isAnimNull => Anim == null;
        
        public void PlayParticle(T animEnum)
        {
            if(ParticleAnims == null) return;
            
            foreach (var particleAnim in ParticleAnims)
            {
                if (!particleAnim.AnimEnum.Equals(animEnum))
                    continue;

                particleAnim.SpawnParticle();
                break;
            }
        }
        
        public void PlayAnimation(T animEnum,int layer = 0,float normalizedTime = 1f,float normalizedTransitionTime = 0.15f)
        {
            if(_isAnimNull) return;
            
            if(CheckAnim(ref animEnum))
                Anim.CrossFade(animEnum.ToString(), normalizedTransitionTime, layer, normalizedTime);
            else 
                Anim.CrossFade(animEnum.ToString(), normalizedTransitionTime, layer);
            
            PlayParticle(animEnum);
        }

        public bool CheckAnim(ref T animEnum)
        {
            return Anim.GetCurrentAnimatorStateInfo(0).IsName(animEnum.ToString());
        }
    }
}
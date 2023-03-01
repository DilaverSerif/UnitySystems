using System;

namespace Character
{
    public interface IAnimable<T> where T: Enum
    {
        void PlayAnimation(T animEnum,int layer = 0,float normalizedTime = 1f,float normalizedTransitionTime = 0.15f);
        bool CheckAnim(ref T animEnum);
        void PlayParticle(T animEnum);
    }
}
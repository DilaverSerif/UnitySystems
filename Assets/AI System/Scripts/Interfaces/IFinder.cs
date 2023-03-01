using UnityEngine;

namespace _GAME_.Scripts.Character.Interfaces
{
    public interface IFinder<out T>
    {
        T FindTarget();
        float GetTargetDistance();
        float GetTargetAngle();
        Vector3 GetTargetPosition();
    }
}
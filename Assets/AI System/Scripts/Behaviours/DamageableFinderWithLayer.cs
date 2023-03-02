using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Data;
using AI_System.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _GAME_.Scripts.Character
{
    public class DamageableFinderWithLayer: MonoBehaviour,IUpdater, IFinder<IDamageable>
    { 
       [BoxGroup("Data")]
       
       public FinderData FinderData;
       [BoxGroup("Data")]
       public FinderData AfterFindData;
       
       [ShowInInspector, ReadOnly, BoxGroup("Current Data")]
       public FinderData CurrentData => Target != null ? AfterFindData : FinderData;

       [BoxGroup("Current Data"), ShowInInspector, ReadOnly]
       private string _currentTargetName => Target == null ? "None" : Target.transform.name;
       private IDamageable Target;

       [BoxGroup("Events")]
       public UnityEvent OnFindTarget;
       [BoxGroup("Events")]
       public UnityEvent OnLostTarget;

       public IDamageable FindTarget()
        {
            var results = Physics.OverlapSphere(transform.position, CurrentData.Radius,CurrentData.TargetMask);
            
            if (results.Length == 0) return Target = null;
            
            foreach (var col in results)
            {
                var thisTransform = transform;
                
                var direction  = (col.transform.position - thisTransform.position).normalized;
                var angle      = Vector3.Angle(thisTransform.forward, direction);

                if (!(angle <= CurrentData.Angle))
                    continue;

                if(Target == null)
                    OnFindTarget?.Invoke();
                
                var damageable = col.GetComponent<IDamageable>();
                
                if(damageable.Health <= 0) continue;
            
                return Target = damageable;
            }
            
            if(Target != null)
                OnLostTarget?.Invoke();
            
            return Target = null;
        }

        public float GetTargetDistance()
        {
            if(Target == null) return -1f;
                
            return Vector3.Distance(transform.position, Target.transform.position);
        }

        public float GetTargetAngle()
        {
            if(Target == null) return -1f;
                
            return Vector3.Angle(transform.position, Target.transform.position);
        }

        public Vector3 GetTargetPosition()
        {
            if(Target == null) 
                return default;
            return Target.transform.position;
        }

        public Transform GetTarget()
        {
            return Target?.transform;
        }

        private void OnDrawGizmos()
        {
            if(CurrentData == null) return;

            var position2 = transform.position;
            Handles.DrawWireDisc(position2, Vector3.up, CurrentData.Radius, 10f);
            Handles.color = Color.blue;

            var forward = transform.forward;
            Handles.DrawWireArc(position2, Vector3.up, forward, CurrentData.Angle, CurrentData.Radius, 7.5f);
            Handles.DrawWireArc(position2, Vector3.up, forward, -CurrentData.Angle, CurrentData.Radius, 7.5f);

            if (Target != null)
            {
                var position = Target.transform.position;
                var position1 = transform.position;
                Handles.DrawLine(position1,position);
                Handles.Label((position + position1) * .5f, Vector3.Distance(position1, position).ToString("F2"));
            }
        
        }
        public void OnUpdate()
        {
            FindTarget();
        }
    }

}
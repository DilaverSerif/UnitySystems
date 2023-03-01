using _GAME_.Scripts.Character.Interfaces;
using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace AI_System.Scripts.VisualScripting
{
    [UnitCategory("AI/Move")]
    public class MoveUnit: Unit
    {
        private IMovable movable;
    
        public ControlInput TriggerInput;
        //public ValueInput MoveDirectionInput;
    
        protected override void Definition()
        {
            //MoveDirectionInput = ValueInput<Vector3>("MoveDirection");
            TriggerInput = ControlInput(nameof(TriggerInput), MoveTrigger);    
        }
        private ControlOutput MoveTrigger(Flow arg)
        {
            movable.Move();
            return null;
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            movable = instance.component.GetComponent<IMovable>();
        }
    }
}
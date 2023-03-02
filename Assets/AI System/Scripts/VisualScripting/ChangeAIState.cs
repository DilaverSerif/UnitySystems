using System.Collections.Generic;
using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace AI_System.Scripts.VisualScripting
{
    [UnitCategory("AI/Override AI State")]
    public class ChangeAIState : Unit
    {
        private IMovable movable;
        private CharacterBase characterBase;
        
        public ValueInput AIState;
        public ControlInput TriggerInput;
        public ControlOutput ControlOutput;
        protected override void Definition()
        {
            AIState = ValueInput<CharacterStates>("AIState", CharacterStates.Idle);
            TriggerInput = ControlInput(nameof(TriggerInput), ChangeStateTrigger);
            ControlOutput = ControlOutput(nameof(ControlOutput));
        }

        private ControlOutput ChangeStateTrigger(Flow arg)
        {
            // movable.ChangeState(arg.GetValue<CharacterStates>(AIState));
            characterBase.ChangeState(arg.GetValue<CharacterStates>(AIState));
            return ControlOutput;
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            characterBase = instance.component.GetComponent<CharacterBase>();
            movable = instance.component.GetComponent<IMovable>();
        }
        

        
    }
}
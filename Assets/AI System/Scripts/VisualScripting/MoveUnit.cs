using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace AI_System.Scripts.VisualScripting
{
    [UnitCategory("AI/Move")]
    public class MoveUnit: Unit
    {
        [DoNotSerialize]
        private IMovable movable;
        [DoNotSerialize]
        public ControlInput HasTargetInput;
        [DoNotSerialize]
        public ControlInput NoTargetControlInput;
        [DoNotSerialize]
        public ControlOutput MoveOutput;
        [DoNotSerialize]
        public ValueInput StateInput;
        
        
        protected override void Definition()
        {
            //MoveDirectionInput = ValueInput<Vector3>("MoveDirection");
            HasTargetInput = ControlInput(nameof(HasTargetInput), HasTarget);
            NoTargetControlInput = ControlInput(nameof(NoTargetControlInput), NoTargetMoveTrigger);
            MoveOutput = ControlOutput(nameof(MoveOutput));
            StateInput = ValueInput<CharacterStates>(nameof(StateInput));
        }
        private ControlOutput HasTarget(Flow arg)
        {
            movable.ChangeState(CharacterStates.Attack);
            movable.Move();
            return MoveOutput;
        }
        private ControlOutput NoTargetMoveTrigger(Flow arg)
        {
            movable.Move();
            return null;
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            movable = instance.component.GetComponent<IMovable>();
            if (movable == null)
                Debug.LogError("No IMovable found on " +
                               instance.component.name);
        }
    }
}
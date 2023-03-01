using _GAME_.Scripts.Character.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace AI_System.Scripts.VisualScripting
{
    public abstract class SearchTarget<T> : Unit
    {
        [DoNotSerialize]
        public ControlInput TriggerInput;
        [DoNotSerialize]
        public ControlOutput NotFoundOutput;
        [DoNotSerialize]
        public ControlOutput FoundOutput;
        [DoNotSerialize]
        public ValueOutput TargetOutput;
        [DoNotSerialize]
        public ValueOutput TargetOutputPosition;
        [DoNotSerialize]
        private IFinder<T> finder;

        protected override void Definition()
        {
            TriggerInput = ControlInput(nameof(TriggerInput),Finder);

            TargetOutput = ValueOutput<T>(nameof(TargetOutput));
            TargetOutputPosition = ValueOutput<Vector3>(nameof(TargetOutputPosition));

            FoundOutput = ControlOutput(nameof(FoundOutput));
            NotFoundOutput = ControlOutput(nameof(NotFoundOutput));
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            finder = instance.component.GetComponent<IFinder<T>>();
        }

        private ControlOutput Finder(Flow flow)
        {
            flow.SetValue(TargetOutput,finder.FindTarget());
            flow.SetValue(TargetOutputPosition, finder.GetTargetPosition());
            
            return finder.FindTarget() == null ? NotFoundOutput : FoundOutput;
        }
    }
}

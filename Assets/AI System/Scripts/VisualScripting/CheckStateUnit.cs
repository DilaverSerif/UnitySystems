using _GAME_.Scripts.Character.Abstracs;
using Unity.VisualScripting;
namespace AI_System.Scripts.VisualScripting
{
    public class CheckStateUnit : Unit
    {
        public ValueInput InputState;
        public ControlInput TriggerInput;
        public ControlOutput OutputTrue;
        public ControlOutput OutputFalse;
        
        private CharacterBase characterBase;
        
        protected override void Definition()
        {
            InputState = ValueInput<CharacterStates>(nameof(InputState),CharacterStates.Idle);
            OutputTrue = ControlOutput(nameof(OutputTrue));
            OutputFalse = ControlOutput(nameof(OutputFalse));
            TriggerInput = ControlInput(nameof(TriggerInput), CheckState);
        }
        
        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            characterBase = instance.component.GetComponent<CharacterBase>();
        }
        
        private ControlOutput CheckState(Flow arg)
        {
            return characterBase.CharacterState ==
                   arg.GetValue<CharacterStates>(InputState) ?
                OutputTrue : OutputFalse;
        }
    }
}

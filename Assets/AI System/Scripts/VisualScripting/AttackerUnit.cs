using _GAME_.Scripts.Character.Abstracs;
using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;

namespace AI_System.Scripts.VisualScripting
{
    public abstract class AttackerUnit<T>: Unit where T: System.Enum
    {
        public ControlInput TriggerInput;
        public ValueInput DamageableInput;
        
        private IAttacker<T> attacker;
        private CharacterBase characterBase;
        
        protected override void Definition()
        {
            DamageableInput = ValueInput<IDamageable>(nameof(DamageableInput));
            TriggerInput = ControlInput(nameof(TriggerInput),Tick);
        }
        private ControlOutput Tick(Flow arg)
        {
            var damageable = arg.GetValue<IDamageable>(DamageableInput);
            if(damageable == null)
                return null;

            characterBase.ChangeState(CharacterStates.Attack);
            attacker.CheckForAttack(damageable);
            return null;
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            characterBase = instance.component.GetComponent<CharacterBase>();
            attacker = instance.component.GetComponent<IAttacker<T>>();
        }
        
    }

    public interface IStateMachine
    {
        void ChangeState(CharacterStates state);
    }
}
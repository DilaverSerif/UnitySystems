using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;

namespace AI_System.Scripts.VisualScripting
{
    public abstract class AttackerUnit<T>: Unit where T: System.Enum
    {
        public ControlInput TriggerInput;
        public ValueInput DamageableInput;
        
        private IAttacker<T> attacker;
        
        protected override void Definition()
        {
            TriggerInput = ControlInput(nameof(TriggerInput),Tick);
        }
        private ControlOutput Tick(Flow arg)
        {
            var damageable = arg.GetValue<IDamageable>(DamageableInput);
            attacker.CheckForAttack(damageable);
            return null;
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            attacker = instance.component.GetComponent<IAttacker<T>>();
        }
    }

}
using _GAME_.Scripts.Character.Interfaces;
using Unity.VisualScripting;

namespace _GAME_.Scripts.Character.VisualScripting
{
    public class AttackerUnit<T>: Unit where T: System.Enum
    {
        public ControlInput TriggerInput;
        public ValueInput AttackerInput;
        
        private IAttacker<T> attacker;
        
        protected override void Definition()
        {
            
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            attacker = instance.component.GetComponent<IAttacker<T>>();
        }
    }
}
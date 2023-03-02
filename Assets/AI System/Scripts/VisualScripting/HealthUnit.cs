using AI_System.Scripts.Interfaces;
using Unity.VisualScripting;

namespace AI_System.Scripts.VisualScripting
{
	public class HealthUnit: Unit
	{
		[DoNotSerialize]
		private IDamageable damageable;
		[DoNotSerialize]
		public ValueOutput Health;
		
		[DoNotSerialize]
		public ControlInput TriggerInput;
		
		public ControlOutput LowHpOutput;
		public ControlOutput HighHpOutput;
		
		public ValueInput LowHpValue;
		
		protected override void Definition()
		{
			ControlInput(nameof(TriggerInput),GetHealth);
			Health = ValueOutput<int>(nameof(GetHealth));
			LowHpValue = ValueInput<int>(nameof(LowHpValue),0);
			
			LowHpOutput = ControlOutput(nameof(LowHpOutput));
			HighHpOutput = ControlOutput(nameof(HighHpOutput)); 
		}
		private ControlOutput GetHealth(Flow arg)
		{
			arg.SetValue(Health, damageable.Health);
				
			return damageable.Health < arg.GetValue<int>(LowHpValue) ? LowHpOutput : HighHpOutput;
		}


		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			damageable = instance.component.GetComponent<IDamageable>();
		}
	}
}
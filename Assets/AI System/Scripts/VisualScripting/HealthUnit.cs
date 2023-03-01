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
		[DoNotSerialize]
		public ControlOutput ControlOutput;
		
		protected override void Definition()
		{
			ControlOutput = ControlOutput(nameof(ControlOutput));
			ControlInput(nameof(TriggerInput),GetHealth);
			Health = ValueOutput<int>(nameof(GetHealth));
		}
		private ControlOutput GetHealth(Flow arg)
		{
			arg.SetValue(Health,0);
			return ControlOutput;
		}


		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			damageable = instance.component.GetComponent<IDamageable>();
		}
	}
}
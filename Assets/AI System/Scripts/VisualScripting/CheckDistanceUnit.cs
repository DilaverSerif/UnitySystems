using Unity.VisualScripting;
using UnityEngine;

namespace AI_System.Scripts.VisualScripting
{
	public class CheckDistanceUnit : Unit
	{
		public ValueInput TransformInput;
		public ValueInput DistanceInput;
		
		protected ValueOutput DistanceValueOutput;
		
		public ControlInput TriggerInput;
		
		public ControlOutput OutputTrue;
		public ControlOutput OutputFalse;

		private Transform thisTransform;

		protected override void Definition()
		{
			TransformInput = ValueInput<Transform>(nameof(TransformInput));
			DistanceInput = ValueInput<float>(nameof(DistanceInput), 0);
			
			OutputTrue = ControlOutput(nameof(OutputTrue));
			OutputFalse = ControlOutput(nameof(OutputFalse));
			
			TriggerInput = ControlInput(nameof(TriggerInput), CheckDistance);
		}
		
		public override void Instantiate(GraphReference instance)
		{
			base.Instantiate(instance);
			thisTransform = instance.component.transform;
		}
		
		private ControlOutput CheckDistance(Flow arg)
		{
			DistanceValueOutput = ValueOutput<float>(nameof(DistanceValueOutput), (flow)=> Vector3.Distance(arg.GetValue<Transform>(TransformInput).position, thisTransform.position));
			return Vector3.Distance(arg.GetValue<Transform>(TransformInput).position, thisTransform.position) < arg.GetValue<float>(DistanceInput) ? OutputTrue : OutputFalse;
		}
	}
}

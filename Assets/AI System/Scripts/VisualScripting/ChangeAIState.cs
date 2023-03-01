using _GAME_.Scripts.Character.Abstracs;
using Unity.VisualScripting;
namespace AI_System.Scripts.VisualScripting
{
	[UnitCategory("AI/Override AI State")]
	public class ChangeAIState : Unit
	{
		public CharacterStates State;
		[DoNotSerialize]
		public ValueInput StateInput;
		[DoNotSerialize]
		public ValueOutput StateOutput;
		protected override void Definition()
		{
			StateInput = ValueInput<CharacterStates>("State",State);
			StateOutput = ValueOutput<CharacterStates>("State",(flow)=>State);
		}
	}
}
using _GAME_.Scripts.Character.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class SearchTarget<T> : Unit
{
    public ControlInput TriggerInput;
    
    public ControlOutput NotFoundOutput;
    public ControlOutput FoundOutput;
    
    public ValueOutput TargetOutput;
    public ValueOutput TargetOutputPosition;
    
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
        
        return null;
    }
}

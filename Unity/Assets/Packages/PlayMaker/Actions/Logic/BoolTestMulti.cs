// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.BoolTestMulti
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Logic)]
[Tooltip("Tests if all the given Bool Variables are are equal to thier Bool States.")]
public class BoolTestMulti : FsmStateAction
{
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Tooltip("This must be the same number used for Bool States.")]
	public FsmBool[] boolVariables;

	[RequiredField]
	[Tooltip("This must be the same number used for Bool Variables.")]
	public FsmBool[] boolStates;

	public FsmEvent trueEvent;

	public FsmEvent falseEvent;

	[UIHint(UIHint.Variable)]
	public FsmBool storeResult;

	public bool everyFrame;

	public override void Reset()
	{
		boolVariables = null;
		boolStates = null;
		trueEvent = null;
		falseEvent = null;
		storeResult = null;
		everyFrame = false;
	}

	public override void OnEnter()
	{
		DoAllTrue();
		if (!everyFrame)
		{
			Finish();
		}
	}

	public override void OnUpdate()
	{
		DoAllTrue();
	}

	private void DoAllTrue()
	{
		if (boolVariables.Length == 0 || boolStates.Length == 0 || boolVariables.Length != boolStates.Length)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < boolVariables.Length; i++)
		{
			if (boolVariables[i].Value != boolStates[i].Value)
			{
				flag = false;
				break;
			}
		}
		storeResult.Value = flag;
		if (flag)
		{
			base.Fsm.Event(trueEvent);
		}
		else
		{
			base.Fsm.Event(falseEvent);
		}
	}
}

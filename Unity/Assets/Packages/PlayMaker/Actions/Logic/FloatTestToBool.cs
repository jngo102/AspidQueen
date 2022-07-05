// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.FloatTestToBool
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.Logic)]
[HutongGames.PlayMaker.Tooltip("Set bools based on the comparison of 2 Floats.")]
public class FloatTestToBool : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The first float variable.")]
	public FsmFloat float1;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The second float variable.")]
	public FsmFloat float2;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Tolerance for the Equal test (almost equal).")]
	public FsmFloat tolerance;

	[HutongGames.PlayMaker.Tooltip("Bool set if Float 1 equals Float 2")]
	[UIHint(UIHint.Variable)]
	public FsmBool equalBool;

	[HutongGames.PlayMaker.Tooltip("Bool set if Float 1 is less than Float 2")]
	[UIHint(UIHint.Variable)]
	public FsmBool lessThanBool;

	[HutongGames.PlayMaker.Tooltip("Bool set if Float 1 is greater than Float 2")]
	[UIHint(UIHint.Variable)]
	public FsmBool greaterThanBool;

	[HutongGames.PlayMaker.Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
	public bool everyFrame;

	public override void Reset()
	{
		float1 = 0f;
		float2 = 0f;
		tolerance = 0f;
		everyFrame = false;
	}

	public override void OnEnter()
	{
		DoCompare();
		if (!everyFrame)
		{
			Finish();
		}
	}

	public override void OnUpdate()
	{
		DoCompare();
	}

	private void DoCompare()
	{
		if (Mathf.Abs(float1.Value - float2.Value) <= tolerance.Value)
		{
			equalBool.Value = true;
		}
		else
		{
			equalBool.Value = false;
		}
		if (float1.Value < float2.Value)
		{
			lessThanBool.Value = true;
		}
		else
		{
			lessThanBool.Value = false;
		}
		if (float1.Value > float2.Value)
		{
			greaterThanBool.Value = true;
		}
		else
		{
			greaterThanBool.Value = false;
		}
	}
}

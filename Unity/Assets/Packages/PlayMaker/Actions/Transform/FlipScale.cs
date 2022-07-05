// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.FlipScale
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.Transform)]
[HutongGames.PlayMaker.Tooltip("Sets the Scale of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
public class FlipScale : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The GameObject to scale.")]
	public FsmOwnerDefault gameObject;

	public bool flipHorizontally;

	public bool flipVertically;

	[HutongGames.PlayMaker.Tooltip("Repeat every frame.")]
	public bool everyFrame;

	[HutongGames.PlayMaker.Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
	public bool lateUpdate;

	public override void Reset()
	{
		flipHorizontally = false;
		flipVertically = false;
		everyFrame = false;
	}

	public override void OnEnter()
	{
		DoFlipScale();
		if (!everyFrame)
		{
			Finish();
		}
	}

	public override void OnUpdate()
	{
		if (!lateUpdate)
		{
			DoFlipScale();
		}
	}

	public override void OnLateUpdate()
	{
		if (lateUpdate)
		{
			DoFlipScale();
		}
		if (!everyFrame)
		{
			Finish();
		}
	}

	private void DoFlipScale()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
		if (!(ownerDefaultTarget == null))
		{
			Vector3 localScale = ownerDefaultTarget.transform.localScale;
			if (flipHorizontally)
			{
				localScale.x = 0f - localScale.x;
			}
			if (flipVertically)
			{
				localScale.y = 0f - localScale.y;
			}
			ownerDefaultTarget.transform.localScale = localScale;
		}
	}
}

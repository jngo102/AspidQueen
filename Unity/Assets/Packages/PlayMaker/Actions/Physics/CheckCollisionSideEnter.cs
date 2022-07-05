// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.CheckCollisionSideEnter
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.Physics)]
[HutongGames.PlayMaker.Tooltip("Detect additional collisions between the Owner of this FSM and other object with additional raycasting.")]
public class CheckCollisionSideEnter : FsmStateAction
{
	public enum CollisionSide
	{
		top,
		left,
		right,
		bottom,
		other
	}

	[UIHint(UIHint.Variable)]
	public FsmBool topHit;

	[UIHint(UIHint.Variable)]
	public FsmBool rightHit;

	[UIHint(UIHint.Variable)]
	public FsmBool bottomHit;

	[UIHint(UIHint.Variable)]
	public FsmBool leftHit;

	public FsmEvent topHitEvent;

	public FsmEvent rightHitEvent;

	public FsmEvent bottomHitEvent;

	public FsmEvent leftHitEvent;

	public bool otherLayer;

	public int otherLayerNumber;

	public FsmBool ignoreTriggers;

	private PlayMakerUnity2DProxy _proxy;

	private Collider2D col2d;

	private const float RAYCAST_LENGTH = 0.08f;

	private List<Vector2> topRays;

	private List<Vector2> rightRays;

	private List<Vector2> bottomRays;

	private List<Vector2> leftRays;

	public override void Reset()
	{
	}

	public override void OnEnter()
	{
		col2d = base.Fsm.GameObject.GetComponent<Collider2D>();
		_proxy = base.Owner.GetComponent<PlayMakerUnity2DProxy>();
		if (_proxy == null)
		{
			_proxy = base.Owner.AddComponent<PlayMakerUnity2DProxy>();
		}
		_proxy.AddOnCollisionEnter2dDelegate(DoCollisionEnter2D);
	}

	public override void OnExit()
	{
		_proxy.RemoveOnCollisionEnter2dDelegate(DoCollisionEnter2D);
	}

	public override void OnUpdate()
	{
	}

	public new void DoCollisionEnter2D(Collision2D collision)
	{
		if (!otherLayer)
		{
			if (LayerMask.LayerToName(collision.gameObject.layer) == "Terrain")
			{
				CheckTouching(8);
			}
		}
		else
		{
			CheckTouching(otherLayerNumber);
		}
	}

	private void CheckTouching(LayerMask layer)
	{
		topRays = new List<Vector2>();
		topRays.Add(new Vector2(col2d.bounds.min.x, col2d.bounds.max.y));
		topRays.Add(new Vector2(col2d.bounds.center.x, col2d.bounds.max.y));
		topRays.Add(col2d.bounds.max);
		rightRays = new List<Vector2>();
		rightRays.Add(col2d.bounds.max);
		rightRays.Add(new Vector2(col2d.bounds.max.x, col2d.bounds.center.y));
		rightRays.Add(new Vector2(col2d.bounds.max.x, col2d.bounds.min.y));
		bottomRays = new List<Vector2>();
		bottomRays.Add(new Vector2(col2d.bounds.max.x, col2d.bounds.min.y));
		bottomRays.Add(new Vector2(col2d.bounds.center.x, col2d.bounds.min.y));
		bottomRays.Add(col2d.bounds.min);
		leftRays = new List<Vector2>();
		leftRays.Add(col2d.bounds.min);
		leftRays.Add(new Vector2(col2d.bounds.min.x, col2d.bounds.center.y));
		leftRays.Add(new Vector2(col2d.bounds.min.x, col2d.bounds.max.y));
		topHit.Value = false;
		rightHit.Value = false;
		bottomHit.Value = false;
		leftHit.Value = false;
		foreach (Vector2 topRay in topRays)
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector3)topRay, Vector2.up, 0.08f, 1 << (int)layer);
			if (raycastHit2D.collider != null && (!ignoreTriggers.Value || !raycastHit2D.collider.isTrigger))
			{
				topHit.Value = true;
				base.Fsm.Event(topHitEvent);
				break;
			}
		}
		foreach (Vector2 rightRay in rightRays)
		{
			RaycastHit2D raycastHit2D2 = Physics2D.Raycast((Vector3)rightRay, Vector2.right, 0.08f, 1 << (int)layer);
			if (raycastHit2D2.collider != null && (!ignoreTriggers.Value || !raycastHit2D2.collider.isTrigger))
			{
				rightHit.Value = true;
				base.Fsm.Event(rightHitEvent);
				break;
			}
		}
		foreach (Vector2 bottomRay in bottomRays)
		{
			RaycastHit2D raycastHit2D3 = Physics2D.Raycast((Vector3)bottomRay, -Vector2.up, 0.08f, 1 << (int)layer);
			if (raycastHit2D3.collider != null && (!ignoreTriggers.Value || !raycastHit2D3.collider.isTrigger))
			{
				bottomHit.Value = true;
				base.Fsm.Event(bottomHitEvent);
				break;
			}
		}
		foreach (Vector2 leftRay in leftRays)
		{
			RaycastHit2D raycastHit2D4 = Physics2D.Raycast((Vector3)leftRay, -Vector2.right, 0.08f, 1 << (int)layer);
			if (raycastHit2D4.collider != null && (!ignoreTriggers.Value || !raycastHit2D4.collider.isTrigger))
			{
				leftHit.Value = true;
				base.Fsm.Event(leftHitEvent);
				break;
			}
		}
	}
}
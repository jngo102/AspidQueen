// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.SpawnObjectFromGlobalPool
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.GameObject)]
[HutongGames.PlayMaker.Tooltip("Spawns a prefab Game Object from the Global Object Pool on the Game Manager.")]
public class SpawnObjectFromGlobalPool : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("GameObject to create. Usually a Prefab.")]
	public FsmGameObject gameObject;

	[HutongGames.PlayMaker.Tooltip("Optional Spawn Point.")]
	public FsmGameObject spawnPoint;

	[HutongGames.PlayMaker.Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
	public FsmVector3 position;

	[HutongGames.PlayMaker.Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
	public FsmVector3 rotation;

	[UIHint(UIHint.Variable)]
	[HutongGames.PlayMaker.Tooltip("Optionally store the created object.")]
	public FsmGameObject storeObject;

	public override void Reset()
	{
		gameObject = null;
		spawnPoint = null;
		position = new FsmVector3
		{
			UseVariable = true
		};
		rotation = new FsmVector3
		{
			UseVariable = true
		};
		storeObject = null;
	}

	public override void OnEnter()
	{
		if (gameObject.Value != null)
		{
			Vector3 vector = Vector3.zero;
			Vector3 euler = Vector3.up;
			if (spawnPoint.Value != null)
			{
				vector = spawnPoint.Value.transform.position;
				if (!position.IsNone)
				{
					vector += position.Value;
				}
				euler = ((!rotation.IsNone) ? rotation.Value : spawnPoint.Value.transform.eulerAngles);
			}
			else
			{
				if (!position.IsNone)
				{
					vector = position.Value;
				}
				if (!rotation.IsNone)
				{
					euler = rotation.Value;
				}
			}
			if (gameObject != null)
			{
				GameObject value = gameObject.Value.Spawn(vector, Quaternion.Euler(euler));
				storeObject.Value = value;
			}
		}
		Finish();
	}
}

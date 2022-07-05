// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.AudioPlayerOneShot
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.Audio)]
[HutongGames.PlayMaker.Tooltip("Instantiate an Audio Player object and play a oneshot sound via its Audio Source.")]
public class AudioPlayerOneShot : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(AudioSource))]
	[HutongGames.PlayMaker.Tooltip("The object to spawn. Select Audio Player prefab.")]
	public FsmGameObject audioPlayer;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("Object to use as the spawn point of Audio Player")]
	public FsmGameObject spawnPoint;

	[CompoundArray("Audio Clips", "Audio Clip", "Weight")]
	public AudioClip[] audioClips;

	[HasFloatSlider(0f, 1f)]
	public FsmFloat[] weights;

	public FsmFloat pitchMin = 1f;

	public FsmFloat pitchMax = 2f;

	public FsmFloat volume;

	public FsmFloat delay;

	public FsmGameObject storePlayer;

	private AudioSource audio;

	private float timer;

	public override void Reset()
	{
		spawnPoint = null;
		audioClips = new AudioClip[3];
		weights = new FsmFloat[3] { 1f, 1f, 1f };
		pitchMin = 1f;
		pitchMax = 1f;
		volume = 1f;
		timer = 0f;
	}

	public override void OnEnter()
	{
		timer = 0f;
		if (delay.Value == 0f)
		{
			DoPlayRandomClip();
			Finish();
		}
	}

	public override void OnUpdate()
	{
		if (delay.Value > 0f)
		{
			if (timer < delay.Value)
			{
				timer += Time.deltaTime;
				return;
			}
			DoPlayRandomClip();
			Finish();
		}
	}

	private void DoPlayRandomClip()
	{
		if (audioClips.Length == 0)
		{
			return;
		}
		_ = audioPlayer.Value;
		Vector3 position = spawnPoint.Value.transform.position;
		Vector3 up = Vector3.up;
		GameObject gameObject = audioPlayer.Value.Spawn(position, Quaternion.Euler(up));
		audio = gameObject.GetComponent<AudioSource>();
		storePlayer.Value = gameObject;
		int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(weights);
		if (randomWeightedIndex != -1)
		{
			AudioClip audioClip = audioClips[randomWeightedIndex];
			if (audioClip != null)
			{
				float pitch = Random.Range(pitchMin.Value, pitchMax.Value);
				audio.pitch = pitch;
				audio.PlayOneShot(audioClip);
			}
		}
		audio.volume = volume.Value;
	}
}

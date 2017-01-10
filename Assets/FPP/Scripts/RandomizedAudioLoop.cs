using UnityEngine;
using System.Collections;

[AddComponentMenu("Dungeon/Generic/RandomizedAudioLoop")]
public class RandomizedAudioLoop : MonoBehaviour
{
	public bool playOnAwake = false;
	public bool loop = true;
	public float duration = float.PositiveInfinity;
	[Range(-0.5f, 0.0f)]
	public float maxPitchUp = 0;
	[Range(0.0f, 0.5f)]
	public float maxPitchDown = 0;
	[Range(1.0f,2.0f)]
	public float maxVolumeUp = 1;
	[Range(0.0f,1.0f)]
	public float maxVolumeDOwn = 1;

	public float minRepeadDelay = 0.0f;
	public float maxRepeadDelay = 0.0f;
	public bool delayOnFirst = false;
	public SoundType soundType = SoundType.SoundEffect;
	public AudioClip[] Clips;


	AudioSource source;
	bool playing = false;
	float defaultPitch;
	float startTime;
	void Awake()
	{
		var go = new GameObject();
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		source = go.AddComponent<AudioSource>();
		source.loop = false;
		source.rolloffMode = AudioRolloffMode.Linear;
		defaultPitch = source.pitch;
	}
	void Start()
	{
		if (playOnAwake)
			Play();
	}
	public void Play()
	{
		playing = true;
		if (delayOnFirst)
			Invoke("play", Random.Range(minRepeadDelay, maxRepeadDelay));
		else
			play();
	}
	void play()
	{
		StartCoroutine(LoopClips());
	}

	public void Stop()
	{
		playing = false;
		source.Stop();
	}

	IEnumerator LoopClips()
	{
		startTime = Time.time;
		while (playing && Clips.Length > 0 && Time.time - startTime < duration)
		{
			float volume = 1;
			switch (soundType)
			{
				case SoundType.SoundEffect:
					volume = AudioManager.SoundEffectsVolume;
					break;
				case SoundType.Ambiance:
					volume = AudioManager.AmbianceVolume;
					break;
				case SoundType.Voice:
					volume = AudioManager.VoiceVolume;
					break;
				case SoundType.Music:
					volume = AudioManager.MusicVolume;
					break;
				default:
					volume = AudioManager.SoundEffectsVolume;
					break;
			}

			var clip = Clips[Random.Range(0, Clips.Length)];
			source.clip = clip;
			source.pitch = defaultPitch + Random.Range(maxPitchDown, maxPitchUp);
			source.volume = volume * Random.Range(maxVolumeDOwn, maxVolumeUp);
			source.Play();
			if (!loop)
				break;
			yield return new WaitForSeconds(clip.length + Random.Range(minRepeadDelay, maxRepeadDelay));
		}
	}
}

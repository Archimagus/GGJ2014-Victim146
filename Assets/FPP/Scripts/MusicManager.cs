using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Audio/Music Manager")]
public class MusicManager : MonoBehaviour 
{
	[Serializable]
	public class MusicTrack
	{
		public string Name;
		public AudioClip[] Clips;
		public float FadeInTime = 1.0f;
		private AudioSource[] sources = new AudioSource[2];
		private int source = 0;
		private double nextClipTime = 0;
		private static double startTime = 0;
		private static int index;
		private static string[] TrackNames = { "Bass", "Drums", "Guitar", "Keys", "Lead", "Pad", "Piano", "Strings", "Synth"};
		private float Volume=-1;
		private bool volumeFade = true;
		private float _fadeStartTime;
		internal void Start(GameObject gameObject)
		{
			int i = 0; 
			while (i < 2)
			{ 
				GameObject child = new GameObject(Name + i.ToString());
				child.transform.parent = gameObject.transform;
				child.transform.localPosition = Vector3.zero;
				sources[i] = child.AddComponent(typeof(AudioSource)) as AudioSource; 
				sources[i].playOnAwake = false;
				i++; 
			}
			if (startTime == 0)
			{
				startTime = AudioSettings.dspTime + 1.0f;
			}
			nextClipTime = startTime;
			_fadeStartTime = Time.time;
		}
		public MusicTrack()
		{
			if (string.IsNullOrEmpty(Name))
			{
				if (index < TrackNames.Length)
				{
					Name = TrackNames[index];
				}
				else
				{
					Name = "Custom Track: " + index;
				}
			}
			index++;
		}
		public void TryQueueRandom(float volume)
		{
			var v = DoIntroFade(volume);
			sources[0].volume = v;
			sources[1].volume = v;

			var time = AudioSettings.dspTime;
			if (time + 2.0f > nextClipTime)
			{
				int c = UnityEngine.Random.Range(0, Clips.Length);
				AudioClip clip = Clips[c];
				sources[source].clip = clip;
				sources[source].PlayScheduled(nextClipTime);
				source = Math.Abs(source-1);
				sources[source].SetScheduledEndTime(nextClipTime);
				nextClipTime += Adjust(clip.length);
			}
		}

		private float DoIntroFade(float volume)
		{
			float v = volume;
			if (volumeFade)
			{
				var fade = Mathf.Lerp(0, 1, Time.time - _fadeStartTime / FadeInTime);
				v = volume * fade;
				if (Mathf.Abs(Volume - volume) < 0.1f)
				{
					volumeFade = false;
				}
			}
			return v;
		}
		double Adjust(double input)
		{
			return input;
			double whole = Math.Truncate(input);
			double adjustedTime = (whole / 10) * 10.1;
			return adjustedTime;
		}
	}

	public float Volume = 0;
	public MusicTrack[] Tracks;

	// Use this for initialization
	void Start () 
	{
		if (!Tracks.IsNullOrEmpty())
		{
			foreach (var t in Tracks)
			{
				t.Start(gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		QueueCLips();
	}

	void QueueCLips()
	{
		Volume = AudioManager.MusicVolume;

		if (!Tracks.IsNullOrEmpty())
		{
			foreach (var t in Tracks)
			{
				t.TryQueueRandom(Volume);
			}
		}
	}
}

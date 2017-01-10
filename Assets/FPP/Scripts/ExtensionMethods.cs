using UnityEngine;
using System.Collections;
using System.Timers;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
	public static Vector3 ClosestPointOnLine(this Vector3 vPoint, Vector3 vA, Vector3 vB)
	{
		var vVector1 = vPoint - vA;
			var vVector2 = (vB - vA).normalized;
		var d = Vector3.Distance(vA, vB);
		var t = Vector3.Dot(vVector2, vVector1);
		if (t <= 0) 
			return vA;
		if (t >= d)
			return vB;
		var vVector3 = vVector2 * t;
		var vClosestPoint = vA + vVector3;
		return vClosestPoint;
	}

	public static bool IsNullOrEmpty(this ICollection e)
	{
		return e == null || e.Count == 0;
	}
	private static Queue<AudioSource> AudioSources = new Queue<AudioSource>();
	public static void PlaySoundEffect(this MonoBehaviour This, AudioClip clip, SoundType type = SoundType.SoundEffect,  Vector3 ?location = null)
	{
		if(!location.HasValue)
		{
			location = This.transform.position;
		}
		AudioSource source;
		if (AudioSources.Count > 0)
		{
			source = AudioSources.Dequeue();
		}
		else 
		{
			var go = new GameObject("soundEffectDummy");
			source = go.AddComponent<AudioSource>();
			source.rolloffMode = AudioRolloffMode.Linear;
			GameObject.DontDestroyOnLoad(go);
		}
		source.gameObject.SetActive(true);
		source.transform.position = location.Value;
		switch (type)
		{
			case SoundType.SoundEffect:
				source.volume = AudioManager.SoundEffectsVolume;
				break;
			case SoundType.Ambiance:
				source.volume = AudioManager.AmbianceVolume;
				break;
			case SoundType.Voice:
				source.volume = AudioManager.VoiceVolume;
				break;
			case SoundType.Music:
				source.volume = AudioManager.MusicVolume;
				break;
			default:
				source.volume = AudioManager.SoundEffectsVolume;
				break;
		}
		source.clip = clip;
		source.Play();
		This.StartCoroutine(requeueSource(source));
	}
	static IEnumerator requeueSource(AudioSource source)
	{
		yield return new WaitForSeconds(source.clip.length);
		source.gameObject.SetActive(false);
		AudioSources.Enqueue(source);
	}
	
	public static void ForEach<T>(this IEnumerable arr, Action<T> act)
	{
		foreach (T t in arr)
		{
			act(t);
		}
	}

	public static T Instantiate<T>(this T o) where T : MonoBehaviour
	{
		return Instantiate(o, Vector3.up*10000, Quaternion.identity);
	}

	public static T Instantiate<T>(this T o, Vector3 position, Quaternion rotation) where T : MonoBehaviour
	{
		if (o != null)
		{
			return GameObject.Instantiate(o, position, rotation) as T;
		}
		return null;
	}

	public static Transform SearchForChild(this Transform transform, string name)
	{
		foreach (Transform t in transform)
		{
			if (t.name == name)
				return t;
			var found = t.SearchForChild(name);
			if (found != null)
				return found;
		}
		return null;
	}
}
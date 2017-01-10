using UnityEngine;
using System.Collections;

public enum SoundType
{
	SoundEffect,
	Ambiance,
	Voice,
	Music
}
public class AudioManager 
{
	private static float _music = 0.5f;
	private static float _soundEffects = 1.0f;
	private static float _ambiance = 1.0f;
	private static float _voice = 1.0f;

	public static float MusicVolume
	{
		get
		{
			return _music;
		}
		set
		{
			_music = value;
			PlayerPrefs.SetFloat("musicVolume", value);
		}
	}
	public static float SoundEffectsVolume
	{
		get 
		{
			return _soundEffects;
		} 
		set 
		{
			_soundEffects = value;
			PlayerPrefs.SetFloat("effectsVolume", value);
		} 
	}
	public static float AmbianceVolume
	{
		get
		{
			return _ambiance;
		}
		set
		{
			_ambiance = value;
			PlayerPrefs.SetFloat("ambienceVolume", value);
		}
	}

	public static float VoiceVolume
	{
		get
		{
			return _voice;
		}
		set
		{
			_voice = value;
			PlayerPrefs.SetFloat("voiceVolume", value);
		}
	}

	static AudioManager()
	{

		if (PlayerPrefs.HasKey("musicVolume"))
		{
			_music = PlayerPrefs.GetFloat("musicVolume");
		}
		else
		{
			PlayerPrefs.SetFloat("musicVolume", _music);
		}
		if (PlayerPrefs.HasKey("effectsVolume"))
		{
			_soundEffects = PlayerPrefs.GetFloat("effectsVolume");
		}
		else
		{
			PlayerPrefs.SetFloat("effectsVolume", _soundEffects);
		}
		if (PlayerPrefs.HasKey("ambienceVolume"))
		{
			_ambiance = PlayerPrefs.GetFloat("ambienceVolume");
		}
		else
		{
			PlayerPrefs.SetFloat("ambienceVolume", _ambiance);
		}
		if (PlayerPrefs.HasKey("voiceVolume"))
		{
			_voice = PlayerPrefs.GetFloat("voiceVolume");
		}
		else
		{
			PlayerPrefs.SetFloat("voiceVolume", _voice);
		}
	}
}

using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MainMenu : MonoBehaviour
{
	public enum menuState
	{
		Main,
		Options,
		Audio,
		Graphics,
		Gameplay,
	}
	public Texture2D background;
	public Texture2D title;
	public Texture2D start;
	public Texture2D credits;
	public Texture2D exit;
	public Texture2D highlight;
	public AudioClip voiceOverSound;
	public AudioClip soundEffectSound;
	public AudioClip ambianceSound;
	public menuState state = menuState.Main;

	private bool clearPersistance = false;
	private bool persistanceCleared = false;
	bool sountEffectVolumeChange = false;
	bool ambianceVolumeChange = false;
	bool voiceVolumeChange = false;
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			switch (state)
			{
				case menuState.Main:
					Application.Quit();
					break;
				case menuState.Options:
					state = menuState.Main;
					break;
				case menuState.Audio:
				case menuState.Graphics:
				case menuState.Gameplay:
					state = menuState.Options;
					break;
				default:
					break;
			}
		}
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(0, 50, 1000, 500), title, ScaleMode.StretchToFill);
		switch (state)
		{
			case menuState.Main:
				drawMainMenu();
				break;
			case menuState.Options:
				drawOptionsMenu();
				break;
			case menuState.Audio:
				drawAudioOptionsMenu();
				break;
			case menuState.Graphics:
				drawGraphicsOptionsMenu();
				break;
			case menuState.Gameplay:
				drawGameplayOptionsMenu();
				break;
			default:
				drawMainMenu();
				break;
		}
	}
	private void drawOptionsMenu()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 200));
		using (new GuiCenter())
		{
			GUILayout.BeginVertical();
			if (GUILayout.Button("Graphics"))
				state = menuState.Graphics;
			if (GUILayout.Button("Audio"))
				state = menuState.Audio;
			if (GUILayout.Button("Gameplay"))
				state = menuState.Gameplay;
			GUILayout.Space(10);
			if (GUILayout.Button("Back"))
				state = menuState.Main;
			GUILayout.EndVertical();
		}
		GUILayout.EndArea();
	}

	private void drawGameplayOptionsMenu()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 200));
		using (new GuiCenter())
		{
			if (clearPersistance)
			{
				GUILayout.BeginVertical();
				GUILayout.Label("Are you sure you want to clear your notebook?");
				GUILayout.BeginHorizontal();
				if(GUILayout.Button("Yes"))
				{
					clearPersistance = false;
					persistanceCleared = true;
					PlayerPrefs.SetString("FoundNotes", "");
				} 
				if (GUILayout.Button("NO!"))
				{
					clearPersistance = false;
				}
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			}
			else
			{
				GUILayout.BeginVertical();
				if (persistanceCleared)
					GUILayout.Label("Persistance Cleared!");
				else
					if (GUILayout.Button("Clear Persistance"))
					{
						clearPersistance = true;
						persistanceCleared = false;
					}
				GUILayout.EndVertical();
			}
		}

		using (new GuiCenter())
		{
			if (GUILayout.Button("Back"))
			{
				persistanceCleared = false;
				state = menuState.Options;
			}
		}

		GUILayout.EndArea();
	}
	private void drawGraphicsOptionsMenu()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 300, Screen.height / 2, 600, 300));
		Screen.fullScreen = GUILayout.Toggle(Screen.fullScreen, "Full Screen");

		if (Screen.fullScreen)
		{
			GUILayout.Label("Resolution");
			int res = Array.IndexOf(Screen.resolutions, Screen.currentResolution);
			var resolutions = Screen.resolutions.Select(r => String.Format("{0}X{1}X{2}Hz", r.width, r.height, r.refreshRate)).ToList();
			int newResolution = GUILayout.SelectionGrid(res, resolutions.ToArray(), 5);
			if (res != newResolution)
			{
				var newRes = Screen.resolutions[newResolution];
				Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen, newRes.refreshRate);
			}
		}
		else
		{
			using (new GuiCenter())
			{
				GUILayout.Label("Resolution");
				GUILayout.Label(String.Format("{0}X{1}", Screen.width, Screen.height));
			}
		}

		GUILayout.Label("Quality Level");
		var level = QualitySettings.GetQualityLevel();
		var newLevel = GUILayout.SelectionGrid(level, QualitySettings.names, QualitySettings.names.Length);
		if(newLevel != level)
		{
			QualitySettings.SetQualityLevel(newLevel, true);
		}

		using (new GuiCenter())
		{
			if (GUILayout.Button("Back"))
				state = menuState.Options;
		}
		GUILayout.EndArea();
	}
	private void drawAudioOptionsMenu()
	{
		var e = Event.current;
		if ((e.type == EventType.mouseUp))
			OnMouseUp();

		GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2, 400, 200));
		using (new GuiCenter())
		{
			GUILayout.Label("Music Volume");
		}		
		using(new GuiCenter())
		{
			AudioManager.MusicVolume = GUILayout.HorizontalSlider(AudioManager.MusicVolume, 0.0f, 1.0f, GUILayout.Width(300));
		}
		using (new GuiCenter())
		{
			GUILayout.Label("Sound Effects Volume");
		}
		using (new GuiCenter())
		{
			float oldVolume = AudioManager.SoundEffectsVolume;
			AudioManager.SoundEffectsVolume = GUILayout.HorizontalSlider(oldVolume, 0.0f, 1.0f, GUILayout.Width(300));
			if (AudioManager.SoundEffectsVolume != oldVolume)
				sountEffectVolumeChange = true;
		}
		using (new GuiCenter())
		{
			GUILayout.Label("Ambiance Volume");
		}
		using (new GuiCenter())
		{
			float oldVolume = AudioManager.AmbianceVolume;
			AudioManager.AmbianceVolume = GUILayout.HorizontalSlider(AudioManager.AmbianceVolume, 0.0f, 1.0f, GUILayout.Width(300));
			if (AudioManager.AmbianceVolume != oldVolume)
				ambianceVolumeChange = true;
		}
		using (new GuiCenter())
		{
			GUILayout.Label("Voice Over Volume");
		}
		using (new GuiCenter())
		{
			float oldVolume = AudioManager.VoiceVolume;
			AudioManager.VoiceVolume = GUILayout.HorizontalSlider(AudioManager.VoiceVolume, 0.0f, 1.0f, GUILayout.Width(300));
			if (AudioManager.VoiceVolume != oldVolume)
				voiceVolumeChange = true;
		}
		using (new GuiCenter())
		{
			if (GUILayout.Button("Back"))
				state = menuState.Options;
		}
		GUILayout.EndArea();
	}
	void OnMouseUp()
	{
		if(sountEffectVolumeChange)
		{
			sountEffectVolumeChange = false;
			this.PlaySoundEffect(soundEffectSound, SoundType.SoundEffect);
		}
		if(ambianceVolumeChange)
		{
			ambianceVolumeChange = false;
			this.PlaySoundEffect(ambianceSound, SoundType.Ambiance);
		}
		if(voiceVolumeChange)
		{
			voiceVolumeChange = false;
			this.PlaySoundEffect(voiceOverSound, SoundType.Voice);
		}
	}
	void drawMainMenu()
	{
		Rect startRect = new Rect((Screen.width / 5) - 60, Screen.height/2 - 50, 150, 75);
		GUI.DrawTexture(startRect, start, ScaleMode.ScaleToFit);
		if (startRect.Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(startRect, highlight, ScaleMode.ScaleToFit);
			if (Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel(2);
			}
		}
		
		Rect optionsRect = new Rect((Screen.width / 5) - 60, Screen.height/2 + 30, 150, 50);
		if(GUI.Button(optionsRect, "Options"))
		{
			state = menuState.Options;
		}

		Rect creditsRect = new Rect((Screen.width / 5) - 50, Screen.height/2 + 100, 150, 75);
		GUI.DrawTexture(creditsRect, credits, ScaleMode.StretchToFill);
		if (creditsRect.Contains(Event.current.mousePosition))
		{
			GUI.DrawTexture(creditsRect, highlight, ScaleMode.ScaleToFit);
			if (Input.GetMouseButtonDown(0))
			{
				Application.LoadLevel("Credits");
			}
		}

		if (!Application.isWebPlayer)
		{
			Rect exitRect = new Rect(Screen.width / 5 - 60, Screen.height / 2 + 200, 150, 75);
			GUI.DrawTexture(exitRect, exit, ScaleMode.ScaleToFit);
			if (exitRect.Contains(Event.current.mousePosition))
			{
				GUI.DrawTexture(exitRect, highlight, ScaleMode.ScaleToFit);
				if (Input.GetMouseButtonDown(0))
				{
					Application.Quit();
				}
			}
		}
	}
	class GuiCenter : IDisposable
	{
		public GuiCenter()
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
		}

		public void Dispose()
		{
			GUILayout.FlexibleSpace();		
			GUILayout.EndHorizontal();
		}
	}
}
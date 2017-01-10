using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour
{
	float progress = 0;
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	// Use this for initialization
	void Start()
	{
	}
	void Play()
	{
		audio.volume = AudioManager.MusicVolume;
		audio.Play();
	}
	// Update is called once per frame
	void Update()
	{
		if (Application.isWebPlayer)
		{
			progress = Application.GetStreamProgressForLevel(2);
		}
		if (Input.anyKeyDown)
		{
			FindObjectOfType<LevelExit>().LoadLevel();
		}
	}
	void OnGUI()
	{
		if (Application.isWebPlayer && progress < 0.99)
		{
			float width = 120;
			float height = 32;
			float hPos = Screen.width / 2 - width/2;
			float vPos = (Screen.height - height / 2) - 20;
			GUI.DrawTexture(new Rect(hPos, vPos, width, height), progressBarEmpty);
			GUI.DrawTexture(new Rect(hPos, vPos, width * Mathf.Clamp01(progress), height), progressBarFull);
			GUI.Box(new Rect(hPos, vPos, width, height), "Loading...");
		}
	}
}

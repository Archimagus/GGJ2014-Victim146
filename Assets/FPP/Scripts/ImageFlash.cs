using UnityEngine;
using System.Collections;

public class ImageFlash : MonoBehaviour 
{
	public Texture Image;
	public float displayTime = 0.25f;

	void OnEnable()
	{
		Invoke("Disable", displayTime);
	}
	void Disable()
	{
		gameObject.SetActive(false);
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Image);
	}
}

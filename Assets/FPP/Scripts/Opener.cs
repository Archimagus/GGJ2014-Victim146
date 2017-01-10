using UnityEngine;
using System.Collections;

public class Opener : MonoBehaviour 
{
	public AudioClip soundEffect;
	void Start()
	{
	}
	public void Open()
	{
		animation.Play();
		var hilight = GetComponent<PickUpHighlight>();
		if (hilight != null)
		{
			hilight.enabled = false;
		}
		if (soundEffect != null)
			this.PlaySoundEffect(soundEffect, SoundType.SoundEffect);
		enabled = false;
	}
}

using UnityEngine;
using System.Collections;

public class VisibilitySoundTrigger : MonoBehaviour 
{
	public AudioClip clip;
	public bool oneTime = true;
	bool lostVisibilty = true;
	// Use this for initialization
	void Start () 
	{
		if(renderer == null)
		{
			Debug.Log("No renderer on visibility trigger.");
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(renderer.isVisible && lostVisibilty )
		{
			this.PlaySoundEffect(clip, SoundType.SoundEffect);
			lostVisibilty = false;
			if (oneTime)
			{
				enabled = false;
			}
		}
		else if(!renderer.isVisible)
		{
			lostVisibilty = true;
		}
	}
}

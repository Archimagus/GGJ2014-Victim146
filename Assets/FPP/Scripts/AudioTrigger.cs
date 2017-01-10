using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider), typeof(RandomizedAudioLoop))]
public class AudioTrigger : MonoBehaviour 
{
	public bool oneTime = true;
	public bool playOnTriggerEnter = true;
	public bool playOnTriggerExit = false;
	bool triggered = false;
	RandomizedAudioLoop looper;
	// Use this for initialization
	void Start () 
	{
		looper = GetComponent<RandomizedAudioLoop>();
		GetComponent<SphereCollider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	void OnTriggerEnter(Collider other)
	{
		if (playOnTriggerEnter)
		{
			if (other.CompareTag("Player"))
			{
				if (oneTime && triggered)
					return;
				triggered = true;
				looper.Play();
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			if (playOnTriggerExit)
			{
				if (oneTime && triggered)
					return;
				triggered = true;
				looper.Play();
			}
			else
			{
				looper.Stop();
			}
		}
	}
}

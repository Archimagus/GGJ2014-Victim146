using UnityEngine;
using System.Collections;
using System;

public class Trigger : MonoBehaviour 
{
	public int TriggerId;
	public event Action<Trigger> TriggerEntered;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && TriggerEntered != null)
		{
			TriggerEntered(this);
		}
	}
}

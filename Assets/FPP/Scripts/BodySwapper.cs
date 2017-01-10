using UnityEngine;
using System.Collections;
using System;

public class BodySwapper : MonoBehaviour 
{
	public bool oneTime = true;
	public GameObject[] objectsToDisable;
	public GameObject[] objectsToEnable;
	public Activation[] actionsToTrigger;

	// Use this for initialization
	void Start () 
	{
		foreach (var go in objectsToEnable)
		{
			go.SetActive(false);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (!CheckVisibility())
				return;
			foreach (var go in objectsToDisable)
			{
				go.SetActive(false);
			}
			foreach (var go in objectsToEnable)
			{
				go.SetActive(true);
			}
			foreach(var a in actionsToTrigger)
			{
				a.targetObject.SendMessage(a.message, SendMessageOptions.DontRequireReceiver);
			}
			if (oneTime)
				collider.enabled = false;
		}
	}
	bool CheckVisibility()
	{
		foreach (var go in objectsToDisable)
		{
			var renderers = go.GetComponentsInChildren<Renderer>();
			foreach (var r in renderers)
			{
				if (r.isVisible)
					return false;				
			}
		}
		return true;
	}
	
}

[Serializable]
public class Activation
{
	public GameObject targetObject;
	public string message;
}
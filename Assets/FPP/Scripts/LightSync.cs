using UnityEngine;
using System.Collections;

public class LightSync : MonoBehaviour 
{
	Light _parentLight;
	// Use this for initialization
	void Start () 
	{
		_parentLight = transform.parent.gameObject.light;
	}
	
	// Update is called once per frame
	void Update () 
	{
		light.intensity = _parentLight.intensity;
	}
}

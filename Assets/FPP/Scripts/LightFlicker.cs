using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour
{
	public float minIntensity;
	public float maxIntensity;
	public float flickerRate;

	float baseRange;
	float targetIntensity;
	float flickerVelocity;
	float changeTime = 0;
	// Use this for initialization
	void Start () 
	{
		baseRange = light.range;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - changeTime > flickerRate)
		{
			targetIntensity = Random.Range(minIntensity, maxIntensity);
			changeTime = Time.time;
		}
		light.intensity = Mathf.SmoothDamp(light.intensity, targetIntensity, ref flickerVelocity, flickerRate);
		light.range = baseRange * light.intensity;
	}
}

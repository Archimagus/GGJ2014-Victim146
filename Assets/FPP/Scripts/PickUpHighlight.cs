using UnityEngine;
using System.Collections;

public class PickUpHighlight : MonoBehaviour 
{
	public MeshRenderer meshToHilight;
	public Material hilightMaterial;
	public float hilightFadeTime = 0.1f;

	Material defaultMaterial;
	float hilightFinishTime;
	bool hilight;

	void Start()
	{
		if (meshToHilight == null)
			meshToHilight = GetComponent<MeshRenderer>();
		defaultMaterial = meshToHilight.material;
	}
	// Update is called once per frame
	void Update () 
	{
		if(hilight && Time.time > hilightFinishTime)
		{
			OnDisable();
		}
	}
	void OnDisable()
	{
		hilight = false;
		meshToHilight.material = defaultMaterial;
	}
	public void Hilight()
	{
		if(!hilight && enabled)
		{
			hilight = true;
			meshToHilight.material = hilightMaterial;
		}
		hilightFinishTime = Time.time + hilightFadeTime;
	}
}

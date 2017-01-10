using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour 
{
	public int levelToLoad = 2;

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			LoadLevel();
		}
	}
	public void LoadLevel()
	{
		StopCoroutine("loadLevel");
		StartCoroutine("loadLevel");
	}

	IEnumerator loadLevel()
	{
		while (!Application.CanStreamedLevelBeLoaded(2))
		{
			yield return null;
		}
		Application.LoadLevel(levelToLoad);
	}
}

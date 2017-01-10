using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class NoteSpawner : MonoBehaviour 
{
	public int numberOfNotesToSpawn = 7;
	public NoteData[] randomNotes;
	public Note note146;
	public Note notePrefab;
	public List<int> foundNotes = new List<int>();
	void Start () 
	{		
		var locations = GameObject.FindGameObjectsWithTag("NoteSpawner").ToList();
		if(PlayerPrefs.HasKey("FoundNotes"))
		{
			var strings = PlayerPrefs.GetString("FoundNotes");
			if (!string.IsNullOrEmpty(strings))
			{
				strings = strings.Trim();
				var intStrings = strings.Split(new []{' '});
				var ints = intStrings.Select(s => int.Parse(s));
				foundNotes.AddRange(ints);
			}
		}
		var availableNotes = randomNotes.Where(n => !foundNotes.Contains(n.NoteNumber)).ToList();
		for (int i = 0; i < numberOfNotesToSpawn; i++)
		{
			if(availableNotes.Count > 0)
			{
				var data = availableNotes[Random.Range(0, availableNotes.Count)];
				availableNotes.Remove(data);
				var loc = locations[Random.Range(0, locations.Count)];
				locations.Remove(loc);
				var note = Instantiate(notePrefab, loc.transform.position, loc.transform.rotation) as Note;
				note.data = data;
			}
			else
			{
				if (note146 != null)
					note146.gameObject.SetActive(true);
				break;
			}
		}
	}
}

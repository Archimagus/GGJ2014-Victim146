using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Player : MonoBehaviour
{
	public float clickRange = 2.0f;
	public NoteDisplay noteDisplay;
	public static bool lockCursor = true;

	private NoteSpawner noteSpawner;
	Animator animator;
	List<int> keys = new List<int>();
	Queue<char> cheatCodes = new Queue<char>();
	void Awake()
	{
		if(!PlayerPrefs.HasKey("FoundNotes"))
		{
			PlayerPrefs.SetString("FoundNotes", "");
		}
		if(!noteDisplay.gameObject.activeInHierarchy)
		{
			noteDisplay = Instantiate(noteDisplay) as NoteDisplay;
		}
		animator = GetComponentInChildren<Animator>();
	}
	// Use this for initialization
	void Start()
	{
		Screen.lockCursor = true;
		noteSpawner = gameObject.GetComponent<NoteSpawner>();
	}
	public bool HasKey(int key)
	{
		return keys.Contains(key);
	}
	public PickUpHighlight mouseOverObject;
	// Update is called once per frame
	void Update()
	{

		if (Screen.lockCursor != lockCursor)
		{
			Screen.lockCursor = lockCursor;
		}
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
		{
			mouseOverObject = hit.collider.gameObject.GetComponent<PickUpHighlight>();
			if (mouseOverObject != null)
				mouseOverObject.Hilight();
		}

		// Check if we clicked on a pickup
		if (Input.GetMouseButtonDown(0) && mouseOverObject != null && hit.distance < clickRange)
		{
			animator.SetTrigger("Pickup");
			if (hit.collider.tag == "Note")
			{
				var note = hit.collider.gameObject.GetComponent<Note>();
				if (note.data.NoteNumber != -1)
				{
					PlayerPrefs.SetString("FoundNotes", PlayerPrefs.GetString("FoundNotes") + " " + note.data.NoteNumber);
					noteSpawner.foundNotes.Add(note.data.NoteNumber);

				}
				if (note.data.VoiceOver != null)
					this.PlaySoundEffect(note.data.VoiceOver, SoundType.Voice);
				var display = noteDisplay;
				display.NoteText = note.data.Text;
				display.Background = note.data.Background;
				display.gameObject.SetActive(true);
				Destroy(hit.collider.gameObject);
			}
			else if (hit.collider.tag == "Key")
			{
				keys.Add(hit.collider.gameObject.GetComponent<Key>().ID);
				Destroy(hit.collider.gameObject);
			}
			else if(hit.collider.tag == "Openable")
			{
				var opener = hit.collider.gameObject.GetComponent<Opener>();
				opener.Open();
			}

		}

		// Cheat Codes
		foreach (char c in Input.inputString) {
			if (c == "\b"[0])
				break;
			else if (c == "\n"[0] || c == "\r"[0])
				break;
			else
				cheatCodes.Enqueue(c);
		}
		while(cheatCodes.Count > 10)
		{
			cheatCodes.Dequeue();
		}
		string codes = new string(cheatCodes.ToArray());
		if(codes.Contains("allkeys"))
		{
			cheatCodes.Clear();
			for (int i = 0; i < 10; i++)
			{
				keys.Add(i);
			}
		} 
		if (codes.Contains("allnotes"))
		{
			cheatCodes.Clear();
			for (int i = 0; i < 146; i++)
			{
				PlayerPrefs.SetString("FoundNotes", "");
				noteSpawner.foundNotes.Clear();
				StringBuilder sb = new StringBuilder();
				foreach (var note in noteSpawner.randomNotes)
				{
					sb.Append(" ");
					sb.Append(note.NoteNumber);
					noteSpawner.foundNotes.Add(note.NoteNumber);					
				}
				PlayerPrefs.SetString("FoundNotes", sb.ToString());
			}
		}
	}
}

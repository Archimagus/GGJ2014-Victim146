using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NotesMenu : MonoBehaviour {
	public Sprite noteBack;
	public Texture2D noteIcon;
	public Texture2D cancel;

	private FirstPersonCharacter character;
	private List<int> noteList;
	private NoteData[] randomNotes;
	private NoteDisplay noteDisplay;
	// Use this for initialization
	void Start()
	{
		noteList = GetComponent<NoteSpawner>().foundNotes;
		randomNotes = GetComponent<NoteSpawner>().randomNotes;
		noteDisplay = GetComponent<Player>().noteDisplay;
	}
	// Update is called once per frame
	void Update () {
		noteList = GameObject.FindObjectOfType<NoteSpawner>().foundNotes;
		noteList.Sort();
		character = gameObject.GetComponent<FirstPersonCharacter>();
	}


	public Vector2 scrollPosition;

	void OnGUI()
	{
		if (character != null && character.inNoteMenu)
		{
			GUI.BeginGroup(new Rect(150, 50, 150, Screen.height));
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(120), GUILayout.Height(Screen.height - 100));

			for (int i = 0; i < noteList.Count; ++i)
			{
				if (noteList[i] != -1)
				{
					GUIContent temp = new GUIContent(noteList[i].ToString(), noteIcon);
					if (GUILayout.Button(temp, GUILayout.MaxHeight(80), GUILayout.MaxWidth(80)))
					{
						var display = noteDisplay;
						display.NoteText = randomNotes[noteList[i]-1].Text;
						display.Background = noteBack;
						display.gameObject.SetActive(true);
					}
				}
			}

			GUILayout.EndScrollView();
			GUI.EndGroup();

			if (GUI.Button(new Rect(50, 53, 80, 80), cancel))
			{
				character.inNoteMenu = false;
				character.InMenu = true;
			}
		}
	}
}

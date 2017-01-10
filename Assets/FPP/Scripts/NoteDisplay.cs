using UnityEngine;
using System.Collections;

public class NoteDisplay : MonoBehaviour 
{
	public string NoteText { get { return textMesh.text; } set { textMesh.text = value; } }
	public Sprite Background { get { return noteSpriteObject.sprite; } set { noteSpriteObject.sprite = value; } }

	public TextMesh textMesh;
	public SpriteRenderer noteSpriteObject;
	private FirstPersonCharacter character;
	void Start()
	{
		character = GameObject.FindObjectOfType<FirstPersonCharacter>();
	}

	void Update()
	{
		character.InNote = true;
		Player.lockCursor = false;

		if (Input.GetMouseButtonDown(0))
		{
			gameObject.SetActive(false);
			character.InNote = false;
			if (!character.InMenu && !character.inNoteMenu)
			{
				Player.lockCursor = true;
			}
		}
	}

	void OnGUI()
	{

	}
}

using UnityEngine;
using System.Collections;
using System;

public class Note : MonoBehaviour
{
	public NoteData data;
}
[Serializable]
public class NoteData
{
	public int NoteNumber;
	[Multiline]
	public string Text;
	public AudioClip VoiceOver;
	public Sprite Background;
}

using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour
{

    public Texture2D enter;
    public Texture2D exit;
    public Texture2D cancel;
    public Texture2D resume;
    public Texture2D highlight;
    public Texture2D note;

    private FirstPersonCharacter character;
    private NotesMenu notes;
    private bool showQuitWindow;
    // Use this for initialization
    void Start()
    {
        character = gameObject.GetComponent<FirstPersonCharacter>();
        showQuitWindow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (character.InMenu)
            {
                Player.lockCursor = true;
                character.InMenu = false;
            }
            else
            {
                if (!character.InNote)
                {
					Player.lockCursor = false;
                    character.InMenu = true;
                }
            }

            if (showQuitWindow)
            {
                showQuitWindow = false;
            }
        }
    }

    public Rect WindowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150);
    void OnGUI()
    {
        if (character.InMenu)
        {
            if (!showQuitWindow)
            {
                Rect resumeRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 150, 75);
                GUI.DrawTexture(resumeRect, resume, ScaleMode.ScaleToFit);
                if(resumeRect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(resumeRect, highlight, ScaleMode.StretchToFill);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Player.lockCursor = true;
                        character.InMenu = false;
                    }
                }

                Rect notesRect = new Rect(Screen.width / 2 - 50, Screen.height / 2, 150, 75);
                GUI.DrawTexture(notesRect, note, ScaleMode.ScaleToFit);
                if (notesRect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(notesRect, highlight, ScaleMode.StretchToFill);
                    if (Input.GetMouseButtonDown(0))
                    {
                        character.inNoteMenu = true;
                        character.InMenu = false;
                    }
                }

                Rect exitRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 100, 150, 75);
                GUI.DrawTexture(exitRect, exit, ScaleMode.ScaleToFit);
                if (exitRect.Contains(Event.current.mousePosition))
                {
                    GUI.DrawTexture(exitRect, highlight, ScaleMode.ScaleToFit);
                    if (Input.GetMouseButtonDown(0))
                    {
                        showQuitWindow = true;
                    }
                }
            }
            else
            {
                WindowRect = GUI.Window(0, WindowRect, MakeQuitWinowButtons, "Quit the Game?");
            }
        }
    }

    void MakeQuitWinowButtons(int windowID)
    {
        Rect enterRect = new Rect(25, 50, 100, 50);
        GUI.DrawTexture(enterRect, enter, ScaleMode.ScaleToFit);
        if (enterRect.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(enterRect, highlight, ScaleMode.ScaleToFit);
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("MainMenu");
            }
        }

        Rect cancelRect = new Rect(175, 50, 100, 50);
        GUI.DrawTexture(cancelRect, cancel, ScaleMode.ScaleToFit);
        if (cancelRect.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(cancelRect, highlight, ScaleMode.ScaleToFit);
            if (Input.GetMouseButtonDown(0))
            {
                showQuitWindow = false;
            }

        }

    }
}

using UnityEngine;
using System.Collections;

public class RollCreds : MonoBehaviour {

	public Texture2D background;
	public float ScrollSpeed = 0.0f ;
	private float WaitTime = 1f ;

	// Use this for initialization
	void Start ()
	{
		Screen.lockCursor = false;
		guiText.color = Color.white ;
		guiText.alignment = TextAlignment.Center ;
		guiText.fontSize = 16 ;
		guiText.richText = true ;
		guiText.text = "<size=24><b>Credits</b></size>\n\n\n\n\n\n\n\n\n\n\n" ;

		guiText.text += "\n\n\n\n\n<size=22><b>Soop's Swagtastik GGJ Team</b></size>" ;
		guiText.text += "\n\n\n<size=20><b>Executive Producer</b></size>" ;
		guiText.text += "\nRupert \"SoopRoop\" Meghnot" ;
		guiText.text += "\n\n\n<size=20><b>External Producer</b></size>" ;
		guiText.text += "\nMicah Brown" ;
		guiText.text += "\n\n\n<size=20><b>Programming Director</b></size>" ;
		guiText.text += "\nPeter Haigis" ;
		guiText.text += "\n\n\n<size=20><b>Art Director</b></size>" ;
		guiText.text += "\nKate Neeves" ;
		guiText.text += "\n\n\n<size=20><b>Design Lead</b></size>" ;
		guiText.text += "\nJosh Liebel";
		guiText.text += "\nMalcolm Ward";
		guiText.text += "\n\n\n<size=20><b>Audio Director</b></size>" ;
		guiText.text += "\nPatrick Flynn";

		// For the Credits list, copy+paste the following line and replace
		// INSERT_NAME with your name; place it under the respective Team,
		// right before the line that says #endregion __________ .
		// Male sure to include the semicolon (;) at the end of the line.
		// from here                                      to here
		// --------> guiText.text += "\nINSERT_NAME" ;  <------
		guiText.text += "\n\n\n\n\n\n\n\n\n\n<size=22><b>Sour Foot Games</b></size>" ;
		#region Lead Prod
		guiText.text += "\n\n\n<size=20><b>Lead Producer</b></size>" ;
		//
		// List the PRODUCERS here
		//
		guiText.text += "\nMichael Meyer" ;
		#endregion Lead Prod



		#region Producers
		guiText.text += "\n\n\n\n\n<size=20><b>Producers</b></size>" ;
		//
		// List the PRODUCERS here
		//
		guiText.text += "\nJesse Janowski" ;
		guiText.text += "\nJean-Karlo Accetta" ;
		guiText.text += "\nChong Han Lim" ;
		guiText.text += "\nJose Bueno" ;
		#endregion Producers



		#region Lead Prog
		guiText.text += "\n\n\n\n\n<size=20><b>Lead Programmer</b></size>" ;
		//
		// List the PRODUCERS here
		//
		guiText.text += "\nJesse Pascoe" ;
		#endregion Lead Prog



		#region Dev Team
		guiText.text += "\n\n\n\n\n<size=20><b>Development Team</b></size>" ;
		//
		// List the DEVELOPMENT TEAM here
		//
		guiText.text += "\nMatthew Genskow" ;
		guiText.text += "\nJesse Pascoe" ;
		guiText.text += "\nJean-Karlo Accetta" ;
		#endregion Dev Team



		#region Lead Art
		guiText.text += "\n\n\n\n\n<size=20><b>Lead Artist</b></size>" ;
		//
		// List the Art Team
		//
		guiText.text += "\nEric Shipp" ;
		#endregion Lead Art



		#region Art Team
		guiText.text += "\n\n\n\n\n<size=20><b>Art Team</b></size>" ;
		//
		// List the Art Team
		//
		guiText.text += "\nEric Shipp" ;
		guiText.text += "\nKate Neeves" ;
		guiText.text += "\nMike Biachinni" ;
		//guiText.text += "\nThomas" ;
		guiText.text += "\nJacob Garbani" ;
		guiText.text += "\nOscar Araque" ;
		#endregion Art Team



		#region Lead Design
		guiText.text += "\n\n\n\n\n<size=20><b>Lead Designers</b></size>" ;
		//
		// List the Design Team
		//
		guiText.text += "\nKate Neeves" ;
		guiText.text += "\nGabriel Orlandelli";
		#endregion Lead Design



		#region Design Team
		guiText.text += "\n\n\n\n\n<size=20><b>Design Team</b></size>" ;
		//
		// List the Design Team
		//
		guiText.text += "\nKate Neeves" ;
		guiText.text += "\nGabriel Orlandelli";
		guiText.text += "\nJulian Doiley";
		guiText.text += "\nYoussef Ezzeldin";
		guiText.text += "\nMatthew Frassetti";

		#endregion Design Team



		#region Level Developer
		guiText.text += "\n\n\n\n\n<size=20><b>Lead Level Developer</b></size>" ;
		//
		// List the Creative Writers
		//
		guiText.text += "\nDaniel Adamitskiy" ;
		#endregion Level Developer



		#region Lead Writer
		guiText.text += "\n\n\n\n\n<size=20><b>Lead Writer</b></size>" ;
		//
		// List the Creative Writers
		//
		guiText.text += "\nMatthew Frassetti" ;
		#endregion Lead Writer



		#region Creative Writers
		guiText.text += "\n\n\n\n\n<size=20><b>Creative Writers</b></size>" ;
		//
		// List the Creative Writers
		//
		guiText.text += "\nJulian Doiley" ;
		guiText.text += "\nKate Neeves" ;
		guiText.text += "\nJesse Janowski" ;
		guiText.text += "\nMatthew Frassetti" ;
		#endregion Creative Writers



		#region Beats
		guiText.text += "\n\n\n\n\n<size=20><b>Music and Sound</b></size>" ;
		//
		// List the Creative Writers
		//
		guiText.text += "\nAaron Cook" ;
		guiText.text += "\nKangvei 'Scott' Chen" ;
		guiText.text += "\nJustin Black" ;
		#endregion Beats



		#region Acknowledgements
		guiText.text += "\n\n\n\n\n<size=20><b>Acknowledgements</b></size>" ;
		//
		// List any acknowledegments
		//
		guiText.text += "\nSteve VanZandt" ;
        guiText.text += "\nSusan Meghnot";
        guiText.text += "\nDunkin' Donuts";
		guiText.text += "\nTyson Hinz" ;
		guiText.text += "\nJack Leske" ;
		guiText.text += "\nChinmay Purohit" ;
		guiText.text += "\nTremaine Williams" ;
		#endregion Acknowledgements


	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( WaitTime <= 0 )
		{
			transform.position += (Vector3.up * ScrollSpeed / 100 * Time.deltaTime );
		}
		else
			WaitTime -= Time.deltaTime ;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect(Screen.width / 10 , Screen.height - 75, 100 , 50), "Go Back"))
		{
			Application.LoadLevel ("MainMenu");
		}
	}
}
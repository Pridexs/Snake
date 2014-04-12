using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public float GUIPlacementY1 = 0.63f;

	public float GUIPlacementY2;
	public float GUIPlacementY3;
	public float GUIPlacementY4;
	public float GUIPlacementY5;

	public GUIStyle play_button;
	public GUIStyle exit_button;
	public GUISkin toggle_button;

	private bool main = true;
	private bool gameSettings = false;

	private bool easyButton = true;
	private bool mediumButton = false;
	private bool hardButton = false;
	private bool hideMenu = false;

	public float delay;

	void Start() {
		if (PlayerPrefs.GetInt ("FirstTime") == 0) {
			PlayerPrefs.SetInt ("FirstTime", 1); 
			PlayerPrefs.SetInt ("HighScoreE", 0);
			PlayerPrefs.SetInt ("HighScoreM", 0);
			PlayerPrefs.SetInt ("HighScoreH", 0);
			PlayerPrefs.SetInt ("HighScoreU", 0);
		}

	}

	void OnGUI() {

		GUI.skin = toggle_button; 
			
		if (!hideMenu) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

			if (main) {
				if (GUI.Button (new Rect (Screen.width * 0.25f, Screen.height * 0.5f, Screen.width * 0.5f, Screen.height * 0.1f), "", play_button)) {
					main = false;
					gameSettings = true;

				}
				
				if (GUI.Button (new Rect (Screen.width * 0.25f, Screen.height * GUIPlacementY1, Screen.width * 0.5f, Screen.height * 0.1f), "", exit_button)) {
					Application.Quit ();
				}
			} else if (gameSettings) {
				easyButton = GUI.Toggle (new Rect(Screen.width * 0.25f, Screen.height * GUIPlacementY2, Screen.width * 0.5f, Screen.height * 0.05f), easyButton, "Easy");

				if ( easyButton ) {
					mediumButton = false;
					hardButton = false;
					delay = 0.2f;
				}

				mediumButton = GUI.Toggle (new Rect(Screen.width * 0.25f, Screen.height * GUIPlacementY3, Screen.width * 0.5f, Screen.height * 0.05f), mediumButton, "Medium");

				if ( mediumButton ) {
					easyButton = false;
					hardButton = false;
					delay = 0.1f;
				}

				hardButton = GUI.Toggle (new Rect(Screen.width * 0.25f, Screen.height * GUIPlacementY4, Screen.width * 0.5f, Screen.height * 0.05f), hardButton, "Hard");

				if ( hardButton ) {
					easyButton = false;
					mediumButton = false;
					delay = 0.05f;
				}

				if (!hardButton && !mediumButton && !easyButton) {
					delay = 0.03f;
				}

				if (GUI.Button (new Rect (Screen.width * 0.25f, Screen.height * GUIPlacementY5, Screen.width * 0.5f, Screen.height * 0.1f), "", play_button)) {
					Application.LoadLevel ("_Level01");
					hideMenu = true;
				}
			}
		}

	}

	void Awake() {
		DontDestroyOnLoad(this.gameObject);

	}
}

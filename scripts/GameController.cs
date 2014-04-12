using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Texture backgroundTexture;
	public GameObject snake;

	public float offsetY1;
	public float offsetY2;
	public float offsetY3;
	public float offsetX1;
	public float offsetX2;
	public float offsetX3;
	public GUIText size;

	public GUIStyle button_restart;
	public GUIStyle button_menu;
	public GUIStyle text_style;

	private string difficulty;
	private int highScore;

	void Start () {
		difficulty = ((SnakeHead)snake.GetComponent(typeof(SnakeHead))).getDifficulty();
		
		highScore = PlayerPrefs.GetInt (difficulty);

	}

	void Update () {
		//Exit if back key is pressed.
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void OnGUI() {
		int score = GameObject.FindGameObjectsWithTag ("SnakeBody").Length;
		size.text = "Size: " + score;

		if ( ((SnakeHead) snake.GetComponent (typeof(SnakeHead))).isDead ){

			if (score > highScore) {
				highScore = score;
				PlayerPrefs.SetInt (difficulty, highScore);
			}

			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

			if (GUI.Button (new Rect (Screen.width * offsetX1, Screen.height * offsetY1, Screen.width * 0.3f, Screen.height * 0.1f), "", button_restart)) {
				Application.LoadLevel ( "_Level01" );
			}

			if (GUI.Button (new Rect (Screen.width * offsetX2, Screen.height * offsetY2, Screen.width * 0.3f, Screen.height * 0.1f), "", button_menu)) {
				Destroy (GameObject.FindGameObjectWithTag("GameSettings"));
				Application.LoadLevel ( "MainMenu" );
			}

			GUI.Label(new Rect(Screen.width * offsetY3, Screen.height * offsetX3, Screen.width * 0.5f, Screen.height * 0.2f), "RECORD: " + highScore, text_style);
		}
	}
}

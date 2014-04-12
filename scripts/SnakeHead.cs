using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeHead : MonoBehaviour {

	public float speed = 1.0f; 
	public int xAxis = 1, zAxis = 0;
	public float movementDelay = 0.2f;
	public GameObject snakeBody;
	public GUIText movement;
	public bool isDead = false;

	private List<GameObject> bodies;

	private float timeElapsed = 0.0f;
	private float threshold = 6.0f;
	private Vector3 lastMovement;
	private int nBodies = 0;

	
	private SnakeBody bodyScript;
	
	void Start () {

		Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
		GameObject gameSettings = GameObject.FindGameObjectWithTag ("GameSettings");

		if (gameSettings != null){
			MainMenu script = (MainMenu) gameSettings.GetComponent (typeof(MainMenu));
			movementDelay = script.delay;
		} else {
			movementDelay = 0.2f;
		}

		bodies = new List<GameObject>();
		lastMovement = new Vector3 (1.0f, 0.0f, 0.0f);
		CreateBody (3);
	}
	
	void Update () {
		
		// Android Movement 
		if (Input.touches.Length > 0) {
			if (Input.touches[0].phase == TouchPhase.Moved) {
				float x = Input.touches[0].deltaPosition.x;
				float y = Input.touches[0].deltaPosition.y;
				
				if ( (Mathf.Abs(x) >= threshold) || (Mathf.Abs(y) >= threshold) ) {
					if (Mathf.Abs(x) >= Mathf.Abs(y) && (x != 0 || y != 0)) {
						if (x > 0) zAxis = -1; else zAxis = 1;
						xAxis = 0;
					} else {
						zAxis = 0;
						if (y > 0) xAxis = 1; else xAxis = -1;
					}
				}
			}
		}

		// Windows movement.
		if (Input.GetKeyDown ("a")) {
			zAxis = 1; xAxis = 0;
		} else if (Input.GetKeyDown ("d")) {
			zAxis = -1; xAxis = 0;
		} else if (Input.GetKeyDown ("w")) {
			zAxis = 0; xAxis = 1; 
		} else if (Input.GetKeyDown ("s")) {
			zAxis = 0; xAxis = -1;
		} else if (Input.GetKeyDown ("f")) {
			CreateBody(5);
		}
		
		
		if (timeElapsed >= movementDelay && !isDead) {
			Vector3 movement = new Vector3 (xAxis * speed, 0, zAxis * speed);
			
			// Cant go back
			if ( (movement.x == -lastMovement.x && movement.x != 0) || (movement.z == -lastMovement.z && movement.z != 0) )
				movement = lastMovement;
			
			transform.Translate (movement);

			for (int i = 0; i < nBodies; i++) {
				SnakeBody script = (SnakeBody)bodies[i].GetComponent(typeof(SnakeBody));
				script.Move ();

				if (i == 0) {
					script.UpdateMovement(movement);
				} else {
					script.UpdateMovement(((SnakeBody)bodies[i-1].GetComponent(typeof(SnakeBody))).lastMovement);
				}
			}
			
			lastMovement = movement;
			timeElapsed = 0.0f;
			
		}
		timeElapsed += Time.deltaTime;
	}
	
	public void CreateBody(int quantity) {
		for (int i = nBodies; i < nBodies+quantity; i++) {

			float positionX;
			float positionY = transform.position.y;
			float positionZ;

			Vector3 movement = new Vector3();
			
			if (i == 0) {
				positionX = transform.position.x - 1;
				positionZ = transform.position.z;
				movement = lastMovement;
			} else {
				SnakeBody script = (SnakeBody)bodies[i-1].GetComponent(typeof(SnakeBody));
				positionX = bodies[i-1].transform.position.x - script.queuedMovement.x;
				positionZ = bodies[i-1].transform.position.z - script.queuedMovement.z;
				movement = script.queuedMovement;
			}

			GameObject go = Instantiate(snakeBody, new Vector3(positionX,positionY, positionZ), Quaternion.identity) as GameObject;
			GameObject mainObject =  GameObject.FindGameObjectWithTag("Snake");
			go.transform.parent = mainObject.transform;
			((SnakeBody)go.GetComponent(typeof(SnakeBody))).UpdateMovement(movement, movement);

			bodies.Add (go);
		}
		nBodies += quantity;	
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "SnakeBody") {
			//Application.LoadLevel ("Game_Over");
			isDead = true;
		}
	}

	public string getDifficulty() {
		if (movementDelay == 0.2f) {
			return "HighScoreE"; //Easy
		} else if (movementDelay == 0.1f) {
			return "HighScoreM"; //Medium
		} else if (movementDelay == 0.05f) {
			return "HighScoreH"; //Hard
		} else {
			return "HighScoreU"; //Ultra
		}
	}
}

using UnityEngine;
using System.Collections;

public class SnakeBody : MonoBehaviour {

	public Vector3 queuedMovement;
	public Vector3 lastMovement;
	private float timeElapsed;
	
	
	void Start () {
		timeElapsed = 0.0f;
		lastMovement = new Vector3 (0.0f, 0.0f, 0.0f);
		//renderer.enabled  = false;
	}
	
	void Update () {
		timeElapsed += Time.deltaTime;

		
	}
	
	public void UpdateMovement(Vector3 movement) {
		queuedMovement = movement;
	}

	//Only called when created
	public void UpdateMovement(Vector3 movement, Vector3 lastMovement) {
		queuedMovement = movement;
		lastMovement = movement;
	}

	public void Move() {
		transform.Translate (queuedMovement);
		lastMovement = queuedMovement;
	}
}

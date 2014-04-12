using UnityEngine;
using System.Collections;

public class FruitCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col) {
		SnakeHead script = ((SnakeHead)col.gameObject.GetComponent (typeof(SnakeHead)));
		if(script != null) {
			script.CreateBody(1);
			Destroy(this.gameObject);
		}
	}
}

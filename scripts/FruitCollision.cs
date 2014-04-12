using UnityEngine;
using System.Collections;

public class FruitCollision : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		SnakeHead script = ((SnakeHead)col.gameObject.GetComponent (typeof(SnakeHead)));
		if(script != null) {
			script.CreateBody(1);
			Destroy(this.gameObject);
		}
	}
}

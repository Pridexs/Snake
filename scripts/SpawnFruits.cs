using UnityEngine;
using System.Collections;

public class SpawnFruits : MonoBehaviour {

	public GameObject fruit;

	private int nFruits = 0;

	void Update () {
		if (GameObject.FindGameObjectWithTag("Fruit") == null) {
			createFruit();
		}
	}

	void createFruit() {
		GameObject[] snake = GameObject.FindGameObjectsWithTag ("SnakeBody");
		bool intersects = false;
		Vector3 position = new Vector3();
		//This can be optimized.
		do {
			intersects = false;
			int positionX = (int) (Random.value * 12);
			int positionZ = (int) (Random.value * 7);

			if (Random.value > 0.5) 
				positionX = -positionX;

			if (Random.value > 0.5)
				positionZ = -positionZ;

			position = new Vector3 (positionX, 1.0f, positionZ);

			for (int i  = 0; i < snake.Length; i++) {
				Vector3 positionSnake = snake[i].gameObject.transform.position;
				if ( (positionSnake.x == positionX) && (positionSnake.z == positionZ) ) {
					intersects = true;
					break;
				}
			}
		} while(intersects == true);
		Instantiate (fruit, position, Quaternion.identity); 
	}
}

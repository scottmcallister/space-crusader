using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public Sprite skin1;
	public Sprite skin2;
	public float rotationSpeed;
	public GameObject tinyAsteroid;
	public Transform spawn1;
	public Transform spawn2;

	// Use this for initialization
	void Start () {
		int random = Random.Range (1, 3);
		switch (random) {
			case 1:
				GetComponent<SpriteRenderer>().sprite = skin1;
				break;
			case 2:
				GetComponent<SpriteRenderer>().sprite = skin2;
				break;
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RotateLeft ();
	}

	void RotateLeft(){
		transform.Rotate (Vector3.forward * -90 * rotationSpeed);
	}

}

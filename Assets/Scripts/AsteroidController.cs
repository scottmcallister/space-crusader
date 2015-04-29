using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public Sprite skin1;
	public Sprite skin2;
	public Sprite skin3;
	public float rotationSpeed;

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
			default:
				GetComponent<SpriteRenderer>().sprite = skin3;
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

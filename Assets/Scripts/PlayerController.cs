using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	
	public float xMin, xMax;
}

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public Boundary boundary;
	public Sprite defaultSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;
	public Sprite spinSprite;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Shoot
		if (Input.GetKey (KeyCode.Space) && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}

		/* 
		 * 	MOVEMENT
		 *

		// Move left
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime * -1.0f);
		}

		// Move right
		else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
		}

		// Spin
		else if (Input.GetKey (KeyCode.D)) {
			GetComponent<SpriteRenderer>().sprite = spinSprite;
		} */

		else {
			GetComponent<SpriteRenderer>().sprite = defaultSprite;
		}


	}

	// FixedUpdate is used to update physics changes
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		
		Vector2 movement = new Vector2 (moveHorizontal, 0.0f); 
		
		GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;
		
		GetComponent<Rigidbody2D>().position = new Vector2 (
			Mathf.Clamp(GetComponent<Rigidbody2D>().position.x,boundary.xMin, boundary.xMax), 
			-20.0f);
	}
	
}

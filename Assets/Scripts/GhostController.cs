using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Sprite[] colors;
	public GameObject[] projectiles;
	public Sprite hurtImage;
	public Transform shotSpawn;
	public float fireRate;
	public GameObject projectile;
	public float moveSpeed;
	public Boundary boundary;

	private string direction;
	private float nextFire;
	private GameObject GameController;

	// Custom constructor for specific colors
	public GhostController(int index){
		if (index > 0 && index < 4) {
			GetComponent<SpriteRenderer>().sprite = colors[index];
			projectile = projectiles[index];
		}
		else{
			// generate random number between 0 and 3
			RandomColor();
		}
	}
	
	void Start () {
		if (projectile == null) {
			RandomColor();
		}
		direction = "Left";
	}

	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
			//GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate(){

		Vector2 movement;
		

		if (direction == "Left") {
			// Move left
			movement = Vector2.right * -1;
		} 
		else {
			// Move right
			movement = Vector2.right;
		}

		GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;
		
		GetComponent<Rigidbody2D>().position = new Vector2 (
			Mathf.Clamp(GetComponent<Rigidbody2D>().position.x,boundary.xMin, boundary.xMax), 
			transform.position.y);
		if (GetComponent<Rigidbody2D> ().position.x == boundary.xMax && direction == "Right" ||
			GetComponent<Rigidbody2D> ().position.x == boundary.xMin && direction == "Left") {

			SwitchDirections();
		}
	}

	void RandomColor(){
		int rand = Random.Range (0, 4);
		GetComponent<SpriteRenderer> ().sprite = colors [rand];
		projectile = projectiles [rand];
	}

	void SwitchDirections(){
		if (direction == "Left") {
			direction = "Right";
		}
		else{
			direction = "Left";
		}
	}

	void OnCollisionEnter(Collision collision){
		/*if (collision.gameObject.tag == "Projectile") {
			Physics.IgnoreCollision(collision, this);
		}*/
	}
}

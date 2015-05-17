using UnityEngine;
using System.Collections;

public class BeeMover : MonoBehaviour {

	public Vector2 endPoint;
	public float speed;
	public float rotationSpeed;
	public float startLoop;
	public float endLoop;
	public float startFormation;
	public float startDive;
	public float xPos;
	public float yPos;
	public Transform shotSpawn; 
	public GameObject projectile;
	public GameObject[] powerUps;

	private GameObject player;
	private Vector2 target;
	private Vector3 rotation;
	private Rigidbody2D body;
	private bool rotate;
	private bool diving;
	private Mover projectileMover;
	private bool canShoot;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		body.velocity = transform.up * speed;
		rotation = new Vector3 (0.0f, 0.0f, rotationSpeed);
		startLoop += Time.time;
		endLoop += Time.time;
		startFormation += Time.time;
		startDive += Time.time;
		target = new Vector2 (xPos, yPos);
		diving = false;
		player = GameObject.FindWithTag ("Player");
		projectileMover = projectile.GetComponent<Mover> ();
		canShoot = true;
	}

	// Called every 0.2 seconds or so
	void FixedUpdate(){
		float currTime = Time.time;
		if (currTime > startLoop && currTime < endLoop) {
			Loop ();
		}
		else if(currTime > startDive){
			Dive ();
		}
		else if(currTime > startFormation && diving == false){
			AttackFormation();
		}
		else {
			// Before loop after spawning
			body.velocity = transform.up * speed;
		}
		if(transform.position.y < -32.0f){
			Respawn ();
		}
	}
	
	// Kamikaze dive toward player
	void Dive(){
		diving = true;
		if(transform.position.y > -15.0f && player != null){
			Vector3 vectorToTarget = player.transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			angle *= 3;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			//transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 400.0f);
			transform.rotation = Quaternion.Euler(0,0,180);
			float step = 35.0f * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
		}
		else{
			//transform.rotation = Quaternion.Euler(0,0,180);
			body.velocity = transform.up * 35.0f;
			//canShoot = true;
		}

		if (transform.position.y < 10.0f) {
			if(canShoot)
				Shoot();
		}
	}

	// Initial Spawn Loop
	void Loop(){
		transform.Rotate (rotation);
		body.velocity = transform.up * speed;
	}

	// Respawn after diving to bottom of screen
	void Respawn(){
		diving = false;
		transform.position = new Vector3(-5.0f, 30.0f, 0.0f);
		startDive = Time.time + 5.0f;
		canShoot = true;
	}

	// Line up in a neat row
	void AttackFormation(){
		transform.rotation = Quaternion.identity;
		body.velocity = transform.up * 0.0f;
		float step = 20.0f * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, target, step);
	}

	// Shoot a projectile
	void Shoot(){
		projectileMover.speed = 50.0f;
		Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
		canShoot = false;
	}

	public void SpawnPowerUp(){
		int randomIndex = Random.Range (0, 5);
		Instantiate(powerUps[randomIndex], shotSpawn.position, Quaternion.identity);
	}
}

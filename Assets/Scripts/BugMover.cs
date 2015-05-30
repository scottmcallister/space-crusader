using UnityEngine;
using System.Collections;

public class BugMover : MonoBehaviour {

	// values that start circular motion when crossed on either axis
	public float xTrigger;
	public float yTrigger;
	public float radius;
	public float delay;
	public Vector3 direction;
	public float circlespeed;
	public float speed;
	public float spinrate;
	public bool offsetIsCenter;
	public Vector3 offset;
	public GameObject projectile;
	public Transform shotSpawn;
	public bool leftspawn;
	public GameObject[] powerUps;

	private bool circling;
	private float loopstart;
	private float nextFire;
	private float fireRate;

	// Use this for initialization
	void Start () {
		circling = false;
		projectile.GetComponent<Mover> ().speed = -50;
		ResetFireRate ();
	}
	
	// Update is called once per frame
	void Update () {
		if (leftspawn) {
			Debug.Log("Left");
			if ((transform.position.x > xTrigger || 
				transform.position.y < yTrigger) && circling == false) {
				StartCircling ();
			}
		}
		else {
			Debug.Log ("Right");
			if ((transform.position.x < xTrigger || 
			     transform.position.y < yTrigger) && circling == false) {
				StartCircling ();
			}
		}


		if (Time.time > nextFire ) {
			ResetFireRate();
			Instantiate(projectile, shotSpawn.position, Quaternion.identity);
		}
	}

	void FixedUpdate(){
		if(circling){
			// Update direction
			transform.position = new Vector2(
				(radius * Mathf.Cos((Time.time-loopstart)*spinrate))+offset.x,
				(radius * Mathf.Sin((Time.time-loopstart)*spinrate))+offset.y);
		}
		else{
			GetComponent<Rigidbody2D>().velocity = direction * speed;
		}

	}

	void ResetFireRate(){
		fireRate = Random.Range (0.5f, 3.5f);
		nextFire = Time.time + fireRate;
	}

	void StartCircling(){
		circling = true;
		loopstart = Time.time + delay;
	}

	public void SpawnPowerUp(){
		int randomIndex = Random.Range (0, 4);
		Instantiate(powerUps[randomIndex], shotSpawn.position, Quaternion.identity);
	}
}

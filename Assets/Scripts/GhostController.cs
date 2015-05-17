using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Sprite[] colors;
	public GameObject[] powerUps;
	public Sprite hurtImage;
	public Transform shotSpawn;
	public float fireRate;
	public GameObject projectile;

	private float nextFire;
	private GameObject GameController;
	private bool hurt;

	// Custom constructor for specific colors
	public GhostController(int index){
		if (index > 0 && index < 4) {
			GetComponent<SpriteRenderer>().sprite = colors[index];
		}
		else{
			// generate random number between 0 and 3
			RandomColor();
		}
	}

	// Called when object is initialized
	void Start () {
		if (projectile == null) {
			RandomColor();
		}
		hurt = false;
		projectile.GetComponent<Mover>().speed = -40.0f;
	}

	// Called every frame
	void Update () {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(projectile, shotSpawn.position, Quaternion.identity);
			//GetComponent<AudioSource>().Play ();
		}
	}



	void RandomColor(){
		int rand = Random.Range (0, 4);
		GetComponent<SpriteRenderer> ().sprite = colors [rand];
	}



	public void HurtGhost(){
		hurt = true;
		GetComponent<SpriteRenderer>().sprite = hurtImage;
		fireRate = 0.75f;
	}

	public bool IsHurt(){
		return hurt;
	}

	public void SpawnPowerUp(){
		int randomIndex = Random.Range (0, 5);
		Instantiate(powerUps[randomIndex], shotSpawn.position, shotSpawn.rotation);
	}
}

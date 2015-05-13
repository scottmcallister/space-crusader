using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	
	public float xMin, xMax, yMin, yMax;
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
	public float spinRate;
	public float spinLength;
	public bool spinning;
	public int health;

	private float nextFire;
	private float nextSpin;
	private float spinStop;
	private float initFireRate;
	private GameObject initShot;
	private Animator animator;
	private GameController gameController;


	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find game controller!");
		}
		gameController.UpdateHealth (health);

		// store initial fire rate and shot to revert back to after power ups have worn out
		initFireRate = fireRate;
		initShot = shot;

		// setup animations
		animator = GetComponent<Animator> ();
		animator.SetBool ("Spin", false);
	}
	
	// Update is called once per frame
	void Update () {

		// Shoot
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}

		if(Input.GetKey (KeyCode.Space) && Time.time > nextSpin){
			nextSpin = Time.time + spinRate;
			spinStop = Time.time + spinLength;
			animator.SetBool("Spin", true);
			spinning = true;
			// play audio sound
		}
		else if(Time.time > spinStop && animator.GetBool("Spin")){
			animator.SetBool ("Spin", false);
			spinning = false;
		}
	}

	// FixedUpdate is used to update physics changes
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		
		Vector2 movement = new Vector2 (moveHorizontal, 0.0f); 
		
		GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;

		animator.SetFloat ("Speed", movement.x);
		
		GetComponent<Rigidbody2D>().position = new Vector2 (
			Mathf.Clamp(GetComponent<Rigidbody2D>().position.x,boundary.xMin, boundary.xMax), 
			-20.0f);
	}

	public IEnumerator PowerUp(PowerUpController powerUp){
		fireRate = powerUp.fireRate;
		shot = powerUp.shot;
		yield return new WaitForSeconds (5);	
		fireRate = initFireRate;
		shot = initShot;
	}

	public IEnumerator Shrink(){
		// shrink

		yield return new WaitForSeconds (5);
		// grow back
	}

	public void TakeHit(){
		health --;
		gameController.UpdateHealth (health);
		if (health == 0)
			Die ();
	}

	public void Heal(){
		health++;
		gameController.UpdateHealth (health);
	}

	public void Die(){
		gameController.GameOver ();
		Destroy (gameObject);
		return;
	}
	
}

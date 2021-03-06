﻿using UnityEngine;
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
	public GameObject explosion;
	public Transform shotSpawn;
	public float fireRate;
	public float spinRate;
	public float spinLength;
	public bool spinning;
	public int health;
	public AudioClip takeDamage;
	public AudioClip deflectDamage;
	public AudioClip explosionClip;

	private float nextFire;
	private float nextSpin;
	private float spinStop;
	private float initFireRate;
	private GameObject initShot;
	private Animator animator;
	private GameController gameController;
	private SpriteRenderer sr;
	private Color defaultColor;
	private AudioSource[] audioArr;
	private bool poweredUp = false;


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

		sr = GetComponent<SpriteRenderer>();
		audioArr = GetComponents<AudioSource> ();
		defaultColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {

		// Shoot
		if (Time.time > nextFire && (Input.GetKey(KeyCode.F) || poweredUp)) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//audioArr[0].Play ();
		}

		// Spin starts
		if(Input.GetKey (KeyCode.Space) && Time.time > nextSpin){
			nextSpin = Time.time + spinRate;
			spinStop = Time.time + spinLength;
			animator.SetBool("Spin", true);
			//StartCoroutine(FlashColor (Color.cyan, spinLength));
			spinning = true;
			// play audio sound
			audioArr[1].Play ();
		}

		// Spin ends
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

	// Upgrade lazer
	public IEnumerator PowerUp(PowerUpController powerUp){
		poweredUp = true;
		fireRate = powerUp.fireRate;
		shot = powerUp.shot;
		yield return new WaitForSeconds (5);	
		fireRate = initFireRate;
		shot = initShot;
		poweredUp = false;
	}

	public void TakeHit(){
		health --;
		gameController.UpdateHealth (health);
		if (health == 0)
			Die ();
		else {
			AudioSource.PlayClipAtPoint (takeDamage, transform.position);
			StartCoroutine (FlashColor (Color.red, 0.10f));
		}
	}

	public IEnumerator FlashColor(Color c, float duration){
		sr.color = c;
		yield return new WaitForSeconds (duration);
		sr.color = defaultColor;
	}

	public void Heal(){
		health++;
		gameController.UpdateHealth (health);
	}

	public void Die(){
		Instantiate(explosion, transform.position, Quaternion.identity);
		gameController.GameOver ();
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint (explosionClip, transform.position);
		return;
	}
	public void DeflectHit(){
		StartCoroutine (FlashColor (Color.green, 0.10f));
		AudioSource.PlayClipAtPoint (deflectDamage, transform.position);
	}
	
}

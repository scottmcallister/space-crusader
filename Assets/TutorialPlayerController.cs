using UnityEngine;
using System.Collections;

public class TutorialPlayerController : MonoBehaviour {

	public float moveSpeed;
	public Boundary boundary;
	public GameObject shot;
	public GameObject explosion;
	public Transform shotSpawn;
	public float fireRate;
	public float spinRate;
	public float spinLength;
	public bool spinning;
	
	private float nextFire;
	private float nextSpin;
	private float spinStop;
	private float initFireRate;
	private GameObject initShot;
	private Animator animator;
	private SpriteRenderer sr;
	private Color defaultColor;
	private AudioSource[] audioArr;
	private bool poweredUp = false;
	
	
	// Use this for initialization
	void Start () {
		
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
}

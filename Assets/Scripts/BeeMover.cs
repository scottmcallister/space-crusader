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
	public GameObject player;

	private Vector2 target;
	private Vector3 rotation;
	private Rigidbody2D body;
	private bool rotate;
	private bool diving;

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
	}

	void FixedUpdate(){
		float currTime = Time.time;
		if (currTime > startLoop && currTime < endLoop) {
			// spawn loop
			transform.Rotate (rotation);
			body.velocity = transform.up * speed;
		}
		else if(currTime > startDive){
			// dive toward player
			diving = true;
			Debug.Log ("Diving...");
			if(transform.position.y > 0.0f){
				Vector3 vectorToTarget = player.transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				angle *= 2;
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 190.0f);
				float step = 20.0f * Time.deltaTime;
				transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
			}
			else{
				transform.rotation = Quaternion.Euler(0,0,180);
				body.velocity = transform.up * 28.0f;
				
			}
		}
		else if(currTime > startFormation && diving == false){
			// move towards target
			transform.rotation = Quaternion.identity;
			body.velocity = transform.up * 0.0f;
			float step = 20.0f * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, target, step);
		}
		else {
			body.velocity = transform.up * speed;
		}
		if(transform.position.y < -32.0f){
			// respawn up top
			Debug.Log ("respawn...");
			diving = false;
			transform.position = new Vector3(-5.0f, 30.0f, 0.0f);
			startDive = Time.time + 5.0f;
		}
	}

	void Dive(){


	}

}

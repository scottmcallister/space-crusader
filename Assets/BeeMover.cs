using UnityEngine;
using System.Collections;

public class BeeMover : MonoBehaviour {

	public Vector2 endPoint;
	public float speed;
	public float rotationSpeed;
	public float startLoop;
	public float endLoop;
	public float startFormation;

	private Vector3 rotation;
	private Rigidbody2D body;
	private bool rotate;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		body.velocity = transform.up * speed;
		rotate = true;
		rotation = new Vector3 (0.0f, 0.0f, rotationSpeed);
		startLoop += Time.time;
		endLoop += Time.time;
		startFormation += Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		float currTime = Time.time;
		if (currTime > startLoop && currTime < endLoop) {
			transform.Rotate (rotation);
			body.velocity = transform.up * speed;
		}
		else if(currTime > startFormation){
			// test stop
			body.velocity = transform.up * 0.0f;
		}
		else {
			rotate = false;
			//transform.Rotate (new Vector3(0.0f, 0.0f, 0.0f));
			body.velocity = transform.up * speed;
		}
	}
}

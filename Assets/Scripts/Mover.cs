using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public Vector3 direction;

	// Use this for initialization
	void Start () {
		if (direction == Vector3.zero)
			direction = transform.up;
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

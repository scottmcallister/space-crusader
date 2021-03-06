﻿using UnityEngine;
using System.Collections;

public class FlightPath : MonoBehaviour {
	
	private string direction;
	public float moveSpeed;
	public Boundary boundary;
	private Animator animator;

	// Use this for initialization
	void Start () {
		boundary.yMax = transform.position.y + 2.0f;
		direction = "Down";
		animator = GetComponent<Animator> ();
	}

	void FixedUpdate(){
		
		Vector2 movement;
		Rigidbody2D ghostBody = GetComponent<Rigidbody2D> ();
		
		// Convert to switch...
		if (direction == "Left") {
			// Move left
			movement = Vector2.right * -1;
		} 
		else if (direction == "Down"){
			movement = Vector2.up * -1;
		}
		else {
			// Move right
			movement = Vector2.right;
		}
		
		ghostBody.velocity = movement * moveSpeed;

		ghostBody.position = new Vector2 (
			Mathf.Clamp(ghostBody.position.x,boundary.xMin, boundary.xMax), 
			transform.position.y);
		if ((ghostBody.position.x == boundary.xMax && direction == "Right") ||
		    (ghostBody.position.x == boundary.xMin && direction == "Left") ||
		    (ghostBody.position.y < boundary.yMin && direction == "Down")) {
			SwitchDirections();
		}
	}

	void SwitchDirections(){
		if (direction == "Left") {
			direction = "Right";
			if(tag == "Ghost")
				animator.SetBool("Left", false);
		} 
		else if (direction == "Down") {
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			direction = "Left";
		}
		else{
			direction = "Left";
			if(tag == "Ghost")
				animator.SetBool("Left", true);
		}
	}
}

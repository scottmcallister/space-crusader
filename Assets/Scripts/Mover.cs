﻿using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

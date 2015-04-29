﻿using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (0.0f, Time.time * speed);

		renderer.material.mainTextureOffset = offset;
	}
}
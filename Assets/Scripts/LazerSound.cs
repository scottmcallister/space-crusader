using UnityEngine;
using System.Collections;

public class LazerSound : MonoBehaviour {

	public AudioClip clip;
	public float volume;

	// Use this for initialization
	void Start () {

		AudioSource.PlayClipAtPoint (clip, transform.position, volume);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

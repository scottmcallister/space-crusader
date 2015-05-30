using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public AudioClip clip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			PlayerController controller = other.gameObject.GetComponent<PlayerController>();
			controller.Heal ();
			AudioSource.PlayClipAtPoint(clip, transform.position);
			Destroy (gameObject);
		}
	}
}

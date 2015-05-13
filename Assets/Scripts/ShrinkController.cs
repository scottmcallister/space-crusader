using UnityEngine;
using System.Collections;

public class ShrinkController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			PlayerController controller = other.gameObject.GetComponent<PlayerController>();
			controller.StartCoroutine(controller.Shrink());
			Destroy (gameObject);
		}
	}
}

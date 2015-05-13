using UnityEngine;
using System.Collections;

public class HourglassController : MonoBehaviour {

	private GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find game controller!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			//GameController controller = gameController.GetComponent<GameController>();
			gameController.StartCoroutine(gameController.SlowDown());
			Destroy (gameObject);
		}
	}
}

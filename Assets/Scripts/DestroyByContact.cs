using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	//public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find game controller!");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Lazer" && tag == "Projectile") {
			return;
		}
		Debug.Log ("other tag: " + other.tag);
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			//Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		if (other.tag == "Ghost") {
			gameController.KillEnemy();
		}
		gameController.AddScore (scoreValue);
		Destroy(other.gameObject);
		Destroy (gameObject);
	}
}

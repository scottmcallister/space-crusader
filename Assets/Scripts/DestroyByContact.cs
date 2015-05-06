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
		if (other.tag == "Boundary") {
			return;
		}
		if (tag == "Ghost") {
			GhostController controller = gameObject.GetComponent<GhostController>();
			if(controller.IsHurt ()){
				gameController.KillEnemy();
				if(gameController.enemyCount == 0){
					// Spawn wave cleared, initialize power up
					GetComponent<GhostController>().SpawnPowerUp();
				}
			}
			else{
				Destroy(other.gameObject);
				controller.HurtGhost ();
				return;
			}
		}
		Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			if(pc.spinning){
				Destroy(gameObject);
				return;
			}
			else{
				pc.TakeHit();
				return;
			}
		}
		gameController.AddScore (scoreValue);
		Destroy(other.gameObject);
		Destroy (gameObject);
	}
}

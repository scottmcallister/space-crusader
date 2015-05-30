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
				ReduceEnemyCount();
				if(gameController.enemyCount == 0){
					GetComponent<GhostController>().SpawnPowerUp();
				}
			}
			else{
				Destroy(other.gameObject);
				controller.HurtGhost ();
				return;
			}
		}
		if (tag == "Bug") {
			ReduceEnemyCount();
			if (gameController.enemyCount == 0) {
				GetComponent<BugMover>().SpawnPowerUp();
			}
		}
		if (tag == "Bee") {
			gameController.AddScore(scoreValue);
			ReduceEnemyCount();
			if(gameController.enemyCount == 0){
				GetComponent<BeeMover>().SpawnPowerUp();
			}
			Instantiate(explosion, transform.position, transform.rotation);
			//gameController.AddScore(scoreValue);
			if(other.tag == "Player"){
				PlayerController pc = other.gameObject.GetComponent<PlayerController>();
				pc.TakeHit();
			}
			else{
				Destroy(other.gameObject);
			}
			Destroy (gameObject);
			return;
		}
		if (tag == "BigAsteroid" || tag == "LittleAsteroid") {
			if(other.tag == "Player"){
				PlayerController pc = other.gameObject.GetComponent<PlayerController>();
				pc.TakeHit();
			}
			else if(tag == "BigAsteroid" && other.tag == "Lazer"){
				AsteroidController ac = gameObject.GetComponent<AsteroidController>();
				Vector2 pos1 = transform.position;
				pos1.x += 2;
				Vector2 pos2 = transform.position;
				pos2.x -= 2;

				GameObject cloneRight = (GameObject)Instantiate(ac.tinyAsteroid, pos1, Quaternion.identity);
				GameObject cloneLeft = (GameObject)Instantiate(ac.tinyAsteroid, pos2, Quaternion.identity);

				cloneRight.GetComponent<Mover>().direction = new Vector3(-1.0f, 1.0f, 0.0f);
				cloneLeft.GetComponent<Mover>().direction = new Vector3(1.0f, 1.0f, 0.0f);
				Destroy(other.gameObject);
			}
			else{
				Destroy(other.gameObject);
			}
			if(tag == "LittleAsteroid" && gameController.enemyCount > 0 && other.tag == "Lazer"){
				ReduceEnemyCount();
				gameController.AddScore(1);
			}
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (gameObject);
			return;
		}

		if (other.tag == "Player") {
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			if(pc.spinning){
				pc.DeflectHit();
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
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	void ReduceEnemyCount(){
		if(gameController.enemyCount > 0){
			gameController.KillEnemy();
		}
	}
}




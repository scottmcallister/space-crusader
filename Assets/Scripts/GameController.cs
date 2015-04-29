using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector2 spawnValues;
	public float spawnWait;
	public float startWait;
	public int score;
	public GUIText scoreText;
	public GUIText pausedText;
	public float savedTimeScale;

	private bool gameOver;
	private bool restart;
	private bool paused;

	// Use this for initialization
	void Start () {
		savedTimeScale = Time.timeScale;
		gameOver = false;
		restart = false;
		paused = false;
		score = 0;
		pausedText.text = "";
		UpdateScore();
		StartCoroutine (SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			TogglePaused();
			
		}
	}

	// Coroutine that spawns asteroids
	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true) {
			Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (hazard, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (spawnWait);
		}
	}

	public void AddScore(int scoreValue){
		score += scoreValue;	
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	void TogglePaused(){
		if (paused) {
			// resume play
			Time.timeScale = savedTimeScale;
			AudioListener.pause = false;
			pausedText.text = "";
			paused = false;
		}
		else{
			// pause game
			savedTimeScale = Time.timeScale;
			Time.timeScale = 0;
			AudioListener.pause = true;
			pausedText.text = "Paused";
			paused = true;
		}

	}
}

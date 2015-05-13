using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector2 spawnValues;
	public float spawnWait;
	public float startWait;
	public float slowmoDuration;
	public int score;
	public GUIText scoreText;
	public GUIText pausedText;
	public GUIText gameOverText;
	public GUIText restartText;
	public GUIText healthText;
	public GUIText highScoreText;
	public float savedTimeScale;
	public float slowmoTimeScale;
	public int enemyCount;

	private bool gameOver;
	private bool restart;
	private bool paused;
	private bool slowmo;

	// Use this for initialization
	void Start () {
		savedTimeScale = Time.timeScale;
		slowmoTimeScale = savedTimeScale / 2;
		slowmo = false;
		gameOver = false;
		restart = false;
		paused = false;
		score = 0;
		pausedText.text = "";
		gameOverText.text = "";
		restartText.text = "";
		highScoreText.text = "High Score " + PlayerPrefs.GetInt ("highscore");
		UpdateScore();
		StartCoroutine (SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			TogglePaused();
			
		}

		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	// Coroutine that spawns asteroids
	IEnumerator SpawnWaves (){
		yield return new WaitForSeconds (startWait);
		while (true) {
			if(enemyCount == 0){
				for(int i = 1; i < 6; i++){
					Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
					Quaternion spawnRotation = Quaternion.identity;
					hazard.gameObject.GetComponent<FlightPath>().boundary.yMin = (i * 5.0f);
					Instantiate (hazard, spawnPosition, spawnRotation);
					enemyCount++;
					yield return new WaitForSeconds (spawnWait);
				}
			}

			yield return new WaitForSeconds(spawnWait);

			if(gameOver){
				yield return new WaitForSeconds (3);
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
		}
	}

	// Coroutine that slows the game down
	public IEnumerator SlowDown(){
		slowmo = true;
		Time.timeScale = slowmoTimeScale;
		yield return new WaitForSeconds (slowmoDuration);
		slowmo = false;
		Time.timeScale = savedTimeScale;
	}

	public void AddScore(int scoreValue){
		score += scoreValue;	
		UpdateScore ();
	}

	public void KillEnemy(){
		enemyCount -= 1;
	}

	void UpdateScore(){
		scoreText.text = "" + score;
	}

	public void UpdateHealth (int newHealth){
		healthText.text = newHealth + "";
	}

	void TogglePaused(){
		if (paused) {
			// resume play
			if(slowmo){
				Time.timeScale = slowmoTimeScale;
			}
			else{
				Time.timeScale = savedTimeScale;
			}
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

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
		StoreHighScore (score);
	}

	void StoreHighScore(int newHighscore){
		int oldHighScore = PlayerPrefs.GetInt ("highscore");
		if(newHighscore > oldHighScore)
			PlayerPrefs.SetInt("highscore", newHighscore);
	}
}

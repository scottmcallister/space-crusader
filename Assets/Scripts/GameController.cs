using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject ghost;
	public GameObject bee;
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
				int random = Random.Range (1, 6); // 
				switch(random){
					case 1:
						StartCoroutine(GhostWave());
						break;
					case 2: 
						StartCoroutine(BeeWaveLeft());
						break;
					case 3:
						StartCoroutine(BeeWaveRight());
						break;
					case 4:
						StartCoroutine(BeeWaveBoth());
						break;
					default:
						StartCoroutine(GhostWave());
						break;
				}

				//StartCoroutine (GhostWave ());
				//StartCoroutine(BeeWaveBoth());


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

	public IEnumerator GhostWave(){
		for(int i = 1; i < 6; i++){
			Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);
			Quaternion spawnRotation = Quaternion.identity;
			ghost.gameObject.GetComponent<FlightPath>().boundary.yMin = (i * 5.0f);
			Instantiate (ghost, spawnPosition, spawnRotation);
			enemyCount++;
			yield return new WaitForSeconds (spawnWait);
		}
	}

	public IEnumerator BeeWaveRight(){
		for (int i = 0; i < 14; i++) {
			Vector2 spawnPosition = new Vector2(20.0f, -15.0f);
			Quaternion spawnRotation = Quaternion.Euler (new Vector3(0.0f, 0.0f, 45f));

			bee.gameObject.GetComponent<BeeMover>().startDive = 3.0f + (i * 0.5f);
			if(i >= 7){
				bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 50.0f;
				bee.gameObject.GetComponent<BeeMover>().yPos = 20;
			}
			else{
				bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 15.0f;
				bee.gameObject.GetComponent<BeeMover>().yPos = 15;
			}
			Instantiate(bee, spawnPosition, spawnRotation);
			enemyCount++;
			yield return new WaitForSeconds (0.2f);
		}
	}

	public IEnumerator BeeWaveLeft(){
		for (int i = 0; i < 14; i++) {
			Vector2 spawnPosition = new Vector2(-20.0f, -15.0f);
			Quaternion spawnRotation = Quaternion.Euler (new Vector3(0.0f, 0.0f, -45.0f));
			bee.gameObject.GetComponent<BeeMover>().rotationSpeed = 10.0f;
			bee.gameObject.GetComponent<BeeMover>().startDive = 3.0f + (i * 0.5f);
			if(i >= 7){
				bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 50.0f;
				bee.gameObject.GetComponent<BeeMover>().yPos = 20;
			}
			else{
				bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 15.0f;
				bee.gameObject.GetComponent<BeeMover>().yPos = 15;
			}
			Instantiate(bee, spawnPosition, spawnRotation);
			enemyCount++;
			yield return new WaitForSeconds (0.2f);
		}
	}

	public IEnumerator BeeWaveBoth (){
		for (int i = 0; i < 7; i++) {
			// Spawn left bee
			Vector2 spawnPosition = new Vector2(-20.0f, -15.0f);
			Quaternion spawnRotation = Quaternion.Euler (new Vector3(0.0f, 0.0f, -45.0f));
			bee.gameObject.GetComponent<BeeMover>().rotationSpeed = 10.0f;
			bee.gameObject.GetComponent<BeeMover>().startDive = 4.5f + (i * 0.5f);
			bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 15.0f;
			bee.gameObject.GetComponent<BeeMover>().yPos = 15;
			Instantiate(bee, spawnPosition, spawnRotation);
			enemyCount++;

			// Spawn right bee
			spawnPosition = new Vector2(20.0f, -10.0f);
			spawnRotation = Quaternion.Euler (new Vector3(0.0f, 0.0f, 45f));
			bee.gameObject.GetComponent<BeeMover>().rotationSpeed = -10.0f;
			bee.gameObject.GetComponent<BeeMover>().startDive = 3.0f + (i * 0.5f);
			bee.gameObject.GetComponent<BeeMover>().xPos = (i * 5.0f) - 15.0f;
			bee.gameObject.GetComponent<BeeMover>().yPos = 20;
			Instantiate(bee, spawnPosition, spawnRotation);
			enemyCount++;

			// Wait for next pair of bee spawns
			yield return new WaitForSeconds (0.2f);
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

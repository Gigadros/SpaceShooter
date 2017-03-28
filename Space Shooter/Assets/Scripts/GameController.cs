using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject enemyShip, enemyShipElite;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait, spawnWait, waveWait, shipSpawnChance;

	public GUIText scoreText, restartText, gameOverText;
	int score;
	bool isGameOver, restart;
	int[] hazardPoolIDs;

	void Awake () {
		StartCoroutine (SpawnWaves ());
		score = 0;
		UpdateScore ();
		isGameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		hazardPoolIDs = new int[3] {2, 3, 4}; // three types of asteroids
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R))
				Application.LoadLevel (Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		int count = 0;
		while(true){
			count++;
			// spawn a wave
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				// spawn ship or asteroid?
				if (Random.Range (0.0f, 1.0f) < shipSpawnChance && score >= 100) { // spawn a ship!
					if (score > Random.Range (200.0f, 4000.0f) && Random.Range (0.0f, 1.0f) < 0.9f) {
						Instantiate (enemyShipElite, spawnPosition, spawnRotation);
					} else {
						Instantiate (enemyShip, spawnPosition, spawnRotation); 
					}
				} else { // spawn an asteroid from the pool
					int hazardPoolID = hazardPoolIDs[(int)Random.Range (0, hazardPoolIDs.Length)];
					GameObject hazardGO = ObjectPooler.current.GetPooledObject(hazardPoolID);
					if (hazardGO == null) yield return null;
					hazardGO.transform.position = spawnPosition;
					hazardGO.transform.rotation = spawnRotation;
					hazardGO.SetActive(true);
				}
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			// make ships appear more often than asteroids as the game goes on
			if (shipSpawnChance < 0.8f)
				shipSpawnChance += 0.02f;
			// increase the wave size every 3 waves
			if (count >= 3)
				hazardCount++;

			if (isGameOver) {
				restartText.text = "Press 'R' to Restart";
				restart = true;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	public void GameOver () {
		gameOverText.text = "Game Over!";
		isGameOver = true;
	}
}

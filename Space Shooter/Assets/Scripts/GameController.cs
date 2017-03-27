using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	//public GameObject[] hazards;
	public GameObject enemyShip;
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
		hazardPoolIDs = new int[3] {2, 3, 4}; //three types of asteroids
	}

	void Update () {
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R))
				Application.LoadLevel (Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while(true){
			for (int i = 0; i < hazardCount; i++) {
				//GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				//spawn ship or asteroid?
				if (Random.Range (0.0f, 1.0f) < shipSpawnChance) {
					Instantiate (enemyShip, spawnPosition, spawnRotation);
				} else {
					int hazardPoolID = hazardPoolIDs[(int)Random.Range (0, hazardPoolIDs.Length)];
					GameObject hazardGO = ObjectPooler.current.GetPooledObject(hazardPoolID);
					if (hazardGO == null) yield return null;
					hazardGO.transform.position = spawnPosition;
					hazardGO.transform.rotation = spawnRotation;
					hazardGO.SetActive(true);
				}

				//Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} else {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Boundary") || other.CompareTag ("Hazard") || other.CompareTag ("Enemy") || other.CompareTag ("Enemy Bolt")) 
			return;
		if (CompareTag ("Enemy Bolt") && other.CompareTag ("Bolt"))
			return;
		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}

		other.gameObject.SetActive(false);
		gameObject.SetActive(false);

		if (CompareTag ("Enemy")) Destroy (gameObject);

		if (other.CompareTag ("Player")) {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
			Destroy (other.gameObject);
		}

		gameController.AddScore (scoreValue);
		//Destroy (other.gameObject);
		//Destroy (gameObject);
	}

}
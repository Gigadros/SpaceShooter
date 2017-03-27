using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other){
		//Destroy (other.gameObject);
		other.gameObject.SetActive(false);
		if (other.CompareTag ("Enemy")) Destroy (other.gameObject);
	}

}

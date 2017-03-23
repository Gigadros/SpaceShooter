using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;
	Rigidbody rb;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}
}

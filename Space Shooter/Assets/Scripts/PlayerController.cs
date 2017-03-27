using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed, tilt, fireRate;
	public Boundary boundary;
	//public GameObject shot;
	public Transform shotSpawn;
	float volMin, volMax, pitchMin, pitchMax;
	AudioSource audioSource;
	Rigidbody rb;
	float nextFire;
	int bulletPoolID;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		bulletPoolID = 0; //player bolt
		volMin = 0.35f;
		volMax = 0.5f;
		pitchMin = 0.9f;
		pitchMax = 1.0f;
	}

	void Update () {
		if(Input.GetButton("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			//Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GameObject bulletGO = ObjectPooler.current.GetPooledObject(bulletPoolID);
			if (bulletGO == null) return;
			bulletGO.transform.position = shotSpawn.transform.position;
			bulletGO.transform.rotation = shotSpawn.transform.rotation;
			bulletGO.SetActive(true);
			audioSource.pitch = Random.Range(pitchMin, pitchMax);
			float vol = Random.Range(volMin, volMax);
			audioSource.PlayOneShot (audioSource.clip, vol);
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

}

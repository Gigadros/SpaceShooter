using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate, delay;
	float volMin, volMax, pitchMin, pitchMax;
	AudioSource audioSource;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		volMin = 0.75f;
		volMax = 0.9f;
		pitchMin = 0.9f;
		pitchMax = 1.1f;
		InvokeRepeating ("Fire", Random.Range (delay*0.75f, delay*1.25f), fireRate);
	}

	void Fire () {
		if (gameObject.active){
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.pitch = Random.Range (pitchMin, pitchMax);
			float vol = Random.Range (volMin, volMax);
			audioSource.PlayOneShot (audioSource.clip, vol);
		}
	}
}
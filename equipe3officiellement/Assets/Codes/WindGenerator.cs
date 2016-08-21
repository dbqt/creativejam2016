using UnityEngine;
using System.Collections;

public class WindGenerator : MonoBehaviour {

	public AudioSource windAudio;
	private float nextSnd = 0.0f;
	public float sndDelay;

	// Use this for initialization
	void Start () {
		windAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSnd) {
			nextSnd = Time.time + sndDelay;
			windAudio.Play ();
		}
	}
}

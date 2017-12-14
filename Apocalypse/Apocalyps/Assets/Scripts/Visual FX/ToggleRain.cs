/**
 * This script keeps track of when to turn rain on/off
 * Jaason Komoda 10/4/17
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRain : MonoBehaviour {

	public GameController gameController;

	public AudioSource rainSound;

    // Use this for initialization
    void Start() {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		rainSound = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update() {

		//play rain if weather is storm mode
		if (!rainSound.isPlaying && gameController.isStorm) {
			gameObject.GetComponent<ParticleSystem>().Play();
			rainSound.Play ();
		}

		//stop rain if weather is not storm mode
		if (!gameController.isStorm) {
			gameObject.GetComponent<ParticleSystem>().Stop();
			gameObject.GetComponent<ParticleSystem>().Clear();
			rainSound.Stop ();
		}
    }
}

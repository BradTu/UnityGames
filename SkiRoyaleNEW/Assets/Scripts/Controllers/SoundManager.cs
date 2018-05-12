/**
 * This script manages the sounds during the race
 * Jason Komoda 11/9/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public GameController gameController;
	[Header ("Game Start and Music")]
	public AudioSource countdown;
	public AudioSource bgMusic;

	public AudioSource p1;
	public AudioSource p2;
	public AudioSource p3;
	public AudioSource p4;
	public AudioSource p1PoleAttack;
	public AudioSource p2PoleAttack;
	public AudioSource p3PoleAttack;
	public AudioSource p4PoleAttack;
	public AudioSource p1RocketIncoming;
	public AudioSource p2RocketIncoming;
	public AudioSource p3RocketIncoming;
	public AudioSource p4RocketIncoming;
	public AudioSource p1UseRocket;
	public AudioSource p2UseRocket;
	public AudioSource p3UseRocket;
	public AudioSource p4UseRocket;

	private AudioSource[] sounds;

    public AudioClip race1Music;
    public AudioClip race2Music;
    public AudioClip race3Music;
	public AudioClip swing;
	public AudioClip itemPickUp;
	public AudioClip slowDown;
	public AudioClip speedUp;
	public AudioClip useRocket;
	public AudioClip snowball;
	public AudioClip sludge;
	public AudioClip rocketHit;






	// Use this for initialization
	void Start () {

		//Get game controller reference
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		sounds = gameObject.GetComponents<AudioSource>();
		countdown = sounds[0];
		bgMusic = sounds[1];
        if(SceneManager.GetActiveScene().name == "Ski_Race1"){
            bgMusic.clip = race1Music;
        }
        else if (SceneManager.GetActiveScene().name == "Ski_Race2")
        {
            bgMusic.clip = race2Music;
        }
        else if (SceneManager.GetActiveScene().name == "Ski_Race3")
        {
            bgMusic.clip = race3Music;
        }
		p1PoleAttack = sounds[2];
		p2PoleAttack = sounds[3];
		p3PoleAttack = sounds[4];
		p4PoleAttack = sounds[5];
		p1RocketIncoming = sounds[6];
		p2RocketIncoming = sounds[7];
		p3RocketIncoming = sounds[8];
		p4RocketIncoming = sounds[9];
		p1 = sounds [10];
		p2 = sounds [11];
		p3 = sounds [12];
		p4 = sounds [13];
		p1UseRocket = sounds [14];
		p2UseRocket = sounds [15];
		p3UseRocket = sounds [16];
		p4UseRocket = sounds [17];
	}

	// Update is called once per frame
	void Update () {
		if (gameController.finishedCountdown){
			if(!bgMusic.isPlaying){
				bgMusic.Play();
			}
		}
	}
}

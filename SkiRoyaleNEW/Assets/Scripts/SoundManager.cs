/**
 * This script manages the sounds during the race
 * Jason Komoda 11/9/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameController gameController;
    private AudioSource countdown;
    private AudioSource bgMusic;
    public AudioSource p1PoleAttack;
    public AudioSource p2PoleAttack;
    public AudioSource p1ItemPickup;
    public AudioSource p2ItemPickup;
    public AudioSource p1SlowDown;
    public AudioSource p2SlowDown;
    public AudioSource p1RocketIncoming;
    public AudioSource p2RocketIncoming;
    public AudioSource p1SpeedUp;
    public AudioSource p2SpeedUp;
    public AudioSource p1UseRocket;
    public AudioSource p2UseRocket;
    public AudioSource p1Snowball;
    public AudioSource p2Snowball;
    public AudioSource p1Sludge;
    public AudioSource p2Sludge;
    public AudioSource p1RocketHit;
    public AudioSource p2RocketHit;

    private AudioSource[] sounds;


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
        p1PoleAttack = sounds[1];
        bgMusic = sounds[2];
        p2PoleAttack = sounds[3];
        p1ItemPickup = sounds[4];
        p2ItemPickup = sounds[5];
        p1SlowDown = sounds[6];
        p2SlowDown = sounds[7];
        p1RocketIncoming = sounds[8];
        p2RocketIncoming = sounds[9];
        p1SpeedUp = sounds[10];
        p2SpeedUp = sounds[11];
        p1UseRocket = sounds[12];
        p2UseRocket = sounds[13];
        p1Snowball = sounds[14];
        p2Snowball = sounds[15];
        p1Sludge = sounds[16];
        p2Sludge = sounds[17];
        p1RocketHit = sounds[18];
        p2RocketHit = sounds[19];
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

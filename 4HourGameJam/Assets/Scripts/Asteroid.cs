//Brad Tully
//18 September 2017
//Asteroid takes 3 hits to destroy and does significant damage if it hits the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{

    public AudioClip explosionSound;
    public AudioSource source;

    // Use this for initialization
    void Start()
    {
        health = 3;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        initializeSpin();
        initializeMovement();
    }

    // Update is called once per frame
    void Update()
    {
        isSpinning();
        updateMovement();
    }

    //Determine what is entering the asteroid
    void OnCollisionEnter2D(Collision2D col)
    {
        // If a missile enters the destroyable part of the target destroy and increment score
        if (col.gameObject.tag == "Missile")
        {
            health = health - 1;
            if (health <= 0)
			{
                gameController.playerScore = gameController.playerScore + 20;
                Destroy(gameObject);
            }
        }
		//possibly put back
		//|| col.gameObject.tag == "Player"
		if (col.gameObject.tag == "EndOfLevel" ) {
			gameController.playerHealth -= 20;
			Destroy (gameObject);
		}
    }

}

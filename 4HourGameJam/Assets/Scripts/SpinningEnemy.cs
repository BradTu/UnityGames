//Brad Tully
//18 September 2017
//This is the spinning enemy class, in order to destroy it the missile must hit one half of it otherwise it won't destroy it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEnemy : Enemy
{

    // Use this for initialization
    void Start()
    {
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

    //Determine what is entering the spinning target
    void OnTriggerEnter2D(Collider2D col)
    {
        // If a missile enters the destroyable part of the target destroy and increment score
        if (col.gameObject.tag == "Missile")
        {
            gameController.playerScore = gameController.playerScore + 20;
            Destroy(gameObject);
			Destroy (col.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If the player collides take out its health
		if (col.gameObject.tag == "EndOfLevel")
        {
			gameController.playerHealth -= 20;
            Destroy(gameObject);
        }
    }





}

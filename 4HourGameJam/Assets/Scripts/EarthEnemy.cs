using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEnemy : Enemy {

	// Use this for initialization
	void Start () {
		health = 20;
		speed = 2;
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		initializeSpin();
		//initializeMovement();
	}
	
	// Update is called once per frame
	void Update () {
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
				gameController.destroyedEarth = true;
				gameController.playerScore = gameController.playerScore + 20;
				Destroy(gameObject);
			}
		}

		if (col.gameObject.tag == "EndOfLevel" || col.gameObject.tag == "Player") {
			gameController.playerHealth = 0;
			Destroy (gameObject);
		}
	}
}

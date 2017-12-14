//Brad Tully
//18 September 2017
//Parent class for all enemy types

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameController gameController;
    public float angle;
    public Vector3 rotationSpeed;
    public Vector3 moveSpeed;
    public float speed;
    public float health;
    public int decrementScore;

    //Allows the enemy object to spin
    public void initializeSpin()
    {
        rotationSpeed.x = 0;
        rotationSpeed.y = 0;
        //Randomly rotate to the left or right
        rotationSpeed.z = Random.Range(-3.0f, 3.0f);
        //How fast it rotates
        angle = Random.Range(1.0f, 3.0f);
    }

    //Makes the enemy spin
    public void isSpinning()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, angle);
    }


	// Use this for initialization
	void Start () {
    GameObject gameControllerObject = GameObject.FindWithTag("GameController");
      if(gameControllerObject != null)
      {
          gameController = gameControllerObject.GetComponent<GameController>();
      }
	}

	// Update is called once per frame
	void Update () {
      
	}

  void OnCollisionEnter2D(Collision2D other)
  {
    // enemy collision with player
	if(other.gameObject.tag == "Player")
    	{
     	 	Destroy(gameObject);
    	}

    // enemy collision with left side of screen
	if(other.gameObject.tag == "EndOfLevel")
    	{
			gameController.playerHealth -= 20;
      		Destroy(gameObject);
    	}
  }

    //Initialize the movement speed of the enemy
    public virtual void initializeMovement()
    {
        //transform.Translate(Vector2.right * -1 * moveSpeed);
        speed = Random.Range(-1, -5);
        moveSpeed.x = speed;
    }

    //Update its movement
    public virtual void updateMovement()
    {
        transform.position = transform.position + (moveSpeed * Time.deltaTime);
        if (transform.position.x < -9)
        {
            Destroy(gameObject);
            gameController.playerScore = gameController.playerScore - decrementScore;
        }
    }


}

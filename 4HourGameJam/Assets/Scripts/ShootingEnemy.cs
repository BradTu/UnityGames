//Brad Tully    
//19 September 2017
//This is the class for the shooting enemy it moves up and down as well as to the left
//It also shoots missiles

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
	public GameObject missle;
	public GameObject misslePrefab;
	public bool spawnMissles;
	public Vector2 missleSpawnPosition;

	private float missleMinTime = 1.0f;
	private float missleMaxTime = 1.5f;

    float speedY;

    // Use this for initialization
    void Start()
    {
		spawnMissles = true;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        initializeMovement();
		StartCoroutine (ShootMissles ());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        updateMovement();
    }

    //Allows the space ship to move up and down as well as horizontal
    public override void initializeMovement()
    {
        speed = Random.Range(-1, -5);
        moveSpeed.x = speed;
        speedY = Random.Range(-3, 3);
        moveSpeed.y = speedY;
    }

    //Updates the movement
    public override void updateMovement()
    {
        if (transform.position.y >= 4 || transform.position.y <= -4)
        {
            moveSpeed.y = moveSpeed.y * -1;
        }
        transform.position = transform.position + (moveSpeed * Time.deltaTime);
        if (transform.position.x < -9)
        {
            Destroy(gameObject);
            gameController.playerScore = gameController.playerScore - decrementScore;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Missile")
        {
            gameController.playerScore = gameController.playerScore + 20;
            Destroy(gameObject);
        }

		if (col.gameObject.tag == "EndOfLevel")
		{
			gameController.playerHealth -= 20;
			Destroy(gameObject);
		}
    }

	IEnumerator ShootMissles(){
		while (spawnMissles) {
			missleSpawnPosition = gameObject.transform.position - new Vector3 (1.0f, 0, 0);
			missle = (GameObject)Instantiate (misslePrefab, missleSpawnPosition, Quaternion.Euler(0, 0, 90));
			yield return new WaitForSeconds (UnityEngine.Random.Range (missleMinTime, missleMaxTime));
		}
	}
}

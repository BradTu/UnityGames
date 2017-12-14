using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int playerHealth;
	public int playerScore;
	public bool destroyedEarth;

	public GameObject spinningEnemy;
	public GameObject spinningEnemyPrefab;
	public GameObject shootingEnemy;
	public GameObject shootingEnemyPrefab;
	public GameObject earth;
	public GameObject earthPrefab;
	public GameObject asteroid;
	public GameObject asteroidPrefab;

	public Text healthText;
	public Text scoreText;
	public Text gameoverLabelText;
	public Text gameoverScoreText;

	private Vector2 enemySpawnPosition;
	private Vector2 earthSpawnPosition;


	private float minTimeSpinning = 4.0f;
	private float maxTimeSpinning = 6.0f;
	private float minTimeShooting = 3.0f;
	private float maxTimeShooting = 5.0f;
	private float minTimeAsteroid = 5.0f;
	private float maxTimeAsteroid = 7.5f;

	private bool alreadySpawnedEarth;
	private bool doSpawn;
	private bool spawnEarth;
	private bool spawnAsteroids;

	// Use this for initialization
	void Start () {
			startGame();
	}

	// Update is called once per frame
	void Update () {
		updateHealthAndScore();
		gameover();
		if (playerScore >= 300) {
			spawnEarth = true;
		}
		SpawnEarth ();
	}

	public void startGame()
	{
		doSpawn = true;
		StartCoroutine (SpawnSpinningEnemies());
		StartCoroutine (SpawnShootingEnemies ());
		StartCoroutine (SpawnAsteroids());
		playerHealth = 100;
		playerScore = 0;
		gameoverLabelText.enabled = false;
		gameoverScoreText.enabled = false;
	}

	public void updateHealthAndScore()
  {
	 	healthText.text = "Health: " + playerHealth;
      scoreText.text = "Score: " + playerScore;
  }

	public void gameover()
	{
		if(playerHealth <= 0)
		{
			doSpawn = false;
			healthText.enabled = false;
			scoreText.enabled = false;
			gameoverLabelText.text = "Gameover!";
			gameoverLabelText.enabled = true;
			gameoverScoreText.text = "Score: " + playerScore;
            gameoverScoreText.enabled = true;
		}

		if (destroyedEarth) {
			doSpawn = false;
			healthText.enabled = false;
			scoreText.enabled = false;
			gameoverLabelText.text = "You Win!";
			gameoverLabelText.enabled = true;
			gameoverScoreText.text = "Score: " + playerScore;
			gameoverScoreText.enabled = true;
		}
	}

	public void SpawnEarth()
	{
		if (spawnEarth && !alreadySpawnedEarth) {
			earthSpawnPosition = new Vector2 (10, 0);
			earth = (GameObject)Instantiate (earthPrefab, earthSpawnPosition, Quaternion.identity);
			alreadySpawnedEarth = true;
		}
	}

	IEnumerator SpawnAsteroids()
	{
		while(doSpawn){
			enemySpawnPosition = new Vector2 (10, UnityEngine.Random.Range (2.9f, -3.9f));
			asteroid = (GameObject)Instantiate (asteroidPrefab, enemySpawnPosition, Quaternion.identity);
			yield return new WaitForSeconds (UnityEngine.Random.Range (minTimeAsteroid, maxTimeAsteroid));
		}
	}

	IEnumerator SpawnSpinningEnemies()
	{
		while (doSpawn) {
			enemySpawnPosition = new Vector2 (10, UnityEngine.Random.Range (2.9f, -3.9f));
			spinningEnemy = (GameObject)Instantiate (spinningEnemyPrefab, enemySpawnPosition, Quaternion.identity);
			yield return new WaitForSeconds (UnityEngine.Random.Range (minTimeSpinning, maxTimeSpinning));
		}
	}

	IEnumerator SpawnShootingEnemies()
	{
		while (doSpawn) {
			enemySpawnPosition = new Vector2 (10, UnityEngine.Random.Range (2.9f, -3.9f));
			shootingEnemy = (GameObject)Instantiate (shootingEnemyPrefab, enemySpawnPosition, Quaternion.identity);
			yield return new WaitForSeconds (UnityEngine.Random.Range (minTimeShooting, maxTimeShooting));
		}
	}
}

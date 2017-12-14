//this is the player class
//has the movement, making you stay on screen, invincibility frames, and collision
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public GameController gameController;

	public float horizontalInput;
	public float verticalInput;
	public float movementSpeed;
	public GameObject player;
	private int life;
	private int invincible;
	private float red;
	private float blue;
	private float green;
	public SpriteRenderer theSprite;
	public SpriteRenderer theColorSprite;

    public AudioClip boomSound;
    public AudioSource source;
    public AudioClip explosionSound;
    public AudioSource source2;

    // Use this for initialization
    void Start () {
		invincible = 300;
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		invincible++;
	}

	void Move(){
		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");
		transform.Translate (transform.right * (horizontalInput * movementSpeed * Time.deltaTime), Space.World);
		transform.Translate (transform.up * (verticalInput * movementSpeed * Time.deltaTime), Space.World);

		if (transform.position.x > 8) {
			transform.position = new Vector3 (transform.position.x - 0.2f, transform.position.y, transform.position.z);
		}
		if (transform.position.x < -8) {
			transform.position = new Vector3 (transform.position.x + 0.2f, transform.position.y, transform.position.z);
		}
		if (transform.position.y > 5) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z);
		}
		if (transform.position.y < -5) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
		}
		//invincibility

		if (invincible < 200) {
			if (red >= .98f) {
				red = 0;
				blue = 0;
			}
			red = Mathf.Lerp (red, .5f, Time.deltaTime);
			blue = Mathf.Lerp (blue, .5f, Time.deltaTime);
			green = Mathf.Lerp (green, .5f, Time.deltaTime);
			Color c = new Color (red, blue, green, 255);
			theSprite.color = c;
		}
		else if(invincible >= 300){
			Color c = new Color(1, 1, 1, 1);
			theSprite.color = c;
		}
	}

	//player collision
	void OnCollisionEnter2D(Collision2D collider){
		if (invincible >= 300) {
			if (collider.gameObject.tag == "EnemyMissile") {
				gameController.playerHealth -= 20;
				invincible = 0;
                source.PlayOneShot(explosionSound);
            }

			if (collider.gameObject.tag == "SpinningEnemy") {
				gameController.playerHealth -= 10;
				invincible = 0;
                source.PlayOneShot(boomSound);
            }
			if (collider.gameObject.tag == "Enemy") {
				gameController.playerHealth -= 10;
				invincible = 0;
                source.PlayOneShot(boomSound);
            }
			if (collider.gameObject.tag == "ShootingEnemy") {
				gameController.playerHealth -= 20;
				invincible = 0;
                source.PlayOneShot(boomSound);
            }

			if (collider.gameObject.tag == "Target") {
				gameController.playerHealth -= 10;
				invincible = 0;
                source.PlayOneShot(boomSound);
            }

		}
	}


}

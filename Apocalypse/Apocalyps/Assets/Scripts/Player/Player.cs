/// <sumarry>
/// Brad Tully 4 October 2017
/// This is the class that allows the player to control the character
/// </sumarry>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//General player controls and stats
	public GameController gameController;
	public float velocity;
	[HideInInspector]
	public float horizontalSpeed = 0;
	[HideInInspector]
	public float verticalSpeed = 0;
	public GameObject player;
	public int playerHealth, playerHunger, playerThirst;

	//Control what direction the player is facing
	public float left, right, up, down;
	public GameObject childSprite;
	public bool isRunning = false;
	public Animator animator;

	//For placing the trap
	public float xPositionTrap, yPositionTrap;
	public Vector3 trapPosition;
	Quaternion trapRotation;
	public GameObject trapPrefab;
	protected GameObject createdTrap;

	//Invincibility related stuff
	private int invincible;
	private float red;
	private float blue;
	private float green;
	public SpriteRenderer theSprite;
	public SpriteRenderer theColorSprite;


	// Use this for initialization
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		left = transform.localScale.x * -1;
		right = transform.localScale.x;
		up = transform.localScale.y * -1;
		down = transform.localScale.y;
		//animator = GetComponent<Animator>();
		theSprite = childSprite.GetComponent<SpriteRenderer>();

	}

	// Update is called once per frame
	void Update()
	{
		moving();
		invincible++;
	}

	//This controls the movement of the player, moves it in a pokemon style ie 4 direction options one at a time
	public void moving()
	{

		//Determine movement direction/speed
		horizontalSpeed = Input.GetAxis("Horizontal");
		verticalSpeed = Input.GetAxis("Vertical");

		//Translates the player and changes the direction it is facing
		if (horizontalSpeed > .01f || horizontalSpeed < -.01f && verticalSpeed == 0f)
		{

			isRunning = true;
			transform.Translate(transform.right * (horizontalSpeed * velocity * Time.deltaTime), Space.World);
			// Right movement
			if (horizontalSpeed > .01f)
			{
				//transform.localScale = new Vector2(right, down);
				//transform.rotation = Quaternion.Euler(0f, 0f, 90f);
				childSprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

			}
			// Left movement
			else if (horizontalSpeed < -.01f)
			{
				//transform.localScale = new Vector2(left, down);
				childSprite.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
			}
		}
		else if (verticalSpeed > .01f || verticalSpeed < -.01f && horizontalSpeed == 0f)
		{
			isRunning = true;
			transform.Translate(transform.up * (verticalSpeed * velocity * Time.deltaTime), Space.World);
			// Upward movement
			if (verticalSpeed > .01f)
			{
				childSprite.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
			}
			// Downward movement
			else if (verticalSpeed < -.01f)
			{
				childSprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
		else
		{
			isRunning = false;
			//animator.SetBool("isRunning", isRunning);
		}

		//invincibility

		if (invincible < 200)
		{
			if (red >= .98f)
			{
				red = 0;
				blue = 0;
			}
			red = Mathf.Lerp(red, .5f, Time.deltaTime);
			blue = Mathf.Lerp(blue, .5f, Time.deltaTime);
			green = Mathf.Lerp(green, .5f, Time.deltaTime);
			Color c = new Color(red, blue, green, 255);
			theSprite.color = c;
		}
		else if (invincible >= 300)
		{
			Color c = new Color(1, 1, 1, 1);
			theSprite.color = c;
		}
		animator.SetBool("IsRunning", isRunning);
	}

	//Place the trap somewhere on the map and decrement from game controller
	void placeTrap()
	{
		if (Input.GetButtonDown("Fire1") == true && gameController.trapCount > 0)
		{
			trapRotation.z = 0;
			trapRotation.y = 0;
			trapRotation.x = 0;
			trapRotation.w = 0;
			trapPosition = Input.mousePosition;
			trapPosition = Camera.main.ScreenToWorldPoint(trapPosition);
			trapPosition.z = 0;
			Instantiate(trapPrefab, trapPosition, trapRotation);
			gameController.trapCount--;
		}
	}

	//Deer attack does damage and starts invinciblity frames
    //Pickup objects as well
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Deer")
		{
            if (invincible > 200)
            {
                gameController.playerHealth -= 15;
                invincible = 0;
            }
		}
        if (collision.gameObject.tag == "Twig")
        {
            gameController.twigCount++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Water")
        {
            gameController.waterCount++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Meat")
        {
            gameController.foodCount++;
            Destroy(collision.gameObject);
        }
	}

	//Fire does 5 damage when player steps in it
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Fire")
		{
			gameController.playerHealth -= 5;
		}
	}
}

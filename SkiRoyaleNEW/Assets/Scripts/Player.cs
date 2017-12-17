///<summary>
///Made by Brad Tully
///11 October 2017
///The player class this controls the player in the skiing section
///Xbox Controller Controls: left joystick move left/right, left bumper hit left, right bumper hit right, "b" brake
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//fV is the speed of the player, sV is for left/right movement, hS is for keyboard input, vI is how
	//much the velocity increases in the coroutine, vD is how much velocity increases whe moving left/right
	public float forwardVelocity, sidewaysVelocity, horizontalSpeed, velocityIncrease, velocityDecrease;
	//sV used for player movement after breaking, direction turns the player
	public float startVelocity, direction;
	/// <summary>
	/// The skiing bool.
	/// </summary>
	protected bool skiing;
	/// <summary>
	/// The sprite since the sprite is a child attached to the player
	/// </summary>
	public GameObject theSprite;
	/// <summary>
	/// The finished bool
	/// </summary>
	public bool finished;
	/// <summary>
	/// The minutes and seconds
	/// </summary>
	public int minutes, seconds;
	/// <summary>
	/// the text for the clock
	/// </summary>
	public Text clock;
	/// <summary>
	/// the place the player is in/ finishes in.
	/// </summary>
	public int place;
	/// <summary>
	/// The name of the player.
	/// </summary>
	public string theName;
	/// <summary>
	/// The cap for speed.
	/// </summary>
	public float cap;
	/// <summary>
	/// The direction max, direction increment and sidevel.
	/// </summary>
	public float directionMax, directionIncrement, sideVelInc;
	/// <summary>
	/// The input hold delay.
	/// </summary>
	public float inputHoldDelay;
    // Used to point player forward with the stick
    public float forwardInput;
    // Values for hard turns
    public float hardDirInc, hardSideVelInc, hardDirection, hardFVelDec;
    public float hardDirMax;

	public Animator playerAnimator;

	public List<Sprite> theSprites;

    // Used for the countdown in the beginning of the race
    [HideInInspector]
    public bool begin;

	protected WaitForSeconds inputHoldWait;

	public GameController gameController;

	public bool hit;


	//Initialize the start velocity, set skiing to true and begin coroutine
	void Start ()
	{
		startVelocity = forwardVelocity;
		skiing = true;
		StartCoroutine ("SpeedIncrease", skiing);
		//StartCoroutine ("timer", finished);
		finished = false;

        //Hard turn values set
        hardDirection = 0;
        hardDirInc = 2.5f * directionIncrement;
        hardSideVelInc = 3 * sideVelInc;
        hardFVelDec = 8 * velocityDecrease;
        hardDirMax = 70;

		inputHoldWait = new WaitForSeconds (inputHoldDelay);

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

        begin = false;

		hit = false;
	}
	
	//Update the speed
	void Update ()
	{
        if (begin == true)
        {
            SpeedIncrease(skiing);
            timer(finished);
            releaseBrake();
            hardTurnUp();
			TurnAnimation ();
        }
	}

	//Update movement here to stop lag
	private void FixedUpdate ()
	{
        if (begin == true)
        {
            if (finished == false)
            {
                Moving();
            }
            else if (finished == true)
            {
                StopCoroutine("timer");
                clock.text = "";
            }

            RubberBand();

			if(((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x > 1) || ((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x < -1)))){

				this.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
        }
	}

	//This controls the movement of the player
	protected virtual void Moving ()
	{
		if (gameObject.tag == "Player1") {
			horizontalSpeed = Input.GetAxis ("P1Horizontal");
            forwardInput = Input.GetAxis("P1Forward");
		} else if (gameObject.tag == "Player2") {
			horizontalSpeed = Input.GetAxis ("P2Horizontal");
            forwardInput = Input.GetAxis("P2Forward");
		}
		// The player only inputs a sideways value
		//Move the player left/right and up
		transform.Translate (transform.right * (sidewaysVelocity * Time.deltaTime), Space.World);
		transform.Translate (transform.up * (forwardVelocity * Time.deltaTime), Space.World);
		//Check left/right inputs
		if (horizontalSpeed > .01f || horizontalSpeed < -.01f) {
			// Right movement
			if (horizontalSpeed > .2f) {
				//turns at most to directionMax degrees
				if (direction > -directionMax) {
					//Change its direction and sidewaysVelocity, slow down forward velocity
					direction -= directionIncrement;
					sidewaysVelocity += sideVelInc;
					//Make sure the velocity can't be negative
					if (forwardVelocity >= 2) {
						forwardVelocity -= velocityDecrease;
					}
				}
				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
            // Left movement
            else if (horizontalSpeed < -.2f) {
				//turns at most direction degrees
				if (direction < directionMax) {
					//Change its direction and sidewaysVelocity, slow down forward velocity
					direction += directionIncrement;
					sidewaysVelocity -= sideVelInc;
					//Make sure the velocity can't be negative
					if (forwardVelocity >= 2) {
						forwardVelocity -= velocityDecrease;
					}
				}
				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
		}


        //Use a hard turn with the left and right triggers
        if (gameObject.tag == "Player1")
        {
            if (Input.GetButton("P1LBumper"))
            {
                if (direction < hardDirMax)
                {
                    //Change its direction and sidewaysVelocity, slow down forward velocity
                    direction += hardDirInc;
                    sidewaysVelocity -= hardSideVelInc;
                    if (forwardVelocity >= 2)
                    {
                        forwardVelocity -= hardFVelDec;
                    }
                }
            }
            else if (Input.GetButton("P1RBumper"))
            {
                if (direction > -hardDirMax)
                {
                    direction -= hardDirInc;
                    sidewaysVelocity += hardSideVelInc;
                    if (forwardVelocity >= 2)
                    {
                        forwardVelocity -= hardFVelDec;
                    }
                }
            }
            theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetButton("P2LBumper"))
            {
                if (direction < hardDirMax)
                {
                    //Change its direction and sidewaysVelocity, slow down forward velocity
                    direction += hardDirInc;
                    sidewaysVelocity -= hardSideVelInc;
                }
            }
            else if (Input.GetButton("P2RBumper"))
            {
                if (direction > -hardDirMax)
                {
                    direction -= hardDirInc;
                    sidewaysVelocity += hardSideVelInc;
                }
            }
            theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);

        }


        //player one forward
        if (gameObject.tag == "Player1") {
			if (Input.GetButton ("P1Forward") || forwardInput > .2f) {
				sidewaysVelocity = 0;
				direction = 0;
				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
		}
		//player 2 forward
		else if (gameObject.tag == "Player2") {
			if (Input.GetButton ("P2Forward") || forwardInput > .2f) {
				sidewaysVelocity = 0;
				direction = 0;
				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
		}

		// Braking system for players
		//player one
		//slowly turn the sprite to which ever side it is facing while braking
		if (gameObject.tag == "Player1") {
			if (Input.GetButton ("P1Brake")) { // c
				if (direction > 0 && direction < 90) {
					direction += 2f;
					theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
				} else if (direction < 0 && direction > -90 || direction == 0) {
					direction -= 2f;
					theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
				}
				if (forwardVelocity > 0) {
					forwardVelocity -= 0.5f;
				} else {
					forwardVelocity = 0;
				}
				sidewaysVelocity = 0;
			}
		}
        //player 2
        //slowly turn the sprite to which ever side it is facing while braking
        else if (gameObject.tag == "Player2") { //n
			if (Input.GetButton ("P2Brake")) {
				if (direction > 0 && direction < 90) {
					direction += 2f;
					theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
				} else if (direction < 0 && direction > -90 || direction == 0) {
					direction -= 2f;
					theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
				}
				if (forwardVelocity > 0) {
					forwardVelocity -= .5f;
				} else {
					forwardVelocity = 0;
				}
				sidewaysVelocity = 0;
			}
		}
	}

    //When the players release the hard turn button it resets direction and sideways velocity to 0
    public void hardTurnUp()
    {
        if (gameObject.tag == "Player1")
        {
            if (Input.GetButtonUp("P1LBumper") || Input.GetButtonUp("P1RBumper"))
            {
                direction = 0;
                sidewaysVelocity = 0;
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            }
        }
        if (gameObject.tag == "Player2")
        {
            if (Input.GetButtonUp("P2LBumper") || Input.GetButtonUp("P2RBumper"))
            {
                direction = 0;
                sidewaysVelocity = 0;
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            }
        }
    }

    //Stop the player from braking and face forward this has to be a seperate function called in update
    //because it was so nested in the moving function it wouldn't always get called when the button was released
    public void releaseBrake ()
	{
		//reset forwardVelocity, direction, sidewaysVelocity and turn the sprite back
        if (gameObject.tag == "Player1")
        {
            if (Input.GetButtonUp("P1Brake"))
            {
                forwardVelocity = startVelocity;
                direction = 0;
                sidewaysVelocity = 0;
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            }
        }
		if (gameObject.tag == "Player2") {
            //reset forwardVelocity, direction, sidewaysVelocity and turn the sprite back
            if (Input.GetButtonUp("P2Brake"))
            {
                forwardVelocity = startVelocity;
                direction = 0;
                sidewaysVelocity = 0;
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
            }
        }
        
	}

    /// <summary>
    /// Turn animation.
    /// </summary>
    public void TurnAnimation (){
		if (direction > 50) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [3];
		} else if (direction > 30) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [2];
		} else if (direction >= 16) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [1];
		}
		else if((direction >= 10.1) && (direction <= 15.9)){
			playerAnimator.enabled = false;

		}
		else if (direction < -50) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [6];
		} else if (direction < -30) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [5];
		} else if (direction <= -16) {
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [4];
		} else if((direction <= -10.1) && (direction >= -15.9f)){
			playerAnimator.enabled = false;
		}
		//change when animation comes
		else if(direction > -10 && direction < 10){
			playerAnimator.enabled = true;
			this.GetComponentInChildren<SpriteRenderer> ().sprite = theSprites [0];
		}
	}

	//This enumerator increases the speed of the player over time as long as they don't get hit
	protected IEnumerator SpeedIncrease (bool tf)
	{
		while (tf == true) {
			if (forwardVelocity >= cap) {
				forwardVelocity = cap;
				yield return null;
			}
			forwardVelocity += velocityIncrease;
			yield return new WaitForSeconds (1);
		}
	}

	//Keep track of the player's time before finishing the race
	protected virtual IEnumerator timer (bool tf)
	{
		while (tf == false) {
			seconds++;
			if (seconds == 60) {
				minutes++;
				seconds = 0;
			}
			clock.text = "Timer: " + minutes + ":" + seconds;
			yield return new WaitForSeconds (1);
		}
	}

	public void StartWaitForHit(){
		hit = true;
		StartCoroutine (WaitForHit ());
	}

	/// <summary>
	/// Waits for hit.
	/// this is a wait for couroutine, except instead of making a new wait for every time, it is being cached
	/// waits the inputHoldWait time that you can set in the inspector
	/// </summary>
	/// <returns>The for hit.</returns>
	private IEnumerator WaitForHit ()
	{
		// As soon as the wait starts, input should no longer be accepted.
		hit = true;
		yield return inputHoldWait;
		this.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		hit = false;
		//hitObject = null;
	}

	public virtual void RubberBand(){
		if(Mathf.Abs(Vector2.Distance(new Vector2(0, gameController.playerPlaceY), new Vector2(0, this.transform.position.y))) > 20){
			cap = 34;
		}

		if (Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.playerPlaceY), new Vector2 (0, this.transform.position.y))) < 7) {
			cap = 25;
		}
	}
}

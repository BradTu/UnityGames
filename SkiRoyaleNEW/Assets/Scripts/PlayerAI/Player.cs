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

	public bool deadFinish;
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
    [HideInInspector]
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
    [HideInInspector]
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
    //Used as a forwrad velocity increase to incetivize drifting/ hard turns
    public float forwardVelBoost, boostIncrement;
    string LorR;

	public Animator playerAnimator;

	public List<Sprite> theSprites;

    public bool gotHit;
    public int currentPlace;
    public bool finishedRace;

    // Used for the countdown in the beginning of the race
    [HideInInspector]
    public bool begin;

	protected WaitForSeconds inputHoldWait;

	public GameController gameController;

	public bool hit;

    //Used for camera shaking
    public GameObject parentCamera;
    public CameraController cameraController;

    //Used for when the player finishes the race
    public Vector3 finishSpot;
	/// <summary>
	/// The life.
	/// </summary>
	public float health;

    public bool isDead;

	public bool isLeftTurning;

	public bool isRightTurning;

	public float hitHit;

	public GameObject timeTrialObject;

	//Initialize the start velocity, set skiing to true and begin coroutine
	void Start ()
	{
		startVelocity = forwardVelocity;
		skiing = true;
		StartCoroutine ("SpeedIncrease", skiing);
		//StartCoroutine ("timer", finished);
		finished = false;
        forwardVelBoost = 0;
		health = 100f;

		inputHoldWait = new WaitForSeconds (inputHoldDelay);

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

		timeTrialObject = GameObject.Find ("TimeTrial");

        begin = false;

		hit = false;

        //Load in camera script for camera shaking
        if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
        {
            cameraController = parentCamera.GetComponent<CameraController>();
        }
    }
	
	//Update the speed
	void Update ()
	{
        if (begin == true)
        {
            SpeedIncrease(skiing);
            timer(finished);
            releaseBrake();
			HardTurnUp ();
			TurnAnimation ();
        }
        MoveToFinishPoint(finished);
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
			if (timeTrialObject == null) {
				RubberBand ();
			}

			if(((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x > 1) || ((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x < -1)))){

				this.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
        }
	}

	//This controls the movement of the player
	protected virtual void Moving ()
	{
		if (gameObject.tag == "Player1") {
			horizontalSpeed = Input.GetAxisRaw ("P1Horizontal");
			forwardInput = Input.GetAxisRaw ("P1Forward");
			HardTurn ("P1LBumper", "P1RBumper");
			Forward ("P1Forward");
			Brake ("P1Brake");
		} else if (gameObject.tag == "Player2") {
			horizontalSpeed = Input.GetAxis ("P2Horizontal");
			forwardInput = Input.GetAxis ("P2Forward");
			HardTurn ("P2LBumper", "P2RBumper");
			Forward ("P2Forward");
			Brake ("P2Brake");
		} else if (gameObject.tag == "Player3") {
			horizontalSpeed = Input.GetAxis ("P3Horizontal");
			forwardInput = Input.GetAxis ("P3Forward");
			HardTurn ("P3LBumper", "P3RBumper");
			Forward ("P3Forward");
			Brake ("P3Brake");
		} else if (gameObject.tag == "Player4") {
			horizontalSpeed = Input.GetAxis ("P4Horizontal");
			forwardInput = Input.GetAxis ("P4Forward");
			HardTurn ("P4LBumper", "P4RBumper");
			Forward ("P4Forward");
			Brake ("P4Brake");
		}
		// The player only inputs a sideways value
		//Move the player left/right and up
		transform.Translate (transform.right * (sidewaysVelocity * Time.deltaTime), Space.World);
		transform.Translate (transform.up * (forwardVelocity * Time.deltaTime), Space.World);
        //Check left/right inputs
            if (horizontalSpeed > .01f || horizontalSpeed < -.01f)
            {
                // Right movement
                if (horizontalSpeed > .2f)
                {
                    //turns at most to directionMax degrees
                    if (direction > -directionMax)
                    {
                        //Change its direction and sidewaysVelocity, slow down forward velocity
                        direction -= directionIncrement;
                        sidewaysVelocity += sideVelInc;
                        //Make sure the velocity can't be negative
                        if (forwardVelocity >= 2)
                        {
                            forwardVelocity -= velocityDecrease;
                        }
                    }
                    theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
                }
                // Left movement
                else if (horizontalSpeed < -.2f)
                {
                    //turns at most direction degrees
                    if (direction < directionMax)
                    {
                        //Change its direction and sidewaysVelocity, slow down forward velocity
                        direction += directionIncrement;
                        sidewaysVelocity -= sideVelInc;
                        //Make sure the velocity can't be negative
                        if (forwardVelocity >= 2)
                        {
                            forwardVelocity -= velocityDecrease;
                        }
                    }
                    theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
                }
            }
	}

    //The hard turn will change the direction of the player for harder turns, it'll initially slow them down
    //but will give them a speed boost based off of how long they hold the hard turn
	public void HardTurn(string leftBump, string rightBump){
        //Left turn
        if (Input.GetButton(leftBump))
		{
			isLeftTurning = true;
            horizontalSpeed = 0f;
            //Increment direction until the max direction is increased, increase sideways velocity
            if (direction < hardDirMax)
			{
				
				direction += hardDirInc;
				sidewaysVelocity -= hardSideVelInc;
				if (forwardVelocity >= 2)
				{
					forwardVelocity -= hardFVelDec;
				}
			}
            //Keep incrementing sideways velocity, increase the boost they get at the end
            else if (direction >= hardDirMax)
            {
                if (sidewaysVelocity >= -40)
                {
                    sidewaysVelocity -= hardSideVelInc;
                }
                if (forwardVelocity >= 2)
                {
                    forwardVelocity -= hardFVelDec;
                }
                if (forwardVelBoost <= 30)
                {
                    forwardVelBoost += boostIncrement;
                }
            }
		}
        //Right turn
		else if (Input.GetButton(rightBump))
		{
			isRightTurning = true;
            horizontalSpeed = 0f;
            //Increment direction until the max direction is increased, increase sideways velocity
            if (direction > -hardDirMax)
			{
				direction -= hardDirInc;
				sidewaysVelocity += hardSideVelInc;
				if (forwardVelocity >= 2)
				{
					forwardVelocity -= hardFVelDec;
				}
			}
            //Keep incrementing sideways velocity, increase the boost they get at the end
            else if (direction <= hardDirMax)
            {
                if (sidewaysVelocity <= 40)
                {
                    sidewaysVelocity += hardSideVelInc;
                }
                if (forwardVelocity >= 2)
                {
                    forwardVelocity -= hardFVelDec;
                }
                if (forwardVelBoost <= 30)
                {
                    forwardVelBoost += boostIncrement;
                }
            }
        }
		theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
	}

    //When the players release the hard turn button it resets the direction to the max/min turning direction
    //it also makes the sideways velocity set to the max/min sideways velocity from normal turns
    //give the player a forward speed boost to incentivize using hard turns
	public void HardTurnUp()
    {
		if (gameObject.tag == "Player1")
		{
			if (Input.GetButtonUp("P1LBumper") || Input.GetButtonUp("P1RBumper"))
			{
				//Left
				if (direction > 1)
				{
					direction = directionMax;
					sidewaysVelocity = -(directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isLeftTurning = false;
				}
				//Right
				else if (direction < -1)
				{
					direction = -directionMax;
					sidewaysVelocity = (directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isRightTurning = false;
				}
			}
		}
		if (gameObject.tag == "Player2")
		{
			if (Input.GetButtonUp("P2LBumper") || Input.GetButtonUp("P2RBumper"))
			{
				//Left
				if (direction > 1)
				{
					direction = directionMax;
					sidewaysVelocity = -(directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isLeftTurning = false;
				}
				//Right
				else if (direction < -1)
				{
					direction = -directionMax;
					sidewaysVelocity = (directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isRightTurning = false;
				}
			}
		}
		if (gameObject.tag == "Player3")
		{
			if (Input.GetButtonUp("P3LBumper") || Input.GetButtonUp("P3RBumper"))
			{
				//Left
				if (direction > 1)
				{
					direction = directionMax;
					sidewaysVelocity = -(directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isLeftTurning = false;
				}
				//Right
				else if (direction < -1)
				{
					direction = -directionMax;
					sidewaysVelocity = (directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isRightTurning = false;
				}
			}
		}
		if (gameObject.tag == "Player4")
		{
			if (Input.GetButtonUp("P4LBumper") || Input.GetButtonUp("P4RBumper"))
			{
				//Left
				if (direction > 1)
				{
					direction = directionMax;
					sidewaysVelocity = -(directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isLeftTurning = false;
				}
				//Right
				else if (direction < -1)
				{
					direction = -directionMax;
					sidewaysVelocity = (directionMax / directionIncrement) * sideVelInc;
					theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
					forwardVelocity += forwardVelBoost;
					forwardVelBoost = 0;
					isRightTurning = false;
				}
			}
		}
    }

    void Forward(string forward){
		if (Input.GetButton (forward) || forwardInput > .2f) {
			sidewaysVelocity = 0;
			direction = 0;					
			theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
		}
	}

	void Brake(string brake){
		if (Input.GetButton (brake)) { // c
//			if (direction > 0 && direction < 90) {
//				direction += 2f;
//				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
//			} else if (direction < 0 && direction > -90 || direction == 0) {
//				direction -= 2f;
//				theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
//			}
			direction = 0;
			theSprite.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			theSprite.GetComponent<Animator>().SetBool("Brake", true);
			if (forwardVelocity > 0) {
				forwardVelocity -= 0.6f;
			} else {
				forwardVelocity = 0;
			}
			sidewaysVelocity = 0;
		}
        if (Input.GetButtonUp(brake))
        {
            direction = 0;
			theSprite.GetComponent<Animator>().SetBool("Brake", false);
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
				theSprite.GetComponent<Animator>().SetBool("Brake", false);
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
				theSprite.GetComponent<Animator>().SetBool("Brake", false);
            }
        }
		if (gameObject.tag == "Player3") {
			//reset forwardVelocity, direction, sidewaysVelocity and turn the sprite back
			if (Input.GetButtonUp("P3Brake"))
			{
				forwardVelocity = startVelocity;
				direction = 0;
				sidewaysVelocity = 0;
				theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
				theSprite.GetComponent<Animator>().SetBool("Brake", false);
			}
		}
		if (gameObject.tag == "Player4") {
			//reset forwardVelocity, direction, sidewaysVelocity and turn the sprite back
			if (Input.GetButtonUp("P4Brake"))
			{
				forwardVelocity = startVelocity;
				direction = 0;
				sidewaysVelocity = 0;
				theSprite.transform.rotation = Quaternion.Euler(0f, 0f, direction);
				theSprite.GetComponent<Animator>().SetBool("Brake", false);
			}
		}
        
	}

    /// <summary>
    /// Turn animation.
    /// </summary>
    public virtual void TurnAnimation (){
		if (direction > 50) {
			playerAnimator.SetBool ("Left3", true);
			playerAnimator.SetBool ("Left2", false);
		} else if (direction > 30) {
			playerAnimator.SetBool ("Left3", false);
			playerAnimator.SetBool ("Left2", true);
			playerAnimator.SetBool ("Left1", false);
		} else if (direction >= 16) {
			playerAnimator.SetBool ("Left2", false);
			playerAnimator.SetBool ("Left1", true);
			playerAnimator.SetBool ("Straight", false);
		}
		//right
		else if (direction < -50) {
			playerAnimator.SetBool ("Right3", true);
			playerAnimator.SetBool ("Right2", false);
		} else if (direction < -30) {
			playerAnimator.SetBool ("Right3", false);
			playerAnimator.SetBool ("Right2", true);
			playerAnimator.SetBool ("Right1", false);
		} else if (direction <= -16) {
			playerAnimator.SetBool ("Right2", false);
			playerAnimator.SetBool ("Right1", true);
			playerAnimator.SetBool ("Straight", false);
		} 
		else if(direction > -15 && direction < 15){
			playerAnimator.SetBool ("Straight", true);
			playerAnimator.SetBool ("Left3", false);
			playerAnimator.SetBool ("Left2", false);
			playerAnimator.SetBool ("Left1", false);
			playerAnimator.SetBool ("Right3", false);
			playerAnimator.SetBool ("Right2", false);
			playerAnimator.SetBool ("Right1", false);
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
	/// <summary>
	/// Starts the wait for hit.
	/// </summary>
	public void StartWaitForHit(){
        if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
        {
            cameraController.startShake(.8f, .3f);
        }
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
		health -= hitHit;
        //hitObject = null;
	}

	public virtual void RubberBand(){
		if (isDead == false && this.transform.GetChild(0).GetComponent<UseItem>().speedUp == false && this.transform.GetChild(0).GetComponent<UseItem>().playerSlowed == false) {
			if (Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.activePlayers [0].gameObject.transform.position.y), new Vector2 (0, gameController.activePlayers [currentPlace - 1].gameObject.transform.position.y))) > 270) {
				cap = 31;
			}
			else if(Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.activePlayers [0].gameObject.transform.position.y), new Vector2 (0, gameController.activePlayers [currentPlace - 1].gameObject.transform.position.y))) < 100){
				cap = 27;
			}
		}
	}

    //This function will move the player to their finish spot at the end of the race
    //It also turns off the snow particles and turns off the animator
    //Then turns the sprite to make it look like it is moving in that direction
    //when it gets to the point it turs 90 degrees
    public void MoveToFinishPoint(bool finish)
    {
        if (finish == true && theSprite.transform.position != finishSpot)
        {
            theSprite.GetComponentInChildren<ParticleSystem>().Stop();
            theSprite.GetComponentInChildren<Animator>().speed = 0;
            theSprite.transform.position = Vector3.MoveTowards(theSprite.transform.position, finishSpot, .2f);
            if (finishSpot.x > theSprite.transform.position.x)
            {
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, -50f);
                LorR = "right";
            }
            else if (finishSpot.x < theSprite.transform.position.x)
            {
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, 50f);
                LorR = "left";
            }
        }
        else if (finish == true && Vector3.Distance(theSprite.transform.position, finishSpot) < 1f)
        {
            if (LorR == "right")
            {
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
            else if (LorR == "left")
            {
                theSprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }

		if (deadFinish == true) {
			Stop ();
		}
    }

    public void StopSpeedBoost()
    {
        this.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void PlayPlayerRocketHit()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

	public void Stop(){
		forwardVelocity = 0;
		sidewaysVelocity = 0;
		velocityIncrease = 0;
		direction = 0;
		directionIncrement = 0;
		sideVelInc = 0;
		hardSideVelInc = 0;
		hardFVelDec = 0;
		StopCoroutine ("timer");
	}

}

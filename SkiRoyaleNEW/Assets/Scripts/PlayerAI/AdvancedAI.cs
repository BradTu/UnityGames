using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//change to way points.
//have it avoid obsticles.
//do hitting and items from here to call upon other scripts
//make a new item script for ai
//maybe make a new hitting script
public class AdvancedAI : Player
{
	public bool turnLeft;

	public bool turnRight;

	public bool avoiding;

	public bool avoidLeft;

	public bool avoidRight;

	public bool startedTime;

	public float direction2;

	[Header("WayPoint Objects")]
	public List<GameObject> wayPoints;
	public bool isPatrolling;
	public Transform target;
	public int val = 0;
	public Vector3 difference;
	public float rotZ;

	[Header ("Advance AI Objects")]
	public GameObject poleLeft;

	public GameObject poleRight;

	public GameObject center;

	public GameObject left;

	public GameObject right;

	public float hardDirectionMax;

	[Header ("Sensors")]
	public Vector2 sensorStartingPos;
	public float sensorLength;
	public Vector2 frontSensorPos;
	public Vector2 frontRightSensorPos;
	public Vector2 frontLeftSensorPos;
	public float leftDiagonalSensor;
	public float rightDiagonalSensor;
	public float rightSensor;
	public float leftSensor;

	[Header ("Distances")]
	public float frontDistance;
	public float frontRightDistance;
	public float frontLeftDistance;
	public float leftDiagonalDistance;
	public float rightDiagonalDistance;
	public float rightDistance;
	public float leftDistance;

	[Header ("Game Objects")]
	public GameObject frontGameObject;
	public GameObject frontRightGameObject;
	public GameObject frontLeftGameObject;
	public GameObject leftDiagonalGameObject;
	public GameObject rightDiagonalGameObject;
	public GameObject rightGameObject;
	public GameObject leftGameObject;

	// Use this for initialization
	void Start ()
	{
        health = 100f;
		hit = false;
		startVelocity = forwardVelocity;
		skiing = true;
		StartCoroutine ("SpeedIncrease", skiing);

		inputHoldWait = new WaitForSeconds (inputHoldDelay);

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		minutes = 0;
		seconds = 0;
		begin = false;
		finished = false;

		isPatrolling = true;

		frontSensorPos = new Vector2 (center.transform.position.x, center.transform.position.y);
		frontRightSensorPos = new Vector2 (right.transform.position.x, right.transform.position.y);
		frontLeftSensorPos = new Vector2 (left.transform.position.x, left.transform.position.y);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDead == true && forwardVelocity != 0) {
			Stop ();
		}
	}

	private void FixedUpdate ()
	{
		Sensors ();
		if (begin == true){
			
		Moving ();
		Rotating (direction2);
		//Turning ();
		//TurningChecks ();
		TurnAnimation ();
		HittingChecks ();
		ItemChecks ();

		//RubberBand();

			if (finished == true)
			{
				StopCoroutine("timer");
			}

		if (((hit == false) && (this.GetComponent<Rigidbody2D> ().velocity.x > 1) || ((hit == false) && (this.GetComponent<Rigidbody2D> ().velocity.x < -1)))) {
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		}


		}


	}

	void Sensors ()
	{
		sensorStartingPos = transform.position; 
		RaycastHit2D hit;
		//center
		frontSensorPos = new Vector2 (center.transform.position.x, center.transform.position.y);
		sensorStartingPos = frontSensorPos;
		//sensorStartingPos.x += transform.forward.x * frontSensorPos;
		hit = Physics2D.Raycast (sensorStartingPos, transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			frontDistance = Vector2.Distance (hit.point, this.transform.position);
			frontGameObject = hit.collider.gameObject;
		} else {
			frontDistance = 10;
			frontGameObject = null;
		}
		//front right
		frontRightSensorPos = new Vector2 (right.transform.position.x, right.transform.position.y);
		sensorStartingPos = frontRightSensorPos;
		hit = Physics2D.Raycast (sensorStartingPos, transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			frontRightDistance = Vector2.Distance (hit.point, this.transform.position);
			frontRightGameObject = hit.collider.gameObject;
		} else {
			frontRightDistance = 10;
			frontRightGameObject = null;
		}
		//right angle
		hit = Physics2D.Raycast (sensorStartingPos, Quaternion.AngleAxis (rightDiagonalSensor, -transform.forward) * transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			rightDiagonalDistance = Vector2.Distance (hit.point, this.transform.position);
			rightDiagonalGameObject = hit.collider.gameObject;
		} else {
			rightDiagonalDistance = 10;
			rightDiagonalGameObject = null;
		}

		//right
		hit = Physics2D.Raycast (sensorStartingPos, Quaternion.AngleAxis (rightSensor, -transform.forward) * transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			rightDistance = Vector2.Distance (hit.point, this.transform.position);
			rightGameObject = hit.collider.gameObject;
		} else {
			rightDistance = 10;
			rightGameObject = null;
		}

		//front left
		frontLeftSensorPos = new Vector2 (left.transform.position.x, left.transform.position.y);
		sensorStartingPos = frontLeftSensorPos;
		hit = Physics2D.Raycast (sensorStartingPos, transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			frontLeftDistance = Vector2.Distance (hit.point, this.transform.position);
			frontLeftGameObject = hit.collider.gameObject;
		} else {
			frontLeftDistance = 10;
			frontLeftGameObject = null;
		}

		//left angle
		hit = Physics2D.Raycast (sensorStartingPos, Quaternion.AngleAxis (leftDiagonalSensor, transform.forward) * transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			leftDiagonalDistance = Vector2.Distance (hit.point, this.transform.position);
			leftDiagonalGameObject = hit.collider.gameObject;
		} else {
			leftDiagonalDistance = 10;
			leftDiagonalGameObject = null;
		}

		//left
		hit = Physics2D.Raycast (sensorStartingPos, Quaternion.AngleAxis (leftSensor, transform.forward) * transform.up, sensorLength);
		if (hit.collider != null) {
			//Debug.DrawLine (sensorStartingPos, hit.point, Color.red);
			leftDistance = Vector2.Distance (hit.point, this.transform.position);
			leftGameObject = hit.collider.gameObject;
		} else {
			leftDistance = 10;
			leftGameObject = null;
		}
	}

	protected override void Moving ()
	{
		if (isPatrolling == true)
		{
			target.position = Vector3.MoveTowards(target.position, wayPoints[val].transform.position, (forwardVelocity * Time.deltaTime));

			if (target.position == wayPoints[val].transform.position)
			{
				val++;
				difference = wayPoints[val].transform.position - transform.position;
				difference.Normalize();
				rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
				direction2 = rotZ;
				//if(rotZ < this.transform
				//transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
				if (val == (wayPoints.Count))
				{
					Debug.Log("DONE");
				}
			}
		}
		else if (isPatrolling == false)
		{
			MoveToFinishPoint(finished);
		}
	}
	//think of a new turn thing
	void Rotating (float dir)
	{
		if ((direction < dir + 2) && (direction > dir - 2)) {
			turnLeft = false;
			turnRight = false;
		}
		else if (direction > dir) {
			turnLeft = false;
			turnRight = true;
		} else if (direction < dir) {
			turnRight = false;
			turnLeft = true;
		} 

		if (turnLeft || turnRight) {
			// Right movement
			if (turnRight) {
				//turns at most to directionMax degrees
				//if (direction > -directionMax) {
					//Change its direction and sidewaysVelocity, slow down forward velocity
					direction -= directionIncrement;
					//Make sure the velocity can't be negative
					if (forwardVelocity >= 2) {
						forwardVelocity -= velocityDecrease;
					}
				//}
				this.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
			// Left movement
			else if (turnLeft) {
				//turns at most direction degrees
				//if (direction < directionMax) {
					//Change its direction and sidewaysVelocity, slow down forward velocity
					direction += directionIncrement;
					//Make sure the velocity can't be negative
					if (forwardVelocity >= 2) {
						forwardVelocity -= velocityDecrease;
					}
				//}
				this.transform.rotation = Quaternion.Euler (0f, 0f, direction);
			}
		}
	}
	//maybe keep this
//	void ObstacleTurning ()
//	{
//		if (avoidLeft || avoidRight) {
//			// Right movement
//			if (avoidRight) {
//				//turns at most to directionMax degrees
//				if (direction > -directionMax) {
//					//Change its direction and sidewaysVelocity, slow down forward velocity
//					direction -= directionIncrement;
//					sidewaysVelocity += sideVelInc;
//					//Make sure the velocity can't be negative
//					if (forwardVelocity >= 2) {
//						forwardVelocity -= velocityDecrease;
//					}
//				}
//				this.transform.rotation = Quaternion.Euler (0f, 0f, direction);
//			}
//			// Left movement
//			else if (avoidLeft) {
//				//turns at most direction degrees
//				if (direction < directionMax) {
//					//Change its direction and sidewaysVelocity, slow down forward velocity
//					direction += directionIncrement;
//					sidewaysVelocity -= sideVelInc;
//					//Make sure the velocity can't be negative
//					if (forwardVelocity >= 2) {
//						forwardVelocity -= velocityDecrease;
//					}
//				}
//				this.transform.rotation = Quaternion.Euler (0f, 0f, direction);
//			}
//		}
//	}

	void ItemChecks(){
		if (this.GetComponent<AdvancedAIItemSystem> ().currentItem != null) {
			if (this.GetComponent<AdvancedAIItemSystem> ().currentItem.name == "Snowball") {
				if (!startedTime) {
					StartCoroutine (Timer());
				}
				if ((frontGameObject != null && frontGameObject.tag.Contains ("Sprite")) || 
					(frontGameObject != null && frontGameObject.tag.Contains ("AI"))) {
					//activate snow
						if (this.GetComponent<AdvancedAIItemSystem> ().currentItem.name == "Snowball") {
							this.GetComponent<AdvancedAIItemSystem> ().ActivateSnowBall ();
							StopCoroutine (Timer());
							startedTime = false;
						}
				}

				else if ((frontRightGameObject != null && frontRightGameObject.tag.Contains ("Sprite")) || 
					(frontRightGameObject != null && frontRightGameObject.tag.Contains ("AI"))) {
					//activate snow
						if (this.GetComponent<AdvancedAIItemSystem> ().currentItem.name == "Snowball") {
							this.GetComponent<AdvancedAIItemSystem> ().ActivateSnowBall ();
							StopCoroutine (Timer());
							startedTime = false;
						}
				}

				else if ((frontLeftGameObject != null && frontLeftGameObject.tag.Contains ("Sprite")) ||
					(frontLeftGameObject != null && frontLeftGameObject.tag.Contains ("AI"))) {
					if (this.GetComponent<AdvancedAIItemSystem> ().currentItem.name == "Snowball") {
							this.GetComponent<AdvancedAIItemSystem> ().ActivateSnowBall ();
							StopCoroutine (Timer());
							startedTime = false;
						}
					}
				}
			}
	}

	private IEnumerator Timer(){
		startedTime = true;

		yield return new WaitForSeconds (Random.Range(10f, 20f));

		if (this.GetComponent<AdvancedAIItemSystem> ().currentItem != null) {
			if (this.GetComponent<AdvancedAIItemSystem> ().currentItem.name == "Snowball") {
				this.GetComponent<AdvancedAIItemSystem> ().ActivateSnowBall ();
			}
		}
	}

	void HittingChecks(){
		if (leftGameObject != null) {
			//if(leftGameObject.name == "
			if(leftGameObject.tag.Contains("Sprite") || leftGameObject.tag.Contains("AI")){
				if (leftDistance < 2.5) {
					if (leftGameObject.name == "LeftAIPole") {
						return;
					}
					this.transform.GetChild (0).GetComponent<AdvancedAIPoleAttack> ().StartLeftAttack ();
				}
			}
		}
	
		if (rightGameObject != null) {
			if(rightGameObject.tag.Contains("Sprite") || rightGameObject.tag.Contains("AI")){
				if (rightDistance < 2.5) {
					if (rightGameObject.name == "RightAIPole") {
						return;
					}
					this.transform.GetChild (1).GetComponent<AdvancedAIPoleAttack> ().StartRightAttack ();
				}
			}
		}
	}

	//put in the player, there should be no need now to turn on and off animators and all that shit
	public override void TurnAnimation () {
		//left
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
		
	private IEnumerator WaitForHit ()
	{
		// As soon as the wait starts, input should no longer be accepted.
		hit = true;
		yield return inputHoldWait;
		this.GetComponentInChildren<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		health -= hitHit;
		hit = false;
	}

	//put back
	protected override IEnumerator timer (bool tf)
	{
		while (tf == false) {
			seconds++;
			if (seconds == 60) {
				minutes++;
				seconds = 0;
			}
			yield return new WaitForSeconds (1);
		}
	}

	//put back and work on
	public override void RubberBand(){
		if (isDead == false && this.GetComponent<AdvancedAIItemSystem>().speedUp == false && this.GetComponent<AdvancedAIItemSystem>().aiSlowed == false) {
			if (Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.activePlayers [0].gameObject.transform.position.y), new Vector2 (0, gameController.activePlayers [currentPlace - 1].gameObject.transform.position.y))) > 270) {
				cap = 31;
			}
			else if(Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.activePlayers [0].gameObject.transform.position.y), new Vector2 (0, gameController.activePlayers [currentPlace - 1].gameObject.transform.position.y))) < 100){
				cap = 27;
			}
		}
	}

	void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "SoftBoarder") {
			if (forwardVelocity >= 8) {
				forwardVelocity -= 0.1f;
			}
		}
	}


}

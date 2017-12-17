using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic A.
/// for now the same as the player class pretty much.
/// Joe Yates
/// </summary>
public class BasicAI : Player
{
	public GameObject poleLeft;

	public GameObject poleRight;

	public List<GameObject> wayPoints;
	public bool isPatrolling;
	public Transform target;
	public int val = 0;
	public Vector3 difference;
	public float rotZ;

	//Initialize the start velocity, set skiing to true and begin coroutine
	void Start ()
	{
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
	}

	//Update the speed
	void Update ()
	{
		
	}

	//Update movement here to stop lag
	private void FixedUpdate ()
	{
        if (begin == true)
        {
            Moving();
            RubberBand();
            /*if (finished == false)
            {
                Moving();
                RubberBand();
            }*/
            if (finished == true)
            {
                StopCoroutine("timer");
            }

			if(((hit == false) && (this.GetComponent<Rigidbody2D>().velocity.x > 1) || ((hit == false) && (this.GetComponent<Rigidbody2D>().velocity.x < -1)))){
				//Debug.Log ("hello:");
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
        }
	}

	//This controls the movement of the AI
	protected override void Moving ()
	{
        //Move the player left/right and up
        //transform.Translate(transform.right * (sidewaysVelocity * Time.deltaTime), Space.World);
        if (isPatrolling == true)
        {
            target.position = Vector3.MoveTowards(target.position, wayPoints[val].transform.position, (forwardVelocity * Time.deltaTime));

            if (target.position == wayPoints[val].transform.position)
            {
                val++;
                difference = wayPoints[val].transform.position - transform.position;
                difference.Normalize();
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                if (val == (wayPoints.Count))
                {
                    Debug.Log("DONE");
                }
            }
        }
        else if (isPatrolling == false)
        {
            target.position = Vector3.MoveTowards(transform.position, 
                                                  new Vector3(transform.position.x, 1000f, 0), 
                                                  1000f);
        }
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

	}

	//Keep track of the player's time before finishing the race
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

	/// <summary>
	/// if anyone comes in the radius, they start swinging.
	/// </summary>
	/// <param name="theCollider">The collider.</param>
	void OnTriggerEnter2D(Collider2D theCollider){
		if (theCollider.gameObject.name.Contains("AI") && theCollider.gameObject.transform.position.x <= this.transform.position.x) {
			poleLeft.GetComponent<PoleAttackAI> ().StartLeftAttack ();

		}
		if (theCollider.gameObject.name.Contains("AI") && theCollider.gameObject.transform.position.x > this.transform.position.x) {
			poleRight.GetComponent<PoleAttackAI> ().StartRightAttack ();

		}

		if (theCollider.gameObject.tag == "P1Sprite" && theCollider.gameObject.transform.position.x <= this.transform.position.x) {
			if (poleLeft != null) {
				poleLeft.GetComponent<PoleAttackAI> ().StartLeftAttack ();
			}
		}

		if (theCollider.gameObject.tag == "P1Sprite" && theCollider.gameObject.transform.position.x > this.transform.position.x) {
			if (poleRight != null) {
				poleRight.GetComponent<PoleAttackAI> ().StartRightAttack ();
			}
		}

		if (theCollider.gameObject.tag == "P2Sprite" && theCollider.gameObject.transform.position.x <= this.transform.position.x) {
			if (poleLeft != null) {
				poleLeft.GetComponent<PoleAttackAI> ().StartLeftAttack ();
			}
		}

		if (theCollider.gameObject.tag == "P2Sprite" && theCollider.gameObject.transform.position.x > this.transform.position.x) {
			if (poleRight != null) {
				poleRight.GetComponent<PoleAttackAI> ().StartRightAttack ();
			}
		}
	}

	public override void RubberBand(){
		if(Mathf.Abs(Vector2.Distance(new Vector2(0, gameController.playerPlaceY), new Vector2(0, this.transform.position.y))) > 30){
				cap = 28;
			}

		if (Mathf.Abs (Vector2.Distance (new Vector2 (0, gameController.playerPlaceY), new Vector2 (0, this.transform.position.y))) < 7) {
				cap = 23;
			}
	}

	/// <summary>
	/// this is for the soft snow to slow them down
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "SoftBoarder") {
			if (forwardVelocity >= 8) {
				forwardVelocity -= 0.1f;
			}
		}
	}
}

/**
 * This script takes care of player/item collision
 * Jason Komoda 10/12/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	/// <summary>
	/// The game controller.
	/// </summary>
	public GameController gameController;
	/// <summary>
	/// The p1 item box.
	/// </summary>
	public GameObject p1ItemBox;
	/// <summary>
	/// The p2 item box.
	/// </summary>
	public GameObject p2ItemBox;
	/// <summary>
	/// The snowball prefab.
	/// </summary>
	public GameObject snowballPrefab;
	/// <summary>
	/// The snowball.
	/// </summary>
	public GameObject snowball;
	/// <summary>
	/// The following snowball prefab.
	/// </summary>
	public GameObject followingSnowballPrefab;
	/// <summary>
	/// The following snowball.
	/// </summary>
	public GameObject followingSnowball;
	/// <summary>
	/// The p1 can pick up item.
	/// </summary>
	public bool p1CanPickUpItem;
	/// <summary>
	/// The p2 can pick up item.
	/// </summary>
	public bool p2CanPickUpItem;

    public int p1NumberOfPickups;
    public int p2NumberOfPickups;

    public GameObject speedUpPrefab;
    public GameObject speedUp;
    public GameObject slowDownPrefab;
    public GameObject slowDown;
    public GameObject sludgePrefab;
    public GameObject sludge;
    private SoundManager soundManager;

	// Use this for initialization
	void Start ()
	{
		//Get game controller reference
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}

        //Get sound manager reference
        GameObject soundManagerObject = GameObject.FindWithTag("SoundManager");
        if (soundManagerObject != null)
        {
            soundManager = soundManagerObject.GetComponent<SoundManager>();
        }

		//lets players pick up items
		p1CanPickUpItem = true;
		p2CanPickUpItem = true;

        
	}
	
	// Update is called once per frame
	void Update ()
	{
        
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		//player1 collides with random item box
		//player one
		if (gameObject.tag == "P1Sprite") {
			if (other.gameObject.tag == "RandomItemBox") {
				if (p1CanPickUpItem) {
                    soundManager.GetComponent<SoundManager>().p1ItemPickup.Play();
                    p1NumberOfPickups++;
					other.gameObject.GetComponent<RandomItemBox> ().ChooseItem ();
					other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;

					//snowball
					if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "Snowball") {

						snowball = Instantiate (snowballPrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
						p1CanPickUpItem = false;
						snowball.transform.position = p1ItemBox.transform.position;             //move item to item box
						snowball.transform.parent = p1ItemBox.transform;                        //make item a child of item box so it stays in box while moving
						snowball.layer = LayerMask.NameToLayer ("P1Canvas");                     //make item layer p1canvas layer so the other p2camera can't see it
						snowball.GetComponent<CircleCollider2D> ().enabled = false;              //disable collider on item while it's in the item box
						gameController.p1HasItem = true;                                        //keeps track of when the player's item status
						gameController.p1CurrentItem = snowballPrefab;                          //make current item the item the snowball
					}

                    //following snowball
                    else if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "FollowingSnowball") {

						followingSnowball = Instantiate (followingSnowballPrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
						p1CanPickUpItem = false;
						followingSnowball.transform.position = p1ItemBox.transform.position;             
						followingSnowball.transform.parent = p1ItemBox.transform;                        
						followingSnowball.layer = LayerMask.NameToLayer ("P1Canvas");                     
						followingSnowball.GetComponent<CircleCollider2D> ().enabled = false;              
						gameController.p1HasItem = true;                                                
						gameController.p1CurrentItem = followingSnowballPrefab;                                 
					}

                    //speed up
                    else if(other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp"){
                        speedUp = Instantiate(speedUpPrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p1CanPickUpItem = false;
                        speedUp.transform.position = p1ItemBox.transform.position;
                        speedUp.transform.parent = p1ItemBox.transform;
                        speedUp.layer = LayerMask.NameToLayer("P1Canvas");
                        gameController.p1HasItem = true;
                        gameController.p1CurrentItem = speedUpPrefab;
                    }

                    //slow down
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
                    {
                        slowDown = Instantiate(slowDownPrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p1CanPickUpItem = false;
                        slowDown.transform.position = p1ItemBox.transform.position;
                        slowDown.transform.parent = p1ItemBox.transform;
                        slowDown.layer = LayerMask.NameToLayer("P1Canvas");
                        gameController.p1HasItem = true;
                        gameController.p1CurrentItem = slowDownPrefab;
                    }

                    //sludge
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
                    {
                        sludge = Instantiate(sludgePrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p1CanPickUpItem = false;
                        sludge.transform.position = p1ItemBox.transform.position;
                        sludge.transform.parent = p1ItemBox.transform;
                        sludge.layer = LayerMask.NameToLayer("P1Canvas");
                        gameController.p1HasItem = true;
                        gameController.p1CurrentItem = sludgePrefab;
                    }
                    Destroy(other.gameObject);
				} else {
					Physics2D.IgnoreCollision (gameObject.GetComponent<BoxCollider2D> (), other.gameObject.GetComponent<BoxCollider2D> (), true);
				}
			}
            if(other.gameObject.tag == "Sludge"){
                gameObject.GetComponentInParent<Player>().forwardVelocity = 5;
            }
		}

		//player2 collides with random item box
		if (gameObject.tag == "P2Sprite") {
			if (other.gameObject.tag == "RandomItemBox") {
                soundManager.GetComponent<SoundManager>().p2ItemPickup.Play();
				if (p2CanPickUpItem) {
                    p2NumberOfPickups++;
					other.gameObject.GetComponent<RandomItemBox> ().ChooseItem ();
					other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;

					//snowball
					if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "Snowball") {
						snowball = Instantiate (snowballPrefab, p2ItemBox.transform.position, Quaternion.identity) as GameObject;
						p2CanPickUpItem = false;
						snowball.transform.position = p2ItemBox.transform.position;             //move item to item box
						snowball.transform.parent = p2ItemBox.transform;                        //make item a child of item box so it stays in box while moving
						snowball.layer = LayerMask.NameToLayer ("P2Canvas");                     //make item layer p1canvas layer so the other p2camera can't see it
						snowball.GetComponent<CircleCollider2D> ().enabled = false;              //disable collider on item while it's in the item box
						gameController.p2HasItem = true;                                        //keeps track of when the player's item status
						gameController.p2CurrentItem = snowballPrefab;                          //make current item the item the snowball
					}

                    //following snowball
                    else if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "FollowingSnowball") {

						followingSnowball = Instantiate (followingSnowballPrefab, p2ItemBox.transform.position, Quaternion.identity) as GameObject;
						p2CanPickUpItem = false;
						followingSnowball.transform.position = p2ItemBox.transform.position;
						followingSnowball.transform.parent = p2ItemBox.transform;
						followingSnowball.layer = LayerMask.NameToLayer ("P2Canvas");
						followingSnowball.GetComponent<CircleCollider2D> ().enabled = false;
						gameController.p2HasItem = true;
						gameController.p2CurrentItem = followingSnowballPrefab;
					}

                    //speed up
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp")
                    {
                        speedUp = Instantiate(speedUpPrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p2CanPickUpItem = false;
                        speedUp.transform.position = p2ItemBox.transform.position;
                        speedUp.transform.parent = p2ItemBox.transform;
                        speedUp.layer = LayerMask.NameToLayer("P2Canvas");
                        gameController.p2HasItem = true;
                        gameController.p2CurrentItem = speedUpPrefab;
                    }

                    //slow down
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
                    {
                        slowDown = Instantiate(slowDownPrefab, p2ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p2CanPickUpItem = false;
                        slowDown.transform.position = p2ItemBox.transform.position;
                        slowDown.transform.parent = p2ItemBox.transform;
                        slowDown.layer = LayerMask.NameToLayer("P2Canvas");
                        gameController.p2HasItem = true;
                        gameController.p2CurrentItem = slowDownPrefab;
                    }

                    //sludge
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
                    {
                        sludge = Instantiate(sludgePrefab, p1ItemBox.transform.position, Quaternion.identity) as GameObject;
                        p2CanPickUpItem = false;
                        sludge.transform.position = p2ItemBox.transform.position;
                        sludge.transform.parent = p2ItemBox.transform;
                        sludge.layer = LayerMask.NameToLayer("P2Canvas");
                        gameController.p2HasItem = true;
                        gameController.p2CurrentItem = sludgePrefab;
                    }
                    Destroy(other.gameObject);

				} else {
					Physics2D.IgnoreCollision (gameObject.GetComponent<BoxCollider2D> (), other.gameObject.GetComponent<BoxCollider2D> (), true);
				}
			}
            if (other.gameObject.tag == "Sludge")
            {
                gameObject.GetComponentInParent<Player>().forwardVelocity = 1;
            }
		}
	}



	//Reduce player's forward velocity if they collide with another player or AI
	private void OnCollisionEnter2D (Collision2D other)
	{

	}

	//Destroy player1's item in item box after use
	public void P1UsedItem ()
	{
		p1CanPickUpItem = true;
		gameController.p1HasItem = false;
		gameObject.GetComponent<UseItem> ().p1AlreadyGotDirection = false;
		if (snowball != null) {
			Destroy (snowball);
		} else if (followingSnowball != null) {
			Destroy (followingSnowball);
		}
        else if(speedUp != null){
            Destroy(speedUp);
        }
        else if(slowDown != null){
            Destroy(slowDown);
        }
        else if(sludge != null){
            Destroy(sludge);
        }
	}

	//Destroy player2's item in item box after use
	public void P2UsedItem ()
	{
		p2CanPickUpItem = true;
		gameController.p2HasItem = false;
		gameObject.GetComponent<UseItem> ().p2AlreadyGotDirection = false;
		if (snowball != null) {
			Destroy (snowball);
		} else if (followingSnowball != null) {
			Destroy (followingSnowball);
		}
        else if (speedUp != null)
        {
            Destroy(speedUp);
        }
        else if(slowDown != null){
            Destroy(slowDown);
        }
        else if (sludge != null)
        {
            Destroy(sludge);
        }
	}

    /// <summary>
    /// this is for the soft snow to slow them down
    /// </summary>
    /// <param name="collider">Collider.</param>
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "SoftBoarder")
        {
            if (GetComponentInParent<Player>().forwardVelocity >= 8)
            {
                GetComponentInParent<Player>().forwardVelocity -= 0.1f;
            }
        }

        if (collider.gameObject.tag == "Sludge")
        {
            gameObject.GetComponentInParent<Player>().forwardVelocity = 5;
        }
    }
}

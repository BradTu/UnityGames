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
	public GameController gameController;
    public GameObject snowballPrefab;
	public GameObject followingSnowballPrefab;
    public GameObject speedUpPrefab;
    public GameObject slowDownPrefab;
    public GameObject sludgePrefab;
    public GameObject blueRocketPrefab;
	public GameObject itemPickedUp;
    private SoundManager soundManager;
    public bool canPickUpItem;
    public GameObject playerItemBox;
    public string layerName;
    public int numPickups;
    public GameObject bombPrefab;
    public GameObject invincPrefab;
    public GameObject gunPrefab;
    public GameObject molotovPrefab;
	public GameObject player;
	public int count;
	public bool oneItemTest;
	public float bombHit;
    public float fireHit;

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

        canPickUpItem = true;
        numPickups = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		//player collides with item box
        if (gameObject.tag.Contains("Sprite")) {
			if (other.gameObject.tag == "RandomItemBox") {
                if (canPickUpItem) {
                    numPickups++;

                    //play sound on item pickup
                    if(gameObject.tag.Contains("P1")){
						soundManager.p1.clip = soundManager.itemPickUp;
						soundManager.p1.Play ();
                    }
					if (gameObject.tag.Contains ("P2")) {
						soundManager.p2.clip = soundManager.itemPickUp;
						soundManager.p2.Play ();
					} else if (gameObject.tag.Contains ("P3")) {
						soundManager.p3.clip = soundManager.itemPickUp;
						soundManager.p3.Play ();
					} else if (gameObject.tag.Contains ("P4")) {
						soundManager.p4.clip = soundManager.itemPickUp;
						soundManager.p4.Play ();
					}
                    other.gameObject.GetComponent<RandomItemBox> ().ChooseItem(gameObject.GetComponentInParent<Player>().currentPlace);
					other.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					other.gameObject.GetComponent<BoxCollider2D> ().enabled = false;;

                    //Call ItemPickUp function depending on which item the player picked up
                    //snowball
					if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "Snowball") {
                        ItemPickUp(snowballPrefab, "Snowball");
					}

                    //red rocket
                    else if (other.gameObject.GetComponent<RandomItemBox> ().chosenItem == "FollowingSnowball") {
                        ItemPickUp(followingSnowballPrefab, "FollowingSnowball");                               
					}

                    //blue rocket
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "BlueRocket")
                    {
                        ItemPickUp(blueRocketPrefab, "BlueRocket");    
                    }

                    //speed up
                    else if(other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp"){
                        ItemPickUp(speedUpPrefab, "SpeedUp");
                    }

                    //slow down
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
                    {
                        ItemPickUp(slowDownPrefab, "SlowDown");
                    }

                    //sludge
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
                    {
                        ItemPickUp(sludgePrefab, "Sludge");
                    }

                    //bomb
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Bomb")
                    {
                        ItemPickUp(bombPrefab, "Bomb");
                    }
                    //invincibility
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Invinc")
                    {
                        ItemPickUp(invincPrefab, "Invinc");
                    }
                    //gun
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Gun")
                    {
                        ItemPickUp(gunPrefab, "Gun");
                    }
                    //molotov
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Molotov")
                    {
                        ItemPickUp(molotovPrefab, "Molotov");
                    }
                    canPickUpItem = false;
				} else {
					Physics2D.IgnoreCollision (gameObject.GetComponent<BoxCollider2D> (), other.gameObject.GetComponent<BoxCollider2D> (), true);
				}
			}

            //colliding with sludge slows player
            if(other.gameObject.tag == "Sludge"){
                if(!gameObject.GetComponent<UseItem>().isInvinc){
                    gameObject.GetComponentInParent<Player>().forwardVelocity = 1;
                }
            }

            //colliding with bomb blows bomb up and stalls the player/damages them
            if (other.gameObject.tag == "Bomb" && other.gameObject.GetComponent<ItemMovement>().playerThatUsedItem != gameObject)
            {
                if (!gameObject.GetComponent<UseItem>().isInvinc){
                    other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    other.gameObject.GetComponent<AudioSource>().Play();
                    StartCoroutine(HitByBombStall(other.gameObject));
                }
                else{
                    other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    other.gameObject.GetComponent<AudioSource>().Play();
                    StartCoroutine(ExplosionStall(other.gameObject));
                }
            }

            //colliding with fire lights the player on fire and damages them
            if(other.gameObject.tag == "Fire" && !gameObject.GetComponent<UseItem>().isInvinc){
                gameObject.GetComponentInParent<Player>().health -= fireHit;
                StartCoroutine(FireBurn());
            }
		}
	}

    //manages what happens when players pick up an item, (UI and item variables are set here)
	void ItemPickUp(GameObject pre, string itemName){
		if (gameObject.tag.Contains ("P1")) {
			layerName = "P1Canvas";
            playerItemBox = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(8).gameObject;
		} else if (gameObject.tag.Contains ("P2")) {
			layerName = "P2Canvas";
            playerItemBox = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(8).gameObject;
		} else if (gameObject.tag.Contains ("P3")) {
			layerName = "P3Canvas";
            playerItemBox = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(8).gameObject;
		} else if (gameObject.tag.Contains ("P4")) {
			layerName = "P4Canvas";
            playerItemBox = gameObject.transform.parent.transform.GetChild(2).transform.GetChild(8).gameObject;
		}
        itemPickedUp = Instantiate(pre, playerItemBox.transform.position, Quaternion.identity) as GameObject;
        itemPickedUp.transform.position = playerItemBox.transform.position;       //move item to item box
        itemPickedUp.transform.parent = playerItemBox.transform;                        //make item a child of item box so it stays in box while moving
        itemPickedUp.layer = LayerMask.NameToLayer(layerName);                     //make item layer p1canvas layer so the other p2camera can't see it
		if(itemPickedUp.GetComponent<Collider2D>() != null){
			itemPickedUp.GetComponent<Collider2D>().enabled = false;              //disable collider on item while it's in the item box
		}
		gameObject.GetComponent<UseItem>().hasItem = true;
		gameObject.GetComponent<UseItem>().currentItem = itemName;  //make current item the item
	}

	//Destroy player's item in UI item box after use
	public void PlayerUsedItem ()
	{
        canPickUpItem = true;
        gameObject.GetComponent<UseItem>().hasItem = false;
		gameObject.GetComponent<UseItem> ().alreadyGotDirection = false;
		if (itemPickedUp != null) {
			Destroy (itemPickedUp);
		}
        gameObject.GetComponent<UseItem>().currentItem = "";
	}


    void OnTriggerStay2D(Collider2D collider)
    {
        //players hit banks, they get slowed
        if (collider.gameObject.tag == "SoftBoarder")
        {
            if (GetComponentInParent<Player>().forwardVelocity >= 12 && !gameObject.GetComponent<UseItem>().isInvinc)
            {
                GetComponentInParent<Player>().forwardVelocity -= 0.2f;
            }
        }

        //players hit sludge, they get slowed
        if (collider.gameObject.tag == "Sludge" && !gameObject.GetComponent<UseItem>().isInvinc)
        {
            gameObject.GetComponentInParent<Player>().forwardVelocity = 9;
        }
    }

    //duration players get stalled when they hit a bomb
    private IEnumerator HitByBombStall(GameObject theBomb)
    {
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", true);
        gameObject.GetComponent<HitByItem>().hitByProjectile = true;
        this.GetComponentInParent<Player>().health -= bombHit;
        yield return new WaitForSeconds(.8f);
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", false);
        Destroy(theBomb.gameObject);
    }

    //bomb explosion animation stall duration
    private IEnumerator ExplosionStall(GameObject theBomb){
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", true);
        yield return new WaitForSeconds(0.8f);
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", false);
        Destroy(theBomb.gameObject);
    }

    //players light on fire for a certain duration when they pass through molotov
    private IEnumerator FireBurn(){
        gameObject.transform.GetChild(6).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.GetChild(6).GetComponent<ParticleSystem>().Stop();
    }
}

/**
 *  script manages the item usage of the AI players
 * Jason Komoda 11/8/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AiItemSystem : MonoBehaviour {
    public GameController gameController;
    public bool canPickUpItem;
    public bool hasItem;
    public bool canUseItem;
    public GameObject currentItem;
    public GameObject snowball;
    public GameObject followingSnowball;
    public GameObject speedUp;
    public GameObject slowDown;
    public GameObject sludge;
    public GameObject blueRocket;
    public GameObject bomb;
    public GameObject invinc;
    public GameObject gun;
    public GameObject snowballPrefab;
    public GameObject followingSnowballPrefab;
    public GameObject speedUpPrefab;
    public GameObject slowDownPrefab;
    public GameObject sludgePrefab;
    public GameObject blueRocketPrefab;
    public GameObject bombPrefab;
    public GameObject invincPrefab;
    public GameObject gunPrefab;
    public float velocity;
    public float velocityIncrease;
    public float velocityCap;
    private bool alreadyFoundClosest;
    public GameObject targetObject;
    public bool itemUsedByAi;
    public bool alreadyGotDirection;
    public Vector3 aiItemMoveDirection;
    public GameObject playerToSlow;
    private bool waitCoroutineActive;
    public GameObject playerInFirst;
    public bool aiSlowed;
    public bool isInvinc;
    public bool hasGun;
    public bool isTargetedByMissle;
    private bool finishedSlowWait;


	// Use  for initialization
	void Start () {

        playerInFirst = gameObject;
        finishedSlowWait = true;
        canUseItem = true;
        //Get game controller reference
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        //lets AI pick up items
        canPickUpItem = true;
	}
	
	// Update is called once per frame
	void Update () {
        activateItem();
	}

    private void activateItem()
    {
        if (hasItem)
        {
            //gather targets if item is redRocket or slowDown
            if (currentItem.tag == "FollowingSnowball")
            {
                playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                if (playerInFirst.tag != gameObject.tag)
                {
                    targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().GetTargets()).gameObject;
                }
            }
            else if (currentItem.tag == "SlowDown")
            {
                if (playerInFirst.tag == gameObject.tag)
                {
                    targetObject = gameObject;
                    if (finishedSlowWait)
                    {
                        playerToSlow = targetObject;
                    }
                }
                else
                {
                    targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().GetTargets()).gameObject;
                    if (finishedSlowWait)
                    {
                        playerToSlow = targetObject;
                    }
                }
                finishedSlowWait = false;
            }

            //gets player in first place if item is blueRocket;
            else if (currentItem.tag == "BlueRocket")
            {
                playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                targetObject = playerInFirst.transform.gameObject;
            }
            alreadyGotDirection = true;
            //activate snowball
            if (currentItem.tag == "Snowball" && canUseItem)
            {
                if (!alreadyGotDirection)
                {
                    aiItemMoveDirection = gameObject.transform.up;
                }
                alreadyGotDirection = true;
                snowball = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                snowball.GetComponent<ItemMovement>().usedByAi = true;
                snowball.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), snowball.GetComponent<CircleCollider2D>(), true);
            }

            //activate following snowball
            else if (currentItem.tag == "FollowingSnowball" && canUseItem)
            {
                playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                if(playerInFirst.tag != gameObject.tag){
                    followingSnowball = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                followingSnowball.GetComponent<ItemMovement>().usedByAi = true;
                followingSnowball.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
                followingSnowball.GetComponent<ItemMovement>().targetObject = targetObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), followingSnowball.GetComponent<CircleCollider2D>(), true); 
                }
            }

            //activate following snowball
            else if (currentItem.tag == "BlueRocket" && canUseItem)
            {
                if(gameObject.tag != playerInFirst.tag){
                    blueRocket = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                blueRocket.GetComponent<ItemMovement>().usedByAi = true;
                blueRocket.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
                blueRocket.GetComponent<ItemMovement>().targetObject = targetObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), blueRocket.GetComponent<CircleCollider2D>(), true);  
                }
            }

            //activate speed up
            else if (currentItem.tag == "SpeedUp" && canUseItem)
            {
                gameObject.GetComponent<BasicAI>().cap = 30;
                gameObject.GetComponent<BasicAI>().forwardVelocity = 30;                   //call function that destroys item in p1 item box
                StartCoroutine(SpeedUpDurationWait());
            }

            //activate slow down
            else if (currentItem.tag == "SlowDown" && canUseItem)
            {
                int decidingNumToSlow = Random.Range(1, 5);
                if (decidingNumToSlow == 1)
                {
                    aiSlowed = true;
                    playerToSlow = gameObject;
                    playerToSlow.GetComponent<BasicAI>().cap = 9;
                    playerToSlow.GetComponent<BasicAI>().forwardVelocity = 9;
                }
                else
                {
                    playerToSlow = targetObject;
                    if (playerToSlow.gameObject.tag.Contains("Sprite"))
                    {
                        playerToSlow.transform.parent.transform.GetChild(2).transform.GetChild(4).gameObject.GetComponent<Text>().text = gameObject.name + " Slowed You!";
                        playerToSlow.GetComponent<UseItem>().playerSlowed = true;
                        playerToSlow.GetComponentInParent<Player>().cap = 9;
                        playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                    }
                    else
                    {
                        playerToSlow.GetComponent<AiItemSystem>().aiSlowed = true;
                        playerToSlow.GetComponent<BasicAI>().cap = 9;
                        playerToSlow.GetComponent<BasicAI>().forwardVelocity = 9;
                    }
                }
                StartCoroutine(SlowDurationWait());
            }

            //activate sludge
            else if (currentItem.tag == "Sludge" && canUseItem)
            {
                sludge = Instantiate(currentItem, gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity);
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), sludge.GetComponent<BoxCollider2D>(), true);
            }

            //activate bomb
            else if (currentItem.tag == "Bomb" && canUseItem)
            {
                bomb = Instantiate(currentItem, gameObject.transform.position - new Vector3(0, 4, 0), Quaternion.identity);
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bomb.GetComponent<CircleCollider2D>(), true);
            }

            //activate bomb
            else if (currentItem.tag == "Invinc" && canUseItem)
            {
                isInvinc = true;
                StartCoroutine(InvincDuration());
            }

            //activate gun
            else if (currentItem.tag == "Gun" && canUseItem)
            {
                hasGun = true;
                canUseItem = false;
                gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(GunDuration());
            }
        }
        hasItem = false;
        canPickUpItem = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag.Contains("AI")){
            if(other.gameObject.tag == "RandomItemBox"){
                if(canPickUpItem){
                    other.gameObject.GetComponent<RandomItemBox>().ChooseItem(gameObject.GetComponent<BasicAI>().currentPlace);
                    other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    //pickup snowball
                    if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Snowball")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = snowballPrefab;
                    }

                    //pickup red rocket
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "FollowingSnowball")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = followingSnowballPrefab;
                    }

                    //pickup blue rocket
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "BlueRocket")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = blueRocketPrefab;
                    }

                    //speed up
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = speedUpPrefab;
                    }

                    //slow down
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = slowDownPrefab;
                    }

                    //sludge
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = sludgePrefab;
                    }

                    //bomb
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Bomb")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = bombPrefab;
                    }

                    //invinc
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Invinc")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = invincPrefab;
                    }

                    //gun
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Gun")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = gunPrefab;
                    }
                }
                //StartCoroutine(WaitTimeToUseItem());
            }
        }
    }

    private IEnumerator WaitTimeToUseItem(){
        int waitTime = 3;
        yield return new WaitForSeconds(waitTime);
        canUseItem = true;
    }

    private IEnumerator SpeedUpDurationWait()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<BasicAI>().cap = 23;
    }

    private IEnumerator SlowDurationWait()
    {
        yield return new WaitForSeconds(2);
        if(playerToSlow.tag.Contains("Sprite")){
            playerToSlow.GetComponent<UseItem>().playerSlowed = false;
            playerToSlow.transform.parent.GetChild(2).transform.GetChild(4).GetComponent<Text>().enabled = false;
            playerToSlow.GetComponentInParent<Player>().cap = 25;
        }
        else{
            playerToSlow.GetComponent<BasicAI>().cap = 23;
            playerToSlow.GetComponent<AiItemSystem>().aiSlowed = false;
        }
        finishedSlowWait = true;
    }

    private IEnumerator InvincDuration()
    {
        yield return new WaitForSeconds(10);
        isInvinc = false;
    }

    private IEnumerator GunDuration()
    {
        yield return new WaitForSeconds(4);
        hasGun = false;
        canUseItem = true;
        gameObject.GetComponent<AudioSource>().Stop();
    }
}

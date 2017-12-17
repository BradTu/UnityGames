/**
 * This script manages the item usage of the AI players
 * Jason Komoda 11/8/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiItemSystem : MonoBehaviour {

    public GameController gameController;
    public bool canPickUpItem;
    public bool hasItem;
    public GameObject currentItem;
    public GameObject snowball;
    public GameObject followingSnowball;
    public GameObject speedUp;
    public GameObject slowDown;
    public GameObject sludge;
    public GameObject snowballPrefab;
    public GameObject followingSnowballPrefab;
    public GameObject speedUpPrefab;
    public GameObject slowDownPrefab;
    public GameObject sludgePrefab;
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


	// Use this for initialization
	void Start () {
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

    private void activateItem(){
        if (this.hasItem)
        {
            if(!waitCoroutineActive){
                waitCoroutineActive = true;
                StartCoroutine(AiItemWaitTime());
            }

            //gather targets if item is rocket or slowDown
            if (this.currentItem.tag == "FollowingSnowball" || this.currentItem.tag == "SlowDown"){
                gameObject.GetComponent<MissleMovement>().GetTargets();
                this.targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().targets).gameObject;
                Debug.Log(this.gameObject.tag + " : " + gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().targets).gameObject.tag);
            }

            //activate snowball
            if(this.currentItem.tag == "Snowball"){
                if(!this.alreadyGotDirection){
                    this.aiItemMoveDirection = this.gameObject.transform.up;
                }
                this.alreadyGotDirection = true;
                snowball = Instantiate(this.currentItem, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                snowball.GetComponent<ItemMovement>().usedByAi = true;
                snowball.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
                Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), this.snowball.GetComponent<CircleCollider2D>(), true);
            }

            //activate following snowball
            else if (this.currentItem.tag == "FollowingSnowball")
            {
                followingSnowball = Instantiate(this.currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                followingSnowball.GetComponent<ItemMovement>().usedByAi = true;
                followingSnowball.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), followingSnowball.GetComponent<CircleCollider2D>(), true);                   //call function that destroys item in p1 item box
            }

            //activate speed up
            else if (this.currentItem.tag == "SpeedUp")
            {
                gameObject.GetComponent<BasicAI>().cap = 30;
                gameObject.GetComponent<BasicAI>().forwardVelocity = 30;                   //call function that destroys item in p1 item box
                StartCoroutine(SpeedUpDurationWait());
            }

            //activate slow down
            else if (this.currentItem.tag == "SlowDown")
            {
                int decidingNumToSlow = Random.Range(1, 5);
                if(decidingNumToSlow == 1)
                {
                    playerToSlow = gameObject;

                }
                else{
                    playerToSlow = targetObject;
                }
                if(playerToSlow.gameObject.tag == "Player1" || playerToSlow.gameObject.tag == "Player2"){
                    playerToSlow.GetComponentInParent<Player>().cap = 9;
                    playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                    if(playerToSlow.gameObject.tag == "Player1"){
                        gameController.player1Slowed = true;
                        gameController.p1SlowedText.text = gameObject.tag + " Slowed You!";
                    }
                    else{
                        gameController.player2Slowed = true;
                        gameController.p1SlowedText.text = gameObject.tag + " Slowed You!";
                    }
                }
                else{
                    playerToSlow.GetComponent<BasicAI>().cap = 9;
                    playerToSlow.GetComponent<BasicAI>().forwardVelocity = 9;  
                }
                StartCoroutine(SlowDurationWait());
            }

            //activate sludge
            else if(this.currentItem.tag == "Sludge"){
                sludge = Instantiate(this.currentItem, this.gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity);
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), sludge.GetComponent<BoxCollider2D>(), true);  
            }
        }
        this.hasItem = false;
        this.canPickUpItem = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag == "AI 1" || gameObject.tag == "AI 2" || gameObject.tag == "AI 3"|| gameObject.tag == "AI 4"|| gameObject.tag == "AI 5"|| gameObject.tag == "AI 6"){
            if(other.gameObject.tag == "RandomItemBox"){
                if(this.canPickUpItem){
                    other.gameObject.GetComponent<RandomItemBox>().ChooseItem();
                    other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    //pickup snowball
                    if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Snowball")
                    {
                        this.hasItem = true;
                        this.canPickUpItem = false;
                        this.currentItem = snowballPrefab;
                    }

                    //pickup rocket
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "FollowingSnowball")
                    {
                        this.hasItem = true;
                        this.canPickUpItem = false;
                        this.currentItem = followingSnowballPrefab;
                    }

                    //speed up
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp")
                    {
                        this.hasItem = true;
                        this.canPickUpItem = false;
                        this.currentItem = speedUpPrefab;
                    }

                    //slow up
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
                    {
                        this.hasItem = true;
                        this.canPickUpItem = false;
                        this.currentItem = slowDownPrefab;
                    }

                    //slow up
                    else if (other.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
                    {
                        this.hasItem = true;
                        this.canPickUpItem = false;
                        this.currentItem = sludgePrefab;
                    }
                }
            }
        }
    }

    private IEnumerator AiItemWaitTime(){
        yield return new WaitForSeconds(Random.Range(4, 8));
        waitCoroutineActive = false;
    }

    private IEnumerator SpeedUpDurationWait()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<BasicAI>().cap = 23;
    }

    private IEnumerator SlowDurationWait()
    {
        yield return new WaitForSeconds(2);
        if(playerToSlow.gameObject.tag == "Player1" || playerToSlow.gameObject.tag == "Player2"){
            playerToSlow.GetComponent<Player>().cap = 25;
            if (playerToSlow.gameObject.tag == "Player1")
            {
                gameController.player1Slowed = false;
            }
            else
            {
                gameController.player2Slowed = false;
            }
        }
        else{
            playerToSlow.GetComponent<BasicAI>().cap = 23;
        }
    }
}

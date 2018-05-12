/**
 * This script implements the usage of items
 * Jason Komoda 10/19/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UseItem : MonoBehaviour
{
	public GameController gameController;
	private GameObject snowball;
	private GameObject followingSnowball;
	public GameObject playerToSlow;
    public GameObject tempPlayerToSlow;
	private GameObject sludge;
	private GameObject blueRocket;
	public GameObject targetObject;
    private GameObject bomb;
    private GameObject molotov;
    private GameObject gun;
    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
	public bool hasItem;
    public bool canUseItem;
	public string currentItem;
	public string pastItem;
	public bool alreadyGotDirection;
	public Vector3 itemMoveDirection;
	public Vector3 itemStartingPos;
	public GameObject snowballPrefab;
	public GameObject followingSnowballPrefab;
	public GameObject speedUpPrefab;
	public GameObject slowDownPrefab;
	public GameObject sludgePrefab;
	public GameObject blueRocketPrefab;
    public GameObject bombPrefab;
    public GameObject gunPrefab;
    public GameObject invincPrefab;
    public GameObject bulletPrefab;
    public GameObject molotovPrefab;
	public GameObject playerInFirst;
	public bool playerSlowed;
    public bool isInFirst;
    public bool isInvinc;
    public bool hasGun;
    public bool isTargetedByMissle;
	public bool speedUp;
    private bool finishedSlowWait;

	public RuntimeAnimatorController rainbowAnim;
	public RuntimeAnimatorController tempAnim;

	private SoundManager soundManager;

	// Use this for initialization
	void Start()
	{
        playerInFirst = gameObject;
        playerToSlow = gameObject;
        finishedSlowWait = true;
        canUseItem = true;
		//Get game controller reference
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		//Get sound manager reference
		GameObject soundManagerObject = GameObject.FindWithTag("SoundManager");
		if (soundManagerObject != null)
		{
			soundManager = soundManagerObject.GetComponent<SoundManager>();
		}
	}


	// Update is called once per frame
	void Update()
	{
		activateItem();
	}

	//when players use their item
	private void activateItem()
	{
		if (gameObject.tag.Contains("Sprite") && hasItem)
		{
            //find closest target in front of player to hit with red rocket
			if (currentItem == "FollowingSnowball")
			{
                playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                if(playerInFirst.tag != gameObject.tag){
                    targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().GetTargets()).gameObject;
                }
			}

            //find closest target in front of player to slow
            else if(currentItem == "SlowDown"){
                playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                if(playerInFirst.tag == gameObject.tag){
                    targetObject = gameObject;
                    if(finishedSlowWait){
                        playerToSlow = targetObject;
                    }
                }
                else{
                    targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().GetTargets()).gameObject;
                    if (finishedSlowWait)
                    {
                        playerToSlow = targetObject;
                    }
                }
                finishedSlowWait = false;
            }

            //find player in first place to hit with blue rocket
			else if(currentItem == "BlueRocket"){
				playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
                if(playerInFirst.tag != gameObject.tag){
                    targetObject = playerInFirst;
                }
			}
		}

		//make sure the the player has item before using it
		if (gameObject.tag.Contains("Sprite") && hasItem)
		{
            alreadyGotDirection = true;

            //player1 item usage
			if (Input.GetButtonDown ("P1UseItem") && gameObject.tag == "P1Sprite") {
                alreadyGotDirection = false;
				ActivateSnowBall ("P1");
				ActivateRedRocket ("P1");
				ActivateBlueRocket ("P1");
				ActivateSlowDown ("P1");
				ActivateSpeedUp ("P1");
				ActivateSludge ("P1");
                ActivateBomb("P1");
                ActivateInvinc("P1");
                ActivateGun("P1");
                ActivateMolotov("P1");

			}  

            //player2 item usage
            else if (Input.GetButtonDown ("P2UseItem") && gameObject.tag == "P2Sprite") {
                alreadyGotDirection = false;

				ActivateSnowBall ("P2");
				ActivateRedRocket ("P2");
				ActivateBlueRocket ("P2");
				ActivateSlowDown ("P2");
				ActivateSpeedUp ("P2");
				ActivateSludge ("P2");
                ActivateBomb("P2");
                ActivateInvinc("P2");
                ActivateGun("P2");
                ActivateMolotov("P2");

			}  

            //player3 item usage
            else if (Input.GetButtonDown ("P3UseItem") && gameObject.tag == "P3Sprite") {
				alreadyGotDirection = false;

				ActivateSnowBall ("P3");
				ActivateRedRocket ("P3");
				ActivateBlueRocket ("P3");
				ActivateSlowDown ("P3");
				ActivateSpeedUp ("P3");
				ActivateSludge ("P3");
                ActivateBomb("P3");
                ActivateInvinc("P3");
                ActivateGun("P3");
                ActivateMolotov("P3");

			}  

            //player4 item usage
            else if (Input.GetButtonDown ("P4UseItem") && gameObject.tag == "P4Sprite") {
                alreadyGotDirection = false;

				ActivateSnowBall ("P4");
				ActivateRedRocket ("P4");
				ActivateBlueRocket ("P4");
				ActivateSlowDown ("P4");
				ActivateSpeedUp ("P4");
				ActivateSludge ("P4");
                ActivateBomb("P4");
                ActivateInvinc("P4");
                ActivateGun("P4");
                ActivateMolotov("P4");
			}
		}
	}

    //use snowball
	void ActivateSnowBall(string player){
        if (currentItem == "Snowball" && canUseItem)
		{
            if (!alreadyGotDirection)
            {
                itemMoveDirection = gameObject.transform.up;
            }
            alreadyGotDirection = true;
			if(player == "P1"){
				soundManager.p1.clip = soundManager.snowball;
				soundManager.p1.Play ();
			}
			else if(player == "P2"){
				soundManager.p2.clip = soundManager.snowball;
				soundManager.p2.Play ();
			}
			else if (player == "P3")
			{
				soundManager.p3.clip = soundManager.snowball;
				soundManager.p3.Play ();
			}
			else if (player == "P4")
			{
				soundManager.p4.clip = soundManager.snowball;
				soundManager.p4.Play ();
			}
			snowball = Instantiate(snowballPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
			snowball.GetComponent<ItemMovement>().usedByAPlayer = true;
			snowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), snowball.GetComponent<CircleCollider2D>(), true);
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
		}

	}

    //use red rocket
	void ActivateRedRocket(string player){
        if (currentItem == "FollowingSnowball" && canUseItem)
		{
            if(gameObject.tag != playerInFirst.tag){
                followingSnowball = Instantiate(followingSnowballPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
                followingSnowball.GetComponent<ItemMovement>().usedByAPlayer = true;
                followingSnowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                followingSnowball.GetComponent<ItemMovement>().targetObject = targetObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), followingSnowball.GetComponent<CircleCollider2D>(), true);
                gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
				pastItem = currentItem;
            }
		}
	}

    //use blue rocket
	void ActivateBlueRocket(string player)
    {
        if (currentItem == "BlueRocket" && canUseItem)
        {
            if (gameObject.tag != playerInFirst.tag)
            {
                blueRocket = Instantiate(blueRocketPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
                blueRocket.GetComponent<ItemMovement>().usedByAPlayer = true;
                blueRocket.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                blueRocket.GetComponent<ItemMovement>().targetObject = targetObject;
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), blueRocket.GetComponent<CircleCollider2D>(), true);
                gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
				pastItem = currentItem;
            }
        }
    }

    //use speed up
	void ActivateSpeedUp(string player){
        if (currentItem == "SpeedUp" && canUseItem)
		{     
			if (player == "P1") {
				soundManager.p1.clip = soundManager.speedUp;
				soundManager.p1.Play();
			}   else if (player == "P2") {
				soundManager.p2.clip = soundManager.speedUp;
				soundManager.p2.Play();
			}   else if (player == "P3") {
				soundManager.p3.clip = soundManager.speedUp;
				soundManager.p3.Play();
			}	else if (player == "P4") {
				soundManager.p4.clip = soundManager.speedUp;
				soundManager.p4.Play();
			}
			gameObject.GetComponentInParent<Player>().cap = 50;
			gameObject.GetComponentInParent<Player>().forwardVelocity = 50;
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			speedUp = true;
			StartCoroutine(SpeedUpDurationWait());
			pastItem = currentItem;
		}
	}

    //use slow down
	void ActivateSlowDown(string player){
        if (currentItem == "SlowDown" && canUseItem)
		{
            //random number to decide whether to slow oneself or other players
			int decidingNumToSlow = Random.Range(1, 10);

            //sound clips to play on slow down activation
			if (player == "P1")
			{
				soundManager.p1.clip = soundManager.slowDown;
				soundManager.p1.Play();
			}
			else if (player == "P2")
			{
				soundManager.p2.clip = soundManager.slowDown;
				soundManager.p2.Play();
			}
			else if (player == "P3")
			{
				soundManager.p3.clip = soundManager.slowDown;
				soundManager.p3.Play();
			}
			else if (player == "P4")
			{
				soundManager.p4.clip = soundManager.slowDown;
				soundManager.p4.Play();
			}

            //slow oneself if number is 1
            if ((decidingNumToSlow == 1 || playerToSlow.tag == gameObject.tag) && !isInvinc)
			{
                playerToSlow.GetComponent<UseItem>().playerSlowed = true;
                gameObject.transform.parent.gameObject.transform.GetChild(2).transform.GetChild(4).GetComponent<Text>().text = "You Slowed Yourself!";
				playerToSlow.GetComponentInParent<Player>().cap = 9;
				playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                StartCoroutine(SlowDurationWait());
			}

            //slow others if number is 2-10
			else
			{
                if(playerToSlow.gameObject.tag.Contains("Sprite") && !playerToSlow.gameObject.GetComponent<UseItem>().isInvinc){
                    playerToSlow.transform.parent.transform.GetChild(2).transform.GetChild(4).gameObject.GetComponent<Text>().text = gameObject.transform.parent.gameObject.name + " Slowed You!";
                    playerToSlow.GetComponent<UseItem>().playerSlowed = true;
                    playerToSlow.GetComponentInParent<Player>().cap = 9;
                    playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                    StartCoroutine(SlowDurationWait());
                }
				else
				{
                    if(!playerToSlow.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc){
                        playerToSlow.GetComponent<AdvancedAIItemSystem>().aiSlowed = true;
                        playerToSlow.GetComponent<AdvancedAI>().cap = 9;
                        playerToSlow.GetComponent<AdvancedAI>().forwardVelocity = 9; 
                        StartCoroutine(SlowDurationWait());
                    }
				}
			}
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
		}
	}

    //use sludge
	void ActivateSludge(string player){
        if (currentItem == "Sludge" && canUseItem)
		{
            //sound clips on sludge activation
			if (player == "P1")
			{
				soundManager.p1.clip = soundManager.sludge;
				soundManager.p1.Play();
			}
			else if (player == "P2")
			{
				soundManager.p2.clip = soundManager.sludge;
				soundManager.p2.Play();
			}
			else if (player == "P3")
			{
				soundManager.p3.clip = soundManager.sludge;
				soundManager.p3.Play();
			}
			else if (player == "P4")
			{
				soundManager.p4.clip = soundManager.sludge;
				soundManager.p4.Play();
			}
			sludge = Instantiate(sludgePrefab, gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
		}
	}

    //use bomb
    void ActivateBomb(string player)
    {
        if (currentItem == "Bomb" && canUseItem)
        {
            bomb = Instantiate(bombPrefab, gameObject.transform.position - new Vector3(0, 3, 0), Quaternion.identity) as GameObject;
            bomb.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bomb.GetComponent<CircleCollider2D>());
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
            StartCoroutine(BombTickWait(bomb));
        }
    }

    //use invincibility
    void ActivateInvinc(string player)
    {
        if (currentItem == "Invinc" && canUseItem)
        {
            isInvinc = true;
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
			canUseItem = false;
            StartCoroutine(InvincDuration());
        }
    }

    //use gun
    void ActivateGun(string player){
        if(currentItem == "Gun" && canUseItem){
            hasGun = true;
            canUseItem = false;
            gun = Instantiate(gunPrefab, gameObject.transform.position + new Vector3(1, 0, 0), Quaternion.identity) as GameObject;
            gun.GetComponent<SpriteRenderer>().enabled = false;
            gun.gameObject.GetComponent<AudioSource>().Play();
			this.GetComponent<Animator> ().SetBool ("FireGun", true);
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
			pastItem = currentItem;
            StartCoroutine(GunDuration());
        }
    }

    //use molotov
    void ActivateMolotov(string player)
    {
        if (currentItem == "Molotov" && canUseItem)
        {
            if (!alreadyGotDirection)
            {
                itemMoveDirection = gameObject.transform.up;
            }
            alreadyGotDirection = true;
            molotov = Instantiate(molotovPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            molotov.GetComponent<ItemMovement>().usedByAPlayer = true;
            molotov.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
            gameObject.GetComponent<PlayerCollision>().PlayerUsedItem();
            pastItem = currentItem;
        }

    }

    //speed up duration after activation
	private IEnumerator SpeedUpDurationWait()
	{
		yield return new WaitForSeconds(1);
		gameObject.GetComponentInParent<Player>().cap = 27;
		speedUp = false;
	}

    //slow down duration after activation
	private IEnumerator SlowDurationWait()
	{
		yield return new WaitForSeconds(2);
		if (playerToSlow.tag.Contains("Sprite"))
		{
            playerToSlow.GetComponent<UseItem>().playerSlowed = false;
            playerToSlow.transform.parent.GetChild(2).transform.GetChild(4).GetComponent<Text>().enabled = false;
			playerToSlow.GetComponentInParent<Player>().cap = 27;
		}
		else
		{
			playerToSlow.GetComponent<AdvancedAI>().cap = 27;
            playerToSlow.GetComponent<AdvancedAIItemSystem>().aiSlowed = false;
		}
        finishedSlowWait = true;
	}

    //bomb duration before it explodes (if no one collides with it)
    private IEnumerator BombTickWait(GameObject bomb){
        yield return new WaitForSeconds(3);
        bomb.GetComponent<Animator>().SetBool("Explode", true);
        yield return new WaitForSeconds(0.8f);
        bomb.GetComponent<Animator>().SetBool("Explode", false);
        Destroy(bomb);
    }

    //invincible duration after activation
    private IEnumerator InvincDuration()
    {
		tempAnim = this.GetComponent<Animator> ().runtimeAnimatorController;
		this.GetComponent<Animator> ().runtimeAnimatorController = rainbowAnim;
        yield return new WaitForSeconds(5);
		this.GetComponent<Animator> ().runtimeAnimatorController = tempAnim;
        isInvinc = false;
		canUseItem = true;
    }

    //gun duration after activation
    private IEnumerator GunDuration(){
        //create 18 sets of 3 bullet shots
        for (int i = 0; i < 18; i++)
        {
            //middle bullet
            bullet1 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.rotation);
            //right bullet
            bullet2 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(1, 1, 0), gameObject.transform.rotation);
            //left bullet
            bullet3 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(-1, 1, 0), gameObject.transform.rotation);

            bullet1.GetComponent<ItemMovement>().usedByAPlayer = true;
            bullet1.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
			bullet1.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet1.GetComponent<ItemMovement>().bulletDuration());
            bullet2.GetComponent<ItemMovement>().usedByAPlayer = true;
            bullet2.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
			bullet2.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet2.GetComponent<ItemMovement>().bulletDuration());
            bullet3.GetComponent<ItemMovement>().usedByAPlayer = true;
            bullet3.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
            bullet3.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet3.GetComponent<ItemMovement>().bulletDuration());
            yield return new WaitForSeconds(0.1f);
        }
        hasGun = false;
        canUseItem = true;
		this.GetComponent<Animator> ().SetBool ("FireGun", false);
        Destroy(gun);
    }
}


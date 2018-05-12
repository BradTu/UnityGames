using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedAIItemSystem : MonoBehaviour {
	public GameController gameController;
	[Header("What the player has")]
	public GameObject currentItem;
	public GameObject pastItem;
	[Header("Item Game Objects")]
	public GameObject snowball;
	public GameObject followingSnowball;
	public GameObject speedUp;
	public GameObject slowDown;
	public GameObject sludge;
	public GameObject blueRocket;
	public GameObject bomb;
	public GameObject invinc;
	public GameObject gun;
    public GameObject molotov;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
	[Header("Item Pre fabs")]
	public GameObject snowballPrefab;
	public GameObject followingSnowballPrefab;
	public GameObject speedUpPrefab;
	public GameObject slowDownPrefab;
	public GameObject sludgePrefab;
	public GameObject blueRocketPrefab;
	public GameObject bombPrefab;
	public GameObject invincPrefab;
	public GameObject gunPrefab;
    public GameObject bulletPrefab;
    public GameObject molotovPrefab;
	[Header("Bools")]
	public bool canPickUpItem;
	public bool hasItem;
	public bool canUseItem;
	private bool alreadyFoundClosest;
	public bool itemUsedByAi;
	public bool alreadyGotDirection;
	private bool waitCoroutineActive;
	public bool aiSlowed;
	public bool aiSpeed;
	public bool isInvinc;
	public bool hasGun;
	public bool isTargetedByMissle;
	private bool finishedSlowWait;
	public bool preparingThrow;
	public bool oneItemTest;
	[Header("Velocity")]
	public float velocity;
	public float velocityIncrease;
	public float velocityCap;
	[Header("Targeting")]
	public GameObject targetObject;
	public Vector3 aiItemMoveDirection;
	public GameObject playerToSlow;
	public GameObject playerInFirst;
	[Header("Animation Controllers")]
	public RuntimeAnimatorController rainbowAnim;
	public RuntimeAnimatorController tempAnim;
	[Header("Other")]
	public int count;

	// Use this for initialization
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
		preparingThrow = false;
	}
	
	// Update is called once per frame
	void Update () {
		ActivateItems ();
	}

	void ActivateItems(){
		//put the finding places stuff back in here
		if (preparingThrow == true) {
			if (currentItem.tag == "FollowingSnowball")
			{
				playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
				if (playerInFirst.tag != gameObject.tag)
				{
					targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().GetTargets()).gameObject;
				}
			}

			if (currentItem.tag == "BlueRocket")
			{
				playerInFirst = gameObject.GetComponent<MissleMovement>().FindPlayerInFirst().gameObject;
				targetObject = playerInFirst.transform.gameObject;
			}

			if (currentItem.tag == "SlowDown")
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
			return;
		}
			//activate snowball
		if (currentItem != null) {
			//activate following snowball
			if (currentItem.tag == "FollowingSnowball" && canUseItem) {
				StartCoroutine (ActivateRedRockets ());
			}
			//activate following snowball
			if (currentItem.tag == "BlueRocket") {
				StartCoroutine (ActivateBlueRockets ());
			}
			//activate speed up
			if (currentItem.tag == "SpeedUp") {
				StartCoroutine (ActivateSpeedUp ());
			}
			//activate slow down
			if (currentItem.tag == "SlowDown") {
				StartCoroutine (ActivateSlowDown ());
			}
			//activate sludge
			if (currentItem.tag == "Sludge") {
				StartCoroutine (ActivateSludge ());
			}
			//activate bomb
			if (currentItem.tag == "Bomb") {
				StartCoroutine (ActivateBomb ());
			}
			//activate Invinc
			if (currentItem.tag == "Invinc") {
				StartCoroutine (ActivateInvinc ());
			}
			//activate gun
			if (currentItem.tag == "Gun") {
				StartCoroutine (ActivateGun());
			}
            //activate molotov
            if (currentItem.tag == "Molotov")
            {
                StartCoroutine(ActivateMolotov());
            }
		}
	}

	public void ActivateSnowBall() {

		if (currentItem.tag == "Snowball" && canUseItem)
		{
			aiItemMoveDirection = gameObject.transform.up;
			snowball = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
			snowball.GetComponent<ItemMovement>().usedByAi = true;
			snowball.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), snowball.GetComponent<CircleCollider2D>(), true);
		}
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
		preparingThrow = false;
	}

	private IEnumerator ActivateRedRockets(){
		
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(5f, 7f));

		if (currentItem.tag == "FollowingSnowball" && canUseItem)
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

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

	private IEnumerator ActivateBlueRockets(){

		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(4f, 6f));

		if (currentItem.tag == "BlueRocket" && canUseItem)
		{
			if(gameObject.tag != playerInFirst.tag){
				blueRocket = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
				blueRocket.GetComponent<ItemMovement>().usedByAi = true;
				blueRocket.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
				blueRocket.GetComponent<ItemMovement>().targetObject = targetObject;
				Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), blueRocket.GetComponent<CircleCollider2D>(), true);  
			}
		}

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

	private IEnumerator ActivateSpeedUp(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(2f, 4f));

		if (currentItem.tag == "SpeedUp" && canUseItem)
		{
			gameObject.GetComponent<AdvancedAI>().cap = 50;
			gameObject.GetComponent<AdvancedAI>().forwardVelocity = 50;                   //call function that destroys item in p1 item box
			StartCoroutine(SpeedUpDurationWait());
		}

		preparingThrow = false;
		pastItem = currentItem;
		aiSpeed = true;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

	private IEnumerator ActivateSlowDown(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(3f, 5f));

		if (currentItem.tag == "SlowDown" && canUseItem)
		{
			int decidingNumToSlow = Random.Range(1, 5);
			if (decidingNumToSlow == 1)
			{
				aiSlowed = true;
				playerToSlow = gameObject;
				playerToSlow.GetComponent<AdvancedAI>().cap = 9;
				playerToSlow.GetComponent<AdvancedAI>().forwardVelocity = 9;
			}
			else
			{
				if (playerToSlow.gameObject.tag.Contains("Sprite"))
				{
					playerToSlow.transform.parent.transform.GetChild(2).transform.GetChild(4).gameObject.GetComponent<Text>().text = gameObject.name + " Slowed You!";
					playerToSlow.GetComponent<UseItem>().playerSlowed = true;
					playerToSlow.GetComponentInParent<Player>().cap = 9;
					playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
				}
				else
				{
					playerToSlow.GetComponent<AdvancedAIItemSystem>().aiSlowed = true;
					playerToSlow.GetComponent<AdvancedAI>().cap = 9;
					playerToSlow.GetComponent<AdvancedAI>().forwardVelocity = 9;
				}
			}
			StartCoroutine(SlowDurationWait());
		}
		//might need to move this one if it waits till the end of the corotine above
		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}
		
	private IEnumerator ActivateSludge(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(3f, 5f));

		if (currentItem.tag == "Sludge" && canUseItem)
		{
			sludge = Instantiate(currentItem, gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity);
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), sludge.GetComponent<BoxCollider2D>(), true);
		}

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

	private IEnumerator ActivateBomb(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(3f, 5f));

		if (currentItem.tag == "Bomb" && canUseItem)
		{
			bomb = Instantiate(currentItem, gameObject.transform.position - new Vector3(0, 4, 0), Quaternion.identity);
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), bomb.GetComponent<CircleCollider2D>(), true);
            StartCoroutine(BombTickWait(bomb));
		}

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

	private IEnumerator ActivateInvinc(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(1f, 3f));

		if (currentItem.tag == "Invinc" && canUseItem)
		{
			isInvinc = true;
			StartCoroutine(InvincDuration());
		}

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
		canUseItem = false;
	}
	//change
	private IEnumerator ActivateGun(){
		preparingThrow = true;

		yield return new WaitForSeconds (Random.Range(3f, 5f));

		if (currentItem.tag == "Gun" && canUseItem)
		{
			hasGun = true;
			canUseItem = false;
			gun = Instantiate(gunPrefab, gameObject.transform.position + new Vector3(1, 0, 0), Quaternion.identity) as GameObject;
            gun.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<Animator> ().SetBool ("FireGun", true);
			gun.gameObject.GetComponent<AudioSource>().Play();
			StartCoroutine(GunDuration());
		}

		preparingThrow = false;
		pastItem = currentItem;
		currentItem = null;
		hasItem = false;
		canPickUpItem = true;
	}

    private IEnumerator ActivateMolotov()
    {
        preparingThrow = true;
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        if (currentItem.tag == "Molotov" && canUseItem)
        {
            aiItemMoveDirection = gameObject.transform.up;
            molotov = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
            molotov.GetComponent<ItemMovement>().usedByAi = true;
            molotov.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
        }
        pastItem = currentItem;
        currentItem = null;
        hasItem = false;
        canPickUpItem = true;
        preparingThrow = false;
    }

	void OnTriggerEnter2D(Collider2D theCollider) {
		if(gameObject.tag.Contains("AI")){
			if(theCollider.gameObject.tag == "RandomItemBox"){
				if(canPickUpItem){
					theCollider.gameObject.GetComponent<RandomItemBox>().ChooseItem(gameObject.GetComponent<AdvancedAI>().currentPlace);
					theCollider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
					theCollider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
//					if (!oneItemTest) {
//						myLabel:
//						if (pastItem.name == theCollider.gameObject.GetComponent<RandomItemBox> ().chosenItem) {
//							theCollider.gameObject.GetComponent<RandomItemBox> ().ChooseItem (gameObject.GetComponent<AdvancedAI> ().currentPlace);
//							count++;
//							if(count >= 4){
//								Debug.Log ("OUT");
//								goto outLabel;
//							}
//							goto myLabel;
//						}
//					}
//					//pickup snowball
//					outLabel:
					if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Snowball")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = snowballPrefab;
						count = 0;
					}

					//pickup red rocket
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "FollowingSnowball")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = followingSnowballPrefab;
						count = 0;
					}

					//pickup blue rocket
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "BlueRocket")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = blueRocketPrefab;
						count = 0;
					}

					//speed up
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "SpeedUp")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = speedUpPrefab;
						count = 0;
					}

					//slow down
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "SlowDown")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = slowDownPrefab;
						count = 0;
					}

					//sludge
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Sludge")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = sludgePrefab;
						count = 0;
					}

					//bomb
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Bomb")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = bombPrefab;
						count = 0;
					}

					//invinc
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Invinc")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = invincPrefab;
						count = 0;
					}

					//gun
					else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Gun")
					{
						hasItem = true;
						canPickUpItem = false;
						currentItem = gunPrefab;
						count = 0;
					}

                    //molotov
                    else if (theCollider.gameObject.GetComponent<RandomItemBox>().chosenItem == "Molotov")
                    {
                        hasItem = true;
                        canPickUpItem = false;
                        currentItem = molotovPrefab;
                        count = 0;
                    }
//					theCollider.gameObject.SetActive (false);
//					theCollider.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
//					theCollider.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				}
			}
		}
	}

	//probably need a few wait for coroutines possibly to first off certian things after a certain time, just make sure it is being called once, so set a bool mayber


	/// <summary>
	/// Speeds up duration wait.
	/// </summary>
	/// <returns>The up duration wait.</returns>
	private IEnumerator SpeedUpDurationWait()
	{
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<AdvancedAI>().cap = 27;
		aiSpeed = false;
	}

	/// <summary>
	/// Slows the duration wait.
	/// </summary>
	/// <returns>The duration wait.</returns>
	private IEnumerator SlowDurationWait()
	{
		yield return new WaitForSeconds(2);
		if(playerToSlow.tag.Contains("Sprite")){
			playerToSlow.GetComponent<UseItem>().playerSlowed = false;
			playerToSlow.transform.parent.GetChild(2).transform.GetChild(4).GetComponent<Text>().enabled = false;
			playerToSlow.GetComponentInParent<Player>().cap = 27;
		}
		else{
			playerToSlow.GetComponent<AdvancedAI>().cap = 30;
			playerToSlow.GetComponent<AdvancedAIItemSystem>().aiSlowed = false;
		}
		finishedSlowWait = true;
	}

	/// <summary>
	/// Invincs the duration.
	/// </summary>
	/// <returns>The duration.</returns>
	private IEnumerator InvincDuration()
	{
		tempAnim = this.GetComponent<Animator> ().runtimeAnimatorController;
		this.GetComponent<Animator> ().runtimeAnimatorController = rainbowAnim;
		yield return new WaitForSeconds(5);
		this.GetComponent<Animator> ().runtimeAnimatorController = tempAnim;
		isInvinc = false;
		canUseItem = true;
	}

	/// <summary>
	/// Guns the duration.
	/// </summary>
	/// <returns>The duration.</returns>
	private IEnumerator GunDuration()
	{
        for (int i = 0; i < 20; i++)
        {
            bullet1 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0, 1, 0), gameObject.transform.rotation);
            bullet2 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(1, 1, 0), gameObject.transform.rotation);
            bullet3 = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(-1, 1, 0), gameObject.transform.rotation);
            bullet1.GetComponent<ItemMovement>().usedByAi = true;
            bullet1.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
            bullet1.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet1.GetComponent<ItemMovement>().bulletDuration());
            bullet2.GetComponent<ItemMovement>().usedByAi = true;
            bullet2.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
            bullet2.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet2.GetComponent<ItemMovement>().bulletDuration());
            bullet3.GetComponent<ItemMovement>().usedByAi = true;
            bullet3.GetComponent<ItemMovement>().aiThatUsedItem = gameObject;
            bullet3.GetComponent<ItemMovement>().bulletDirection = gameObject.transform.up;
            StartCoroutine(bullet3.GetComponent<ItemMovement>().bulletDuration());
            yield return new WaitForSeconds(0.1f);
        }
		hasGun = false;
		canUseItem = true;
		this.GetComponent<Animator> ().SetBool ("FireGun", false);
		Destroy(gun);
	}

    private IEnumerator BombTickWait(GameObject bomb)
    {
        yield return new WaitForSeconds(3);
        bomb.GetComponent<Animator>().SetBool("Explode", true);
        yield return new WaitForSeconds(0.8f);
        bomb.GetComponent<Animator>().SetBool("Explode", false);
        Destroy(bomb);
    }
}

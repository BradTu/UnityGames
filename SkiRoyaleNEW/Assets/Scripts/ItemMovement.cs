/**
 * This script manages the movement of items that player use
 * Jason Komoda 10/19/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
	/// <summary>
	/// The game controller.
	/// </summary>
	public GameController gameController;
	/// <summary>
	/// The item was used by p1.
	/// </summary>
	public bool itemWasUsedByP1;
	/// <summary>
	/// The item was used by p2.
	/// </summary>
	public bool itemWasUsedByP2;
	/// <summary>
	/// The used by A player.
	/// </summary>
	public bool usedByAPlayer;
	/// <summary>
	/// The player that used item.
	/// </summary>
	public GameObject playerThatUsedItem;

    public GameObject aiThatUsedItem;
    public bool usedByAi;
    public GameObject player1;
    public GameObject player2;
    public float velocity;
    public float velocityIncrease;
    public float velocityCap;
    private bool alreadyFoundClosest;
    public GameObject targetPlayer;

    public GameObject AI1;
    public GameObject AI2;
    public GameObject AI3;
    public GameObject AI4;
    public GameObject AI5;
    public GameObject AI6;
    public SoundManager soundManager;

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

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        AI1 = GameObject.FindGameObjectWithTag("AI1");
        AI2 = GameObject.FindGameObjectWithTag("AI2");
        AI3 = GameObject.FindGameObjectWithTag("AI3");
        AI4 = GameObject.FindGameObjectWithTag("AI4");
        AI5 = GameObject.FindGameObjectWithTag("AI5");
        AI6 = GameObject.FindGameObjectWithTag("AI6");
	}

	// Update is called once per frame
	void Update ()
	{
        
	}


	/// <summary>
	///this is fixed update and is the physics update method
	/// </summary>
	private void FixedUpdate ()
	{
        Movement();
    }

	//suggest maybe having this be in different methods for each player, could be nice in the sense of readability, but not necessary.  If not seperate methods, make sure we are putting player number above it
	//Main movement function that decides how each different item moves when players use them
	private void Movement ()
	{
		//player one
		if (itemWasUsedByP1) {
			//snowball item movement
			usedByAPlayer = true;
			if (gameObject.tag == "Snowball") {
                velocity = player1.GetComponent<Player>().forwardVelocity + 10;
                gameObject.transform.position += player1.GetComponentInChildren<UseItem>().p1ItemMoveDirection * Time.deltaTime * velocity;
                velocity += velocityIncrease;
                if(velocity > 35){
                    velocity = 35;
                }
			}

            //following snowball movement
            else if (gameObject.tag == "FollowingSnowball") {
                Vector3 direction = (player1.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.position - gameObject.transform.position).normalized;
                float zRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
                velocity = player1.GetComponent<Player>().forwardVelocity + 10;
                if(player1.transform.GetChild(0).GetComponent<UseItem>().targetObject.tag.Contains("Player")){
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player1.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.GetChild(0).transform.position, velocity * Time.deltaTime);
                }
                else{
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player1.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.position, velocity * Time.deltaTime);
                }
                velocity += velocityIncrease;
                if (velocity > 35)
                {
                    velocity = 35;
                }
			}

		}
		//player two
		if (itemWasUsedByP2) {
			usedByAPlayer = true;

			//snowball movement
			if (gameObject.tag == "Snowball") {
                velocity = player2.GetComponent<Player>().forwardVelocity + 10;
                gameObject.transform.position += player2.GetComponentInChildren<UseItem>().p2ItemMoveDirection * Time.deltaTime * velocity;
                velocity += velocityIncrease;
                if (velocity > 35)
                {
                    velocity = 35;
                }
			}

            //following snowball movement
            else if (gameObject.tag == "FollowingSnowball") {
                Vector3 direction = (player2.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.position - gameObject.transform.position).normalized;
                float zRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
                velocity = player2.GetComponent<Player>().forwardVelocity + 10;
                if (player2.transform.GetChild(0).GetComponent<UseItem>().targetObject.tag.Contains("Player"))
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player2.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.GetChild(0).transform.position, velocity * Time.deltaTime);
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player2.transform.GetChild(0).GetComponent<UseItem>().targetObject.transform.position, velocity * Time.deltaTime);
                }
                velocity += velocityIncrease;
                if (velocity > 35)
                {
                    velocity = 35;
                }
			}
		}

        //AI
        if(usedByAi){

            //snowball movement
            if (gameObject.tag == "Snowball")
            {
                velocity = aiThatUsedItem.GetComponent<BasicAI>().forwardVelocity + 10;
                gameObject.transform.position += aiThatUsedItem.GetComponent<AiItemSystem>().aiItemMoveDirection * Time.deltaTime * velocity;
                velocity += velocityIncrease;
                if (velocity > 35)
                {
                    velocity = 35;
                }
            }

            //following snowball movement
            else if (gameObject.tag == "FollowingSnowball")
            {
                Vector3 direction = (aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.transform.position - gameObject.transform.position).normalized;
                float zRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
                velocity = aiThatUsedItem.GetComponent<BasicAI>().forwardVelocity + 10;
                if (aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.tag.Contains("Player"))
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.transform.GetChild(0).transform.position, velocity * Time.deltaTime);
                }
                else
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.transform.position, velocity * Time.deltaTime);
                }
                velocity += velocityIncrease;
                if (velocity > 35)
                {
                    velocity = 35;
                }
            }
        }
	}

	//Destroy item on collision after use
	private void OnCollisionEnter2D (Collision2D other)
	{
        if(other.gameObject.tag == "HardBorder"){
            if(gameObject.tag == "FollowingSnowball"){
                if (playerThatUsedItem != null && playerThatUsedItem.tag == "P1Sprite" && soundManager.p1UseRocket.isPlaying)
                {
                    soundManager.p1UseRocket.Stop();
                }
                else if (playerThatUsedItem != null && playerThatUsedItem.tag == "P2Sprite" && soundManager.p2UseRocket.isPlaying)
                {
                    soundManager.p2UseRocket.Stop();
                }
                if(aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.tag == "Player1" && soundManager.p1RocketIncoming.isPlaying){
                    soundManager.p1RocketIncoming.Stop();
                }
                if (aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.tag == "Player2" && soundManager.p2RocketIncoming.isPlaying)
                {
                    soundManager.p2RocketIncoming.Stop();
                }
            }
            Destroy(gameObject);
        }

		if (usedByAPlayer && (other.gameObject.tag != playerThatUsedItem.tag)) {
            if(gameObject.tag == "Snowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite"))){
                other.gameObject.GetComponent<HitByItem>().hitBySnowball = true;
            }
            else if(gameObject.tag == "FollowingSnowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite"))){
                other.gameObject.GetComponent<HitByItem>().hitByFollowingSnowball = true;
                if (other.gameObject.tag == "P1Sprite" && soundManager.p1RocketIncoming.isPlaying)
                {
                    soundManager.p1RocketIncoming.Stop();
                    soundManager.p1RocketHit.Play();
                }
                else if (other.gameObject.tag == "P2Sprite" && soundManager.p2RocketIncoming.isPlaying)
                {
                    soundManager.p2RocketIncoming.Stop();
                    soundManager.p2RocketHit.Play();
                }
                else{
                    soundManager.p1RocketHit.Play();
                }
                if(playerThatUsedItem.tag == "P1Sprite" && soundManager.p1UseRocket.isPlaying ){
                    soundManager.p1UseRocket.Stop();

                }
                else if (playerThatUsedItem.tag == "P2Sprite" && soundManager.p2UseRocket.isPlaying)
                {
                    soundManager.p2UseRocket.Stop();
                }
            }
			Destroy (gameObject);
		}

        if(usedByAi && (other.gameObject.tag != aiThatUsedItem.tag)){
            if(gameObject.tag == "Snowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite"))){
                other.gameObject.GetComponent<HitByItem>().hitBySnowball = true;
            }
            else if (gameObject.tag == "FollowingSnowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite")))
            {
                other.gameObject.GetComponent<HitByItem>().hitByFollowingSnowball = true;
                if (other.gameObject.tag == "P1Sprite" && soundManager.p1RocketIncoming.isPlaying)
                {
                    soundManager.p1RocketIncoming.Stop();
                    soundManager.p1RocketHit.Play();
                }
                if (other.gameObject.tag == "P2Sprite" && soundManager.p2RocketIncoming.isPlaying)
                {
                    soundManager.p2RocketHit.Play();
                    soundManager.p2RocketIncoming.Stop();
                }
            }
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "FollowingSnowball"){
            if(usedByAi){
                if(aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.transform.GetChild(0).gameObject.tag == "P1Sprite"){
                    soundManager.p1RocketIncoming.Play();
                }
                else if(aiThatUsedItem.GetComponent<AiItemSystem>().targetObject.transform.GetChild(0).gameObject.tag == "P2Sprite"){
                    soundManager.p2RocketIncoming.Play();
                }
            }
            else if(usedByAPlayer){
                if(playerThatUsedItem.GetComponent<UseItem>().targetObject.transform.GetChild(0).gameObject.tag == "P1Sprite"){
                    soundManager.p1RocketIncoming.Play();
                }
                else if(playerThatUsedItem.GetComponent<UseItem>().targetObject.transform.GetChild(0).gameObject.tag == "P2Sprite"){
                    soundManager.p2RocketIncoming.Play();
                }
            }
        }
    }
}

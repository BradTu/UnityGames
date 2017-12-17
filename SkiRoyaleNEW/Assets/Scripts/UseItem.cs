/**
 * This script implements the usage of items
 * Jason Komoda 10/19/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    /// <summary>
    /// The game controller.
    /// </summary>
    public GameController gameController;
    /// <summary>
    /// snow ball item
    /// </summary>
    private GameObject snowball;
    /// <summary>
    /// The following snowball item
    /// </summary>
    private GameObject followingSnowball;
    /// <summary>
    /// The p1 just used item.
    /// </summary>
    public bool p1JustUsedItem;
    /// <summary>
    /// The p2 just used item.
    /// </summary>
    public bool p2JustUsedItem;
    /// <summary>
    /// The p1 item move direction.
    /// </summary>
    public Vector3 p1ItemMoveDirection;
    /// <summary>
    /// The p1 already got direction.
    /// </summary>
    public bool p1AlreadyGotDirection;
    /// <summary>
    /// The p2 item move direction.
    /// </summary>
    public Vector3 p2ItemMoveDirection;
    /// <summary>
    /// The p2 already got direction.
    /// </summary>
    public bool p2AlreadyGotDirection;
    /// <summary>
    /// The p1 item starting position.
    /// </summary>
    public Vector3 p1ItemStartingPos;
    /// <summary>
    /// The p2 item starting position.
    /// </summary>
    public Vector3 p2ItemStartingPos;

    public GameObject player1;
    public GameObject player2;
    public GameObject playerToSlow;
    private GameObject sludge;
    public GameObject targetObject;

    private SoundManager soundManager;

    // Use this for initialization
    void Start()
    {
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
        if (gameObject.tag == "P1Sprite" && gameController.p1HasItem)
        {
            if (gameController.p1CurrentItem.tag == "FollowingSnowball" || gameController.p1CurrentItem.tag == "SlowDown")
            {
                gameObject.GetComponent<MissleMovement>().GetTargets();
                targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().targets).gameObject;
            }
        }

        if (gameObject.tag == "P2Sprite" && gameController.p2HasItem)
        {
            if (gameController.p2CurrentItem.tag == "FollowingSnowball" || gameController.p2CurrentItem.tag == "SlowDown")
            {
                gameObject.GetComponent<MissleMovement>().GetTargets();
                targetObject = gameObject.GetComponent<MissleMovement>().FindClosest(gameObject.GetComponent<MissleMovement>().targets).gameObject;
            }
        }

        //when player1 activates item
        if (gameObject.tag == "P1Sprite" && gameController.p1HasItem)
        {
            if (Input.GetButtonDown("P1UseItem"))
            {
                if (!p1AlreadyGotDirection)
                {
                    p1ItemMoveDirection = gameObject.transform.up;
                    p1ItemStartingPos = gameObject.transform.position;
                }
                p1AlreadyGotDirection = true;
                p1JustUsedItem = true;

                //activate snowball
                if (gameController.p1CurrentItem.tag == "Snowball")
                {
                    soundManager.p1Snowball.Play();
                    snowball = Instantiate(gameController.p1CurrentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                    snowball.GetComponent<ItemMovement>().usedByAPlayer = true;
                    snowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                    snowball.GetComponent<ItemMovement>().itemWasUsedByP1 = true;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), snowball.GetComponent<CircleCollider2D>(), true);
                    gameObject.GetComponent<PlayerCollision>().P1UsedItem();                    //call function that destroys item in p1 item box
                }

                //activate following snowball
                else if (gameController.p1CurrentItem.tag == "FollowingSnowball")
                {
                    soundManager.p1UseRocket.Play();
                    followingSnowball = Instantiate(gameController.p1CurrentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                    followingSnowball.GetComponent<ItemMovement>().usedByAPlayer = true;
                    followingSnowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                    followingSnowball.GetComponent<ItemMovement>().itemWasUsedByP1 = true;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), followingSnowball.GetComponent<CircleCollider2D>(), true);
                    gameObject.GetComponent<PlayerCollision>().P1UsedItem();                    //call function that destroys item in p1 item box
                }

                //activate speed up
                else if (gameController.p1CurrentItem.tag == "SpeedUp")
                {
                    soundManager.p1SpeedUp.Play();
                    gameObject.GetComponentInParent<Player>().cap = 35;
                    gameObject.GetComponentInParent<Player>().forwardVelocity = 35;
                    gameObject.GetComponent<PlayerCollision>().P1UsedItem();                    //call function that destroys item in p1 item box
                    StartCoroutine(SpeedUpDurationWait());
                }

                //activate slow down
                else if (gameController.p1CurrentItem.tag == "SlowDown")
                {
                    int decidingNumToSlow = Random.Range(1, 5);
                    soundManager.p1SlowDown.Play();
                    if (decidingNumToSlow == 1)
                    {
                        gameController.p1SlowedText.text = "You Slowed Yourself!";
                        playerToSlow = gameObject.transform.parent.gameObject;
                        playerToSlow.GetComponent<Player>().cap = 9;
                        playerToSlow.GetComponent<Player>().forwardVelocity = 9;
                        gameController.player1Slowed = true;
                    }
                    else
                    {
                        playerToSlow = targetObject;
                        if (playerToSlow.gameObject.tag == "Player2")
                        {
                            gameController.p2SlowedText.text = "Player1 Slowed You!";
                            playerToSlow.GetComponentInParent<Player>().cap = 9;
                            playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                            gameController.player2Slowed = true;
                        }
                        else
                        {
                            playerToSlow.GetComponent<BasicAI>().cap = 9;
                            playerToSlow.GetComponent<BasicAI>().forwardVelocity = 9;
                        }
                    }
                    gameObject.GetComponent<PlayerCollision>().P1UsedItem();
                    StartCoroutine(SlowDurationWait());
                }

                //activate sludge
                else if (gameController.p1CurrentItem.tag == "Sludge")
                {
                    soundManager.p1Sludge.Play();
                    sludge = Instantiate(gameController.p1CurrentItem, gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
                    gameObject.GetComponent<PlayerCollision>().P1UsedItem();
                }
            }
        }

        //when player2 activates item
        if (gameObject.tag == "P2Sprite" && gameController.p2HasItem)
        {
            if (Input.GetButtonDown("P2UseItem"))
            {
                if (!p2AlreadyGotDirection)
                {
                    p2ItemMoveDirection = gameObject.transform.up;
                    p2ItemStartingPos = gameObject.transform.position;
                }
                p2AlreadyGotDirection = true;
                p2JustUsedItem = true;

                //activate snowball
                if (gameController.p2CurrentItem.tag == "Snowball")
                {
                    soundManager.p2Snowball.Play();
                    snowball = Instantiate(gameController.p2CurrentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                    snowball.GetComponent<ItemMovement>().usedByAPlayer = true;
                    snowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                    snowball.GetComponent<ItemMovement>().itemWasUsedByP2 = true;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), snowball.GetComponent<CircleCollider2D>(), true);
                    gameObject.GetComponent<PlayerCollision>().P2UsedItem();                  //call function that destroys item in p2 item box
                }

                //activate following snowball
                else if (gameController.p2CurrentItem.tag == "FollowingSnowball")
                {
                    soundManager.p2UseRocket.Play();
                    followingSnowball = Instantiate(gameController.p2CurrentItem, gameObject.transform.position, Quaternion.identity) as GameObject;
                    followingSnowball.GetComponent<ItemMovement>().usedByAPlayer = true;
                    followingSnowball.GetComponent<ItemMovement>().playerThatUsedItem = gameObject;
                    followingSnowball.GetComponent<ItemMovement>().itemWasUsedByP2 = true;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), followingSnowball.GetComponent<CircleCollider2D>(), true);
                    gameObject.GetComponent<PlayerCollision>().P2UsedItem();                  //call function that destroys item in p1 item box
                }

                //activate speed up
                else if (gameController.p2CurrentItem.tag == "SpeedUp")
                {
                    soundManager.p2SpeedUp.Play();
                    gameObject.GetComponentInParent<Player>().cap = 35;
                    gameObject.GetComponentInParent<Player>().forwardVelocity = 35;
                    gameObject.GetComponent<PlayerCollision>().P2UsedItem();                    //call function that destroys item in p1 item box
                    StartCoroutine(SpeedUpDurationWait());
                }

                //activate slow down
                else if (gameController.p2CurrentItem.tag == "SlowDown")
                {
                    soundManager.p2SlowDown.Play();
                    gameObject.GetComponent<PlayerCollision>().P2UsedItem();
                    int decidingNumToSlow = Random.Range(1, 5);
                    if (decidingNumToSlow == 1)
                    {
                        gameController.p2SlowedText.text = "You Slowed Yourself!";
                        playerToSlow = gameObject.transform.parent.gameObject;
                        playerToSlow.GetComponentInParent<Player>().cap = 9;
                        playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                        gameController.player2Slowed = true;
                    }
                    else
                    {
                        playerToSlow = targetObject;

                        if (playerToSlow.gameObject.tag == "Player1")
                        {
                            gameController.p1SlowedText.text = "Player2 Slowed You!";
                            playerToSlow.GetComponentInParent<Player>().cap = 9;
                            playerToSlow.GetComponentInParent<Player>().forwardVelocity = 9;
                            gameController.player1Slowed = true;
                        }
                        else
                        {
                            playerToSlow.GetComponent<BasicAI>().cap = 9;
                            playerToSlow.GetComponent<BasicAI>().forwardVelocity = 9;
                        }
                    }
                    StartCoroutine(SlowDurationWait());
                }

                //activate sludge
                else if (gameController.p2CurrentItem.tag == "Sludge")
                {
                    soundManager.p2Sludge.Play();
                    sludge = Instantiate(gameController.p2CurrentItem, gameObject.transform.position - new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), sludge.GetComponent<BoxCollider2D>(), true);  
                    gameObject.GetComponent<PlayerCollision>().P2UsedItem();
                }
            }
        }
    }

    private IEnumerator SpeedUpDurationWait()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponentInParent<Player>().cap = 25;
    }

    private IEnumerator SlowDurationWait()
    {
        yield return new WaitForSeconds(2);
        if (playerToSlow.gameObject.tag == "Player1" ||playerToSlow.gameObject.tag == "Player2")
        {
            if(playerToSlow.gameObject.tag == "Player1"){
                gameController.player1Slowed = false;
            }
            else{
                gameController.player2Slowed = false;
            }
            playerToSlow.GetComponent<Player>().cap = 25;
        }
        else
        {
            playerToSlow.GetComponent<BasicAI>().cap = 23;
        }
    }
}

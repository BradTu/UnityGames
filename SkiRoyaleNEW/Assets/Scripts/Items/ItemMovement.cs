/**
 * This script manages the movement of items that player use
 * Jason Komoda 10/19/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{

    public GameController gameController;
    public bool usedByAPlayer;
    [HideInInspector]
    public GameObject playerThatUsedItem;
    public GameObject aiThatUsedItem;
    public bool usedByAi;
    public float velocity;
    public float standardVelIncrease;
    public float followingSnowballVelIncrease;
    public float blueRocketVelIncrease;
    public float velocityCap;
    public GameObject firePrefab;
    private GameObject fire;
    public float distTravelled;
    private bool alreadyFoundClosest;
    public GameObject targetPlayer;
    public SoundManager soundManager;
    public GameObject playerInFirst;
    public GameObject targetObject;
    public Vector3 bulletDirection;
    private bool molotovExploded;

	public float snowballHit;
	public float blueRocketHit;
	public float missileHit;
    public float bulletHit;

    public bool alreadyDestroyedBullet;

    // Use this for initialization
    void Start()
    {
        //Get game controller reference
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
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
        standardVelIncrease = 5;
        followingSnowballVelIncrease = 10;
        blueRocketVelIncrease = 25;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    ///this is fixed update and is the physics update method
    /// </summary>
    private void FixedUpdate()
    {
        Movement();
    }

    //suggest maybe having this be in different methods for each player, could be nice in the sense of readability, but not necessary.  If not seperate methods, make sure we are putting player number above it
    //Main movement function that decides how each different item moves when players use them
    private void Movement()
    {
        //item used by player
        if (usedByAPlayer)
        {
            //snowball item movement
            if (gameObject.tag == "Snowball")
            {
                velocity = 50;
                gameObject.transform.position += playerThatUsedItem.GetComponent<UseItem>().itemMoveDirection * Time.deltaTime * velocity;
                velocity += standardVelIncrease;
                if (velocity > 65)
                {
                    velocity = 65;
                }
            }

            //red rocket movement
            else if (gameObject.tag == "FollowingSnowball" || gameObject.tag == "BlueRocket")
            {
                Vector3 direction = (targetObject.transform.position - gameObject.transform.position).normalized;
                float zRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
                velocity = 80;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetObject.transform.position, velocity * Time.deltaTime);
                if(gameObject.tag == "FollowingSnowball"){
                    velocity += followingSnowballVelIncrease;
                    if (velocity > 110)
                    {
                        velocity = 110;
                    }
                }
                else{
                    velocity += blueRocketVelIncrease;
                    if(velocity > 200){
                        velocity = 200;
                    }
                }
            }

            //bullet movement
            else if(gameObject.tag == "Bullet"){
                velocity = playerThatUsedItem.gameObject.transform.parent.GetComponent<Player>().forwardVelocity + 30;
                gameObject.transform.position += bulletDirection * Time.deltaTime * velocity;
                velocity += standardVelIncrease;
            }

            //molotov movement
            else if(gameObject.tag == "Molotov"){
                if(!molotovExploded){
                    velocity = playerThatUsedItem.gameObject.transform.parent.GetComponent<Player>().forwardVelocity + 30;;
                    gameObject.transform.position += playerThatUsedItem.GetComponent<UseItem>().itemMoveDirection * Time.deltaTime * velocity;
                    distTravelled += (playerThatUsedItem.GetComponent<UseItem>().itemMoveDirection * Time.deltaTime * velocity).y;
                    if (distTravelled >= 42)
                    {
                        molotovExploded = true;
                        gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
                    }
                    velocity += standardVelIncrease;
                }

            }
        }


        //item used by AI
        if (usedByAi)
        {

            //snowball movement
            if (gameObject.tag == "Snowball")
            {
                velocity = 50;
                gameObject.transform.position += aiThatUsedItem.GetComponent<AdvancedAIItemSystem>().aiItemMoveDirection * Time.deltaTime * velocity;
                velocity += standardVelIncrease;
                if (velocity > 65)
                {
                    velocity = 65;
                }
            }

            //red rocket movement
            else if (gameObject.tag == "FollowingSnowball" || gameObject.tag == "BlueRocket")
            {
                Vector3 direction = (targetObject.transform.position - gameObject.transform.position).normalized;
                float zRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
                velocity = 70;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetObject.transform.position, velocity * Time.deltaTime);
                if (gameObject.tag == "FollowingSnowball")
                {
                    velocity += followingSnowballVelIncrease;
                    if (velocity > 110)
                    {
                        velocity = 110;
                    }
                }
                else
                {
                    velocity += blueRocketVelIncrease;
                    if (velocity > 200)
                    {
                        velocity = 200;
                    }
                }
            }

            //bullet movement
            else if (gameObject.tag == "Bullet")
            {
                velocity = aiThatUsedItem.gameObject.GetComponent<AdvancedAI>().forwardVelocity + 30;
                gameObject.transform.position += bulletDirection * Time.deltaTime * velocity;
                velocity += standardVelIncrease;
            }

            //molotov movement
            else if (gameObject.tag == "Molotov")
            {
                if (!molotovExploded)
                {
                    velocity = aiThatUsedItem.gameObject.GetComponent<AdvancedAI>().forwardVelocity + 30; ;
                    gameObject.transform.position += aiThatUsedItem.GetComponent<AdvancedAIItemSystem>().aiItemMoveDirection * Time.deltaTime * velocity;
                    distTravelled += (aiThatUsedItem.GetComponent<AdvancedAIItemSystem>().aiItemMoveDirection * Time.deltaTime * velocity).y;
                    if (distTravelled >= 45)
                    {
                        gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
                        molotovExploded = true;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
                    }
                    velocity += standardVelIncrease;
                }

            }
        }
    }

    //Destroy the on collision and affect players depending on what item hit them
    private void OnCollisionEnter2D(Collision2D other)
    {
        //item used by a player
        if (usedByAPlayer && (other.gameObject.tag != playerThatUsedItem.tag))
        {
            //snowball
            if (gameObject.tag == "Snowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite")))
            {
                if (other.gameObject.tag.Contains("Sprite"))
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponentInParent<Player>().gotHit = true;
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponentInParent<Player> ().health -= snowballHit;
                    }
                }
                else
                {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponentInParent<AdvancedAI>().gotHit = true;
						other.gameObject.GetComponent<AdvancedAI> ().health -= snowballHit;
                    }
                }
                Destroy(gameObject);
            }

            //bullet
            else if (gameObject.tag == "Bullet" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite")))
            {
                if (other.gameObject.tag.Contains("Sprite"))
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponentInParent<Player>().health -= bulletHit;
                    }
                }
                else
                {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<AdvancedAI>().health -= bulletHit;
                    }
                }
                Destroy(gameObject);
                alreadyDestroyedBullet = true;
            }

            //blue rocket hits player
            else if (gameObject.tag == "BlueRocket" && other.gameObject.tag.Contains("Sprite"))
            {
                playerInFirst = playerThatUsedItem.GetComponent<UseItem>().playerInFirst;
                if (other.gameObject.tag != playerInFirst.tag)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
                        other.gameObject.GetComponentInParent<Player>().gotHit = true;
						other.gameObject.GetComponentInParent<Player> ().health -= blueRocketHit;
                    }
                    SetPlayerTargetedByMissleFalse();
                    other.gameObject.GetComponentInParent<Player>().PlayPlayerRocketHit();
                    Destroy(gameObject);
                }
            }

            //blue rocket hits AI
            else if (gameObject.tag == "BlueRocket" && other.gameObject.tag.Contains("AI"))
            {
                playerInFirst = playerThatUsedItem.GetComponent<UseItem>().playerInFirst;
                if (other.gameObject.tag != playerInFirst.tag)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponent<AdvancedAI> ().health -= blueRocketHit;
                    }
                    other.gameObject.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }
            }

            //red rocket hits player
            else if (gameObject.tag == "FollowingSnowball" && other.gameObject.tag.Contains("Sprite"))
            {
				if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                {
                    other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
					other.gameObject.GetComponentInParent<Player> ().health -= missileHit;
                }
                other.gameObject.GetComponentInParent<Player>().PlayPlayerRocketHit();
                SetPlayerTargetedByMissleFalse();
                Destroy(gameObject);
            }

            //red rocket hits AI
            else if (gameObject.tag == "FollowingSnowball" && other.gameObject.tag.Contains("AI"))
            {
				if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                {
                    other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
					other.gameObject.GetComponent<AdvancedAI> ().health -= missileHit;
                }
                other.gameObject.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }

        //item used by AI
        if (usedByAi && (other.gameObject.tag != aiThatUsedItem.tag))
        {
            //snowball
            if (gameObject.tag == "Snowball" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite")))
            {
                if (other.gameObject.tag.Contains("Sprite"))
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponentInParent<Player> ().health -= snowballHit;
                    }
                }
                else
                {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponent<AdvancedAI> ().health -= snowballHit;
                    }
                }
                Destroy(gameObject);
            }

            //bullet
            else if (gameObject.tag == "Bullet" && (other.gameObject.tag.Contains("AI") || other.gameObject.tag.Contains("Sprite")))
            {
                if (other.gameObject.tag.Contains("Sprite"))
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponentInParent<Player>().health -= bulletHit;
                    }
                }
                else
                {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<AdvancedAI>().health -= bulletHit;
                    }
                }
                Destroy(gameObject);
                alreadyDestroyedBullet = true;
            }

            //blue rocket hits player
            else if (gameObject.tag == "BlueRocket" && other.gameObject.tag.Contains("Sprite"))
            {
                playerInFirst = aiThatUsedItem.GetComponent<AdvancedAIItemSystem>().playerInFirst;
                if (other.gameObject.tag != playerInFirst.tag)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                {
					if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponentInParent<Player> ().health -= blueRocketHit;
                    }
                    SetPlayerTargetedByMissleFalse();
					//make sure they have a sound
                    other.gameObject.GetComponentInParent<Player>().PlayPlayerRocketHit();
                    Destroy(gameObject);
                }
            }

            //blue rocket hits AI
            else if (gameObject.tag == "BlueRocket" && other.gameObject.tag.Contains("AI"))
            {
                playerInFirst = aiThatUsedItem.GetComponent<AdvancedAIItemSystem>().playerInFirst;
                if (other.gameObject.tag != playerInFirst.tag)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>());
                }
                else{
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                    {
                        other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
						other.gameObject.GetComponent<AdvancedAI>().health -= blueRocketHit;
                    }
                    Destroy(gameObject);
                }
            }

            //red rocket hits player
            else if (gameObject.tag == "FollowingSnowball" && other.gameObject.tag.Contains("Sprite"))
            {
				if (!other.gameObject.GetComponent<UseItem>().isInvinc && !other.gameObject.GetComponentInParent<Player>().isDead)
                {
                    other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
					other.gameObject.GetComponentInParent<Player> ().health -= missileHit;
                }
				//make sure have sound
                other.gameObject.GetComponentInParent<Player>().PlayPlayerRocketHit();
                SetPlayerTargetedByMissleFalse();
                Destroy(gameObject);
            }

            //red rocket hits AI
            else if (gameObject.tag == "FollowingSnowball" && other.gameObject.tag.Contains("AI"))
            {
				if (!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc && !other.gameObject.GetComponent<AdvancedAI>().isDead)
                {
                    other.gameObject.GetComponent<HitByItem>().hitByProjectile = true;
					other.gameObject.GetComponentInParent<AdvancedAI> ().health -= missileHit;
                }
                Destroy(gameObject);
            }
        }
    }

	//when rocket targets a player and enter the player's radius, show alert icon on their screen
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "FollowingSnowball" || gameObject.tag == "BlueRocket")
        {
			if (targetObject != null) {
				if (targetObject.tag.Contains ("Sprite")) {
					targetObject.gameObject.GetComponent<UseItem> ().isTargetedByMissle = true;
				} 
			}
        }
    }

    //untarget the player from the rocket
    private void SetPlayerTargetedByMissleFalse()
    {
        if (targetObject.tag.Contains("Sprite"))
        {
            targetObject.GetComponent<UseItem>().isTargetedByMissle = false;
            Destroy(gameObject);
        }
    }

    //how long each set of bullets last
    public IEnumerator bulletDuration(){
        yield return new WaitForSeconds(.75f);
        if(!alreadyDestroyedBullet){
            Destroy(gameObject);
        }
    }
}

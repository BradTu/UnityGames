/**
 * This script controls the pole attacks
 * Jason Komoda 10/12/17
 **/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAttack : MonoBehaviour
{
	protected Animator poleSwingAnimator;
    protected Animator playerSwingAnimator;
	//animator for pole swings

	public float knockBack;
    public float lunge;

    public float trigger;

	public Animator playerAnimator;

	//	public float inputHoldDelay = 0.2f;
	//
	//	public GameObject hitObject;
	// The WaitForSeconds used to make the user wait before input is handled again.
	//private WaitForSeconds inputHoldWait;

	/*--------------------------------------------------------------------------------------------------
    //pause time for invincibility
    public float invincibleTime;
    --------------------------------------------------------------------------------------------------*/

    public SoundManager soundManager;

	// Use this for initialization
	void Start ()
    {
		//inputHoldWait = new WaitForSeconds (inputHoldDelay);

		/*--------------------------------------------------------------------------------------------------
        invincibleTime = 2;
        --------------------------------------------------------------------------------------------------*/
		GetComponent<SpriteRenderer> ().enabled = false;

        GameObject soundManagerObject = GameObject.FindWithTag("SoundManager");         if (soundManagerObject != null)         {             soundManager = soundManagerObject.GetComponent<SoundManager>();         }

	}

	// Update is called once per frame
	void Update ()
	{
		StartCoroutine (PoleAttackClick ());
        if (gameObject.transform.parent.gameObject.tag == "P1Sprite") {
            trigger = Input.GetAxis("P1Fire1");
        }
        else if(gameObject.transform.parent.gameObject.tag == "P2Sprite") {
            trigger = Input.GetAxis("P2Fire1");
        }

	}

	//Attack with poles using left and right click
	private IEnumerator PoleAttackClick ()
	{
		if (gameObject.transform.parent.gameObject.tag == "P1Sprite") {
			if (Input.GetButtonDown ("P1Fire1") && gameObject.tag == "P1LeftPole" || trigger < -.5f) { // q click to attack left side
                soundManager.GetComponent<SoundManager>().p1PoleAttack.Play();
                //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(-lunge, 0);
				playerAnimator.enabled = true;
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("LeftClick", true);
                playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
                playerSwingAnimator.SetBool("AttackLeft", true);
				yield return new WaitForSeconds (0.5f);
                //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				poleSwingAnimator.SetBool ("LeftClick", false);
                playerSwingAnimator.SetBool("AttackLeft", false);
				playerAnimator.enabled = false;
			}
			if (Input.GetButtonDown ("P1Fire2") && gameObject.tag == "P1RightPole" || trigger > .5f) { // e click to attack right side
                soundManager.GetComponent<SoundManager>().p1PoleAttack.Play();
                //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(lunge, 0);
				playerAnimator.enabled = true;
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("RightClick", true);
                playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
                playerSwingAnimator.SetBool("AttackRight", true);
				yield return new WaitForSeconds (0.5f);
               // gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				poleSwingAnimator.SetBool ("RightClick", false);
                playerSwingAnimator.SetBool("AttackRight", false);
				playerAnimator.enabled = false;
			}
		} else if (gameObject.transform.parent.gameObject.tag == "P2Sprite") {
			if (Input.GetButtonDown ("P2Fire1") && gameObject.tag == "P2LeftPole" || trigger < -.5f) { // u click to attack left side
                soundManager.GetComponent<SoundManager>().p2PoleAttack.Play();
                //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(-lunge, 0);
				playerAnimator.enabled = true;
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("LeftClick", true);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
				playerSwingAnimator.SetBool("AttackLeft", true);
				yield return new WaitForSeconds (0.5f);
				//gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				poleSwingAnimator.SetBool ("LeftClick", false);
				playerSwingAnimator.SetBool("AttackLeft", false);
				playerAnimator.enabled = false;
			}
			if (Input.GetButtonDown ("P2Fire2") && gameObject.tag == "P2RightPole" || trigger > .5f) { // o click to attack right side
                soundManager.GetComponent<SoundManager>().p2PoleAttack.Play();
                //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(lunge, 0);
				playerAnimator.enabled = true;
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("RightClick", true);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator>();
				playerSwingAnimator.SetBool("AttackRight", true);
				yield return new WaitForSeconds (0.5f);
				// gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				poleSwingAnimator.SetBool ("RightClick", false);
				playerSwingAnimator.SetBool("AttackRight", false);
				playerAnimator.enabled = false;
			}
		}
	}

	// When poles hit AI or other players
	//this will get what pole is hitting, and the sets the hitObject to what was hit to make sure that velocity is changed back
	//might need to add more hits when adding in multiple players
	//invincibity here probably
	private void OnCollisionEnter2D (Collision2D other)
	{
		poleSwingAnimator = gameObject.GetComponent<Animator> ();
		if ((other.gameObject.tag == "P1Sprite" || other.gameObject.tag == "P2Sprite") && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))) {
			if (poleSwingAnimator.GetBool ("LeftClick")) {

				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
				other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
				/*---------------------------------------------------------------------------------------------------------------
                    //Invincibility for seconds using pt
                    invincible();
                    ---------------------------------------------------------------------------------------------------------------*/
			} else {

				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
				other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();


				/*---------------------------------------------------------------------------------------------------------------
                    //Invincibility for seconds using pt
                    invincible();
                    ---------------------------------------------------------------------------------------------------------------*/
			}
		}
		if ((other.gameObject.name.Contains("AI")) && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))) {
			if (poleSwingAnimator.GetBool ("LeftClick")) {

				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
				other.gameObject.GetComponentInParent<BasicAI> ().StartWaitForHit ();
				/*---------------------------------------------------------------------------------------------------------------
                    //Invincibility for seconds using pt
                    invincible();
                    ---------------------------------------------------------------------------------------------------------------*/
			} else {

				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
				other.gameObject.GetComponentInParent<BasicAI> ().StartWaitForHit ();


				/*---------------------------------------------------------------------------------------------------------------
                    //Invincibility for seconds using pt
                    invincible();
                    ---------------------------------------------------------------------------------------------------------------*/
			}
		}
	}

	/*---------------------------------------------------------------------------------------------------------------------------------
    //Set the player and pole to ignore collision then wait for pt seconds and call endInvincibility
    private void invincible()
    {
        Physics2D.IgnoreCollision(hitObject.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        //Debug.Log("invincible");
        //Make the sprite opaque
        if (hitObject.gameObject.tag == "P2Sprite")
        {
            hitObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 235f, .75f);
        }
        else
        {
            hitObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .75f);
        }
        Invoke("endInvincibility", invincibleTime);
        
    }
    -----------------------------------------------------------------------------------------------------------------------------------*/

	/*
    //This turns off the collision being ignored
    private void endInvincibility()
    {
        Physics2D.IgnoreCollision(hitObject.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        //Restore sprite opacity
        if (hitObject.gameObject.tag == "P2Sprite")
        {
            hitObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 235f, 1f);
        }
        else
        {
            hitObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        hitObject = null;
        //Debug.Log("!!NOT INVINCIBLE!!");
    }
	*/
}

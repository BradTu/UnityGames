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
	public Animator poleSwingAnimator;
    public Animator playerSwingAnimator;
	//animator for pole swings

	public float knockBack;
	public float knockBackAI;
    public float lunge;

    public float trigger;

	public Animator playerAnimator;

    public SoundManager soundManager;

	// Use this for initialization
	void Start ()
    {
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
        else if (gameObject.transform.parent.gameObject.tag == "P3Sprite")
        {
            trigger = Input.GetAxis("P3Fire1");
        }
		else if (gameObject.transform.parent.gameObject.tag == "P4Sprite")
		{
			trigger = Input.GetAxis("P4Fire1");
		}
    }

	//Attack with poles using left and right click
	private IEnumerator PoleAttackClick ()
	{
		if (gameObject.transform.parent.gameObject.tag == "P1Sprite") {
			
			if (Input.GetButtonDown ("P1Fire1") && gameObject.tag == "P1LeftPole" || trigger < -.5f) { // q click to attack left side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player1")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p1PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", true);
					yield return new WaitForSeconds (0.7f);
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator.SetBool ("AttackLeft", false);
				}
			}
			if (Input.GetButtonDown ("P1Fire2") && gameObject.tag == "P1RightPole" || trigger > .5f) { // e click to attack right side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player1")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p1PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", true);
					yield return new WaitForSeconds (0.7f);
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator.SetBool ("AttackRight", false);
				}
			}
		} else if (gameObject.transform.parent.gameObject.tag == "P2Sprite") {
			if (Input.GetButtonDown ("P2Fire1") && gameObject.tag == "P2LeftPole" || trigger < -.5f) { // u click to attack left side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player2")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p2PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator.SetBool ("AttackLeft", false);
				}
			}
			if (Input.GetButtonDown ("P2Fire2") && gameObject.tag == "P2RightPole" || trigger > .5f) { // o click to attack right side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player2")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p2PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator.SetBool ("AttackRight", false);
				}
			}
		} else if (gameObject.transform.parent.gameObject.tag == "P3Sprite") {
			if (trigger < -.5f) { // u click to attack left side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player3")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p3PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator.SetBool ("AttackLeft", false);
				}
			}
			if (trigger > .5f) { // o click to attack right side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player3")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p3PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator.SetBool ("AttackRight", false);
				}
			}
		} else if (gameObject.transform.parent.gameObject.tag == "P4Sprite") {
			if (trigger < -.5f) { // u click to attack left side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player4")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p4PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("LeftClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackLeft", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("LeftClick", false);
					playerSwingAnimator.SetBool ("AttackLeft", false);
				}
			}
			if (trigger > .5f) { // o click to attack right side
				if (((gameObject.transform.parent.gameObject.transform.parent.GetComponent<Player> ().hit == true) || 
					(gameObject.transform.parent.GetComponent<HitByItem>().hitByProjectile == true)) && (gameObject.transform.parent.gameObject.transform.parent.tag == "Player4")) {
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", false);
				} else {
					soundManager.GetComponent<SoundManager> ().p4PoleAttack.Play ();
					poleSwingAnimator = gameObject.GetComponent<Animator> ();
					poleSwingAnimator.SetBool ("RightClick", true);
					playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
					playerSwingAnimator.SetBool ("AttackRight", true);
					yield return new WaitForSeconds (0.5f);
					poleSwingAnimator.SetBool ("RightClick", false);
					playerSwingAnimator.SetBool ("AttackRight", false);
				}
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
		if ((other.gameObject.tag == "P1Sprite" || other.gameObject.tag == "P2Sprite" || other.gameObject.tag == "P3Sprite" || other.gameObject.tag == "P4Sprite") && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))) {
			if ((poleSwingAnimator.GetBool ("LeftClick")) && other.otherCollider.name.Contains("Left") ) {
                if(!other.gameObject.GetComponent<UseItem>().isInvinc){
					if (!other.gameObject.GetComponentInParent<Player> ().hit == true) {
						other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
						other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
					}
                }
			} else {
				if (other.otherCollider.name.Contains ("Right")) {
					if (!other.gameObject.GetComponent<UseItem> ().isInvinc) {
						if (!other.gameObject.GetComponentInParent<Player> ().hit == true) {
							other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
							other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();   
						}
					}
				}
			}
		}
		if ((other.gameObject.name.Contains("AdvancedAI")) && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))) {
			if (poleSwingAnimator.GetBool ("LeftClick") && other.otherCollider.name.Contains("Left")) {
                if(!other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc){
					if (!other.gameObject.GetComponent<AdvancedAI> ().hit == true) {
						other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBackAI, 0);
						other.gameObject.GetComponent<AdvancedAI> ().StartWaitForHit (); 
					}
                }
			} else {
				if (other.otherCollider.name.Contains ("Right")) {
					if (!other.gameObject.GetComponent<AdvancedAIItemSystem> ().isInvinc) {
						if (!other.gameObject.GetComponent<AdvancedAI> ().hit == true) {
							other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBackAI, 0);
							other.gameObject.GetComponent<AdvancedAI> ().StartWaitForHit ();
						}
					}
				}
			}
		}

		if ((other.gameObject.name.Contains("Button")) && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))) {
			if (poleSwingAnimator.GetBool ("LeftClick") && other.otherCollider.name.Contains("Left")) {
				other.gameObject.GetComponent<ObstacleButton> ().Coll ();
			} else {
				if (other.otherCollider.name.Contains ("Right")) {
					other.gameObject.GetComponent<ObstacleButton> ().Coll ();
				}
			}
		}
	}
}
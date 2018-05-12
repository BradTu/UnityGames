using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAIPoleAttack : PoleAttack {

	public bool hit;

	public float speed;

	public bool swing;
	/// <summary>
	/// Starts the left attack.
	/// </summary>
	public void StartLeftAttack(){
		if (swing == true) {
			return;
		}
		StartCoroutine (PoleAttackAILeft ());
	}

	/// <summary>
	/// Starts the right attack.
	/// </summary>
	public void StartRightAttack(){
		if (swing == true) {
			return;
		}
		StartCoroutine (PoleAttackAIRight ());
	}

	/// <summary>
	/// Poles the attack AI left.
	/// </summary>
	/// <returns>The attack AI left.</returns>
	private IEnumerator PoleAttackAILeft ()
	{
		swing = true;

		yield return new WaitForSeconds(Random.Range(.1f, .6f));

		if (gameObject.tag == "AILeftPole") {
			if ((gameObject.transform.parent.GetComponent<AdvancedAI> ().hit == true)) {
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("LeftClick", false);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
				playerSwingAnimator.SetBool ("AttackLeft", false);
			} else {
				poleSwingAnimator = GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("LeftClick", true);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
				playerSwingAnimator.SetBool ("AttackLeft", true);
				yield return new WaitForSeconds (0.7f);
				poleSwingAnimator.SetBool ("LeftClick", false);
				playerSwingAnimator.SetBool ("AttackLeft", false);
			}
		}
		swing = false;
	}

	private IEnumerator PoleAttackAIRight ()
	{
		swing = true;

		yield return new WaitForSeconds(Random.Range(.1f, .6f));

		if (gameObject.tag == "AIRightPole") {
			if ((gameObject.transform.parent.GetComponent<AdvancedAI> ().hit == true)) {
				poleSwingAnimator = gameObject.GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("RightClick", false);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
				playerSwingAnimator.SetBool ("AttackRight", false);
			} else {
				poleSwingAnimator = GetComponent<Animator> ();
				poleSwingAnimator.SetBool ("RightClick", true);
				playerSwingAnimator = gameObject.transform.parent.gameObject.GetComponent<Animator> ();
				playerSwingAnimator.SetBool ("AttackRight", true);
				yield return new WaitForSeconds (0.7f);
				poleSwingAnimator.SetBool ("RightClick", false);
				playerSwingAnimator.SetBool ("AttackRight", false);
			}
		}
		swing = false;
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		poleSwingAnimator = GetComponent<Animator> ();
		if ((other.gameObject.name.Contains("AdvancedAI")) && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))){
			if (poleSwingAnimator.GetBool ("LeftClick") && other.otherCollider.name.Contains("Left")) {
                if (!other.gameObject.GetComponent<AdvancedAI> ().hit == true && !other.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc) {
					other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
					other.gameObject.GetComponent<AdvancedAI> ().StartWaitForHit ();
				}
			} else{
				if (other.otherCollider.name.Contains ("Right")) {
					if (!other.gameObject.GetComponent<AdvancedAI> ().hit == true && !other.gameObject.GetComponent<AdvancedAIItemSystem> ().isInvinc) {
						other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
						other.gameObject.GetComponent<AdvancedAI> ().StartWaitForHit ();
					}
				}
			}
		}

		if ((other.gameObject.tag == "P1Sprite" || other.gameObject.tag == "P2Sprite" || other.gameObject.tag == "P3Sprite" || other.gameObject.tag == "P4Sprite") && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))){
			if (poleSwingAnimator.GetBool ("LeftClick") && other.otherCollider.name.Contains("Left")) {
                if (!other.gameObject.GetComponentInParent<Player> ().hit == true && !other.gameObject.GetComponent<UseItem>().isInvinc) {
					other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBackAI, 0);
					other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
				}
			} else {
				if (other.otherCollider.name.Contains ("Right")) {
					if (!other.gameObject.GetComponentInParent<Player> ().hit == true && !other.gameObject.GetComponent<UseItem> ().isInvinc) {
						other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBackAI, 0);
						other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
					}
				}
			}
		}


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shouldn't need to change that much for advanced ai, just make sure animations happen for ai
public class PoleAttackAI : PoleAttack {
	public bool hit;

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine (PoleAttackClick ());
	}

	public void StartLeftAttack(){
		StartCoroutine (PoleAttackAILeft ());
	}

	public void StartRightAttack(){
		StartCoroutine (PoleAttackAIRight ());
	}

	private IEnumerator PoleAttackAILeft ()
	{
		yield return new WaitForSeconds(Random.Range(.5f, .8f));
		
		if (gameObject.tag == "AILeftPole") {
			poleSwingAnimator = GetComponent<Animator> ();
			poleSwingAnimator.SetBool ("LeftClick", true);
			yield return new WaitForSeconds (0.7f);
			poleSwingAnimator.SetBool ("LeftClick", false);
		}
		 
	}

	private IEnumerator PoleAttackAIRight ()
	{
		yield return new WaitForSeconds(Random.Range(.5f, .8f));
		if (gameObject.tag == "AIRightPole") {
			poleSwingAnimator = GetComponent<Animator> ();
			poleSwingAnimator.SetBool ("RightClick", true);
			yield return new WaitForSeconds (0.7f);
			poleSwingAnimator.SetBool ("RightClick", false);
		}
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		poleSwingAnimator = GetComponent<Animator> ();
		if ((other.gameObject.name.Contains("AI")) && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))){
			if (poleSwingAnimator.GetBool ("LeftClick")) {
				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
				other.gameObject.GetComponentInParent<BasicAI> ().StartWaitForHit ();
			} else{
				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
				other.gameObject.GetComponentInParent<BasicAI> ().StartWaitForHit ();
			}
		}

		if ((other.gameObject.tag == "P1Sprite" || other.gameObject.tag == "P2Sprite" || other.gameObject.tag == "P3Sprite" || other.gameObject.tag == "P4Sprite") && (poleSwingAnimator.GetBool ("LeftClick") || poleSwingAnimator.GetBool ("RightClick"))){
			if (poleSwingAnimator.GetBool ("LeftClick")) {
				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockBack, 0);
				other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
			} else {
				other.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 0);
				other.gameObject.GetComponentInParent<Player> ().StartWaitForHit ();
			}
		}


	}
}

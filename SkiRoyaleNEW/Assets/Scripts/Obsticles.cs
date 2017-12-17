using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obsticles.
/// this will deal with obsticle collision and movement if needed.
/// </summary>

public class Obsticles: MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D theCollision){
		if (theCollision.gameObject.tag == "P1Sprite" || theCollision.gameObject.tag == "P2Sprite") {
			if (theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity >= 15) {
				theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity -= 1;
			}

		}

		if (theCollision.gameObject.name.Contains ("AI")) {
			if (theCollision.gameObject.GetComponent<BasicAI> ().forwardVelocity >= 15) {
				theCollision.gameObject.GetComponent<BasicAI> ().forwardVelocity -= 1;
			} else {
				theCollision.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 6f);
			}
		}
	}

	void Move(){

	}
}

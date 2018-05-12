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
		if (theCollision.gameObject.tag == "P1Sprite" || theCollision.gameObject.tag == "P2Sprite" || theCollision.gameObject.tag == "P3Sprite" || theCollision.gameObject.tag == "P4Sprite") {
			if (theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity >= 20) {
				theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity -= 0.5f;
			}
        }

		//put back for basic ai and change basic ai
		if (theCollision.gameObject.name.Contains ("AdvancedAI")) {
			//temp
			if (theCollision.gameObject.GetComponent<AdvancedAI> ().forwardVelocity >= 20) {
				theCollision.gameObject.GetComponent<AdvancedAI> ().forwardVelocity -= 1;
			} else {
				theCollision.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 12f);
			}
		}
	}

    void Move(){

	}
}

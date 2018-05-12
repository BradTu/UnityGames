using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBoarderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D theCollision){
		if (theCollision.gameObject.tag == "P1Sprite" || theCollision.gameObject.tag == "P2Sprite" || theCollision.gameObject.tag == "P3Sprite" || theCollision.gameObject.tag == "P4Sprite") {
			if (theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity >= 10) {
				theCollision.gameObject.GetComponentInParent<Player> ().forwardVelocity = 9;
			}

			//clipping fix attempt.
			if (theCollision.gameObject.GetComponentInParent<Player> ().sidewaysVelocity >= 10.5) {
				if (theCollision.gameObject.GetComponentInParent<Player> ().isRightTurning == true) {
					theCollision.gameObject.GetComponentInParent<Player> ().sidewaysVelocity -= 1.5f;
					theCollision.gameObject.GetComponentInParent<Player> ().direction += 5;
					theCollision.gameObject.GetComponentInParent<Player> ().theSprite.transform.rotation = Quaternion.Euler (0f, 0f, theCollision.gameObject.GetComponentInParent<Player> ().direction);
				}
			} else if (theCollision.gameObject.GetComponentInParent<Player> ().sidewaysVelocity <= -10.5) {
				if (theCollision.gameObject.GetComponentInParent<Player> ().isLeftTurning == true) {
					theCollision.gameObject.GetComponentInParent<Player> ().sidewaysVelocity += 1.5f;
					theCollision.gameObject.GetComponentInParent<Player> ().direction -= 5;
					theCollision.gameObject.GetComponentInParent<Player> ().theSprite.transform.rotation = Quaternion.Euler (0f, 0f, theCollision.gameObject.GetComponentInParent<Player> ().direction);
				}
			}

		}
	}
}

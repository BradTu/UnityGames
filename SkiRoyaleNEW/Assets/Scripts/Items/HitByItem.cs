/**
 * This script implements what happens when a player gets hit by a certain item
 * Jason Komoda 10/19/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByItem : MonoBehaviour
{
	/// <summary>
	/// The hit by snowball bool.
	/// </summary>
	public bool hitByProjectile;
    public GameObject cameraParent;
    public CameraController cameraScript;

	// Use this for initialization
	void Start ()
	{
        hitByProjectile = false;
        if (gameObject.tag == "P1Sprite" || gameObject.tag == "P2Sprite" || gameObject.tag == "P3Sprite" || gameObject.tag == "P4Sprite")
        {
            cameraScript = cameraParent.GetComponent<CameraController>();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
		StartCoroutine (hitSetback ());
	}

	//Coroutine is ran when a player gets hit by a snowball from another player
	private IEnumerator hitSetback ()
	{
        if (hitByProjectile) {
			if(gameObject.tag == "P1Sprite" || gameObject.tag == "P2Sprite" || gameObject.tag == "P3Sprite" || gameObject.tag == "P4Sprite")
            {
                cameraScript.startShake(.8f, .3f);
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().sidewaysVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().direction = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				//Here Brad
				gameObject.GetComponent<Animator> ().SetBool ("Fall", true);
                yield return new WaitForSeconds(1.0f);
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = gameObject.transform.parent.gameObject.GetComponent<Player>().startVelocity;
                gameObject.GetComponentInParent<Player>().gotHit = false;
				//Here Brad
				gameObject.GetComponent<Animator> ().SetBool ("Fall", false);
                hitByProjectile = false;
            }
            else
            {
                gameObject.GetComponent<AdvancedAI>().forwardVelocity = 0;
//				gameObject.GetComponent<AdvancedAI>().sidewaysVelocity = 0;
//				gameObject.GetComponent<AdvancedAI>().direction = 0;
//               gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				gameObject.GetComponent<Animator> ().SetBool ("Fall", true);
                yield return new WaitForSeconds(1.0f);
//                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
				gameObject.GetComponent<AdvancedAI>().forwardVelocity = gameObject.GetComponent<AdvancedAI>().startVelocity;
				gameObject.GetComponent<AdvancedAI>().gotHit = false;
				gameObject.GetComponent<Animator> ().SetBool ("Fall", false);
                hitByProjectile = false;
            }
		}
	}
}

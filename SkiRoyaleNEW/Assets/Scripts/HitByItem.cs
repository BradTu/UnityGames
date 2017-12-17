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
	public bool hitBySnowball;
    public bool hitByFollowingSnowball;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		StartCoroutine (hitBySnowballSetback ());
	}

	//Coroutine is ran when a player gets hit by a snowball from another player
	private IEnumerator hitBySnowballSetback ()
	{
		if (hitBySnowball) {
            if(gameObject.tag == "P1Sprite" || gameObject.tag == "P2Sprite")
            {
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().sidewaysVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().direction = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(1.0f);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = gameObject.transform.parent.gameObject.GetComponent<Player>().startVelocity;
            }
            else
            {
                gameObject.GetComponent<BasicAI>().forwardVelocity = 0;
                gameObject.GetComponent<BasicAI>().sidewaysVelocity = 0;
                gameObject.GetComponent<BasicAI>().direction = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(1.0f);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.GetComponent<BasicAI>().forwardVelocity = gameObject.GetComponent<BasicAI>().startVelocity;
            }
			hitBySnowball = false;
		}
        else if (hitByFollowingSnowball){
            if (gameObject.tag == "P1Sprite" || gameObject.tag == "P2Sprite")
            {
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().sidewaysVelocity = 0;
                gameObject.transform.parent.gameObject.GetComponent<Player>().direction = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(1.0f);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.transform.parent.gameObject.GetComponent<Player>().forwardVelocity = gameObject.transform.parent.gameObject.GetComponent<Player>().startVelocity;
            }
            else
            {
                gameObject.GetComponent<BasicAI>().forwardVelocity = 0;
                gameObject.GetComponent<BasicAI>().sidewaysVelocity = 0;
                gameObject.GetComponent<BasicAI>().direction = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(1.0f);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.GetComponent<BasicAI>().forwardVelocity = gameObject.GetComponent<BasicAI>().startVelocity;
            }
            hitByFollowingSnowball = false;
        }
	}
}

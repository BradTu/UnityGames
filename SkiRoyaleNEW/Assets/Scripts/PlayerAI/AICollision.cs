/**
 * This script keeps track of AI collision/triggers
 * Jason Komoda 11/2/17
 */

//parts will need to changed for advanced
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//wait to make it a child for now
public class AICollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when AIs enter sludge triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Sludge"){
            gameObject.GetComponent<BasicAI>().forwardVelocity = 8;
        }

        if (other.gameObject.tag == "Bomb" && gameObject != other.gameObject.GetComponent<ItemMovement>().aiThatUsedItem)
        {
            if (!gameObject.GetComponent<AiItemSystem>().isInvinc)
            {
                other.gameObject.GetComponent<Animator>().SetBool("Explode", true);
                other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                //other.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(HitByBombStall(other.gameObject));

            }
            else
            {
                other.gameObject.GetComponent<Animator>().SetBool("Explode", true);
                other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                //other.gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(ExplosionStall(other.gameObject));
            }
        }
    }

    //when AIs stay in sludge triggers
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Sludge"){
            gameObject.GetComponent<BasicAI>().forwardVelocity = 8;
        }
    }

    private IEnumerator HitByBombStall(GameObject other)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<HitByItem>().hitByProjectile = true;
        other.gameObject.GetComponent<Animator>().SetBool("Explode", true);
        other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
		this.GetComponent<BasicAI> ().health -= 5;
        Destroy(other.gameObject);
    }

    private IEnumerator ExplosionStall(GameObject theBomb)
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(theBomb.gameObject);
    }
}

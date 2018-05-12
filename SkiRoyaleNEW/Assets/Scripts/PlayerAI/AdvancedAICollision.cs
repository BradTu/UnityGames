//made by Joe Yates based on Jason Komoda original from basic ai
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAICollision : MonoBehaviour {
	public float bombHit;
    public float fireHit;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//when AIs enter sludge triggers
	private void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.tag == "Sludge" && !gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc){
			gameObject.GetComponent<AdvancedAI>().forwardVelocity = 8;
		}

		if (other.gameObject.tag == "Bomb" && gameObject != other.gameObject.GetComponent<ItemMovement>().aiThatUsedItem)
		{
			if (!gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc)
			{
				other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
				other.gameObject.GetComponent<AudioSource>().Play();
				StartCoroutine(HitByBombStall(other.gameObject));
			}
			else
			{
				other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
				other.gameObject.GetComponent<AudioSource>().Play();
				StartCoroutine(ExplosionStall(other.gameObject));
			}
		}

        if(other.gameObject.tag == "Fire" && !gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc){
            this.GetComponent<AdvancedAI>().health -= fireHit;
            StartCoroutine(FireBurn());
        }
	}

	//when AIs stay in sludge triggers
	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "Sludge"){
			gameObject.GetComponent<AdvancedAI>().forwardVelocity = 8;
		}
	}

	private IEnumerator HitByBombStall(GameObject other)
	{
        other.gameObject.GetComponent<Animator>().SetBool("Explode", true);
		gameObject.GetComponent<HitByItem>().hitByProjectile = true;
        this.GetComponent<AdvancedAI>().health -= bombHit;
		yield return new WaitForSeconds(0.8f);
        other.gameObject.GetComponent<Animator>().SetBool("Explode", false);
		Destroy(other.gameObject);
	}

	private IEnumerator ExplosionStall(GameObject theBomb)
	{
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", true);
		yield return new WaitForSeconds(0.8f);
        theBomb.gameObject.GetComponent<Animator>().SetBool("Explode", true);
		Destroy(theBomb.gameObject);
	}

    private IEnumerator FireBurn()
    {
        gameObject.transform.GetChild(9).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2);
        gameObject.transform.GetChild(9).GetComponent<ParticleSystem>().Stop();
    }
}

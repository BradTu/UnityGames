/**
 * This script implements the logic of deciding what item the player gets
 * Jason Komoda 10/25/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemBox : MonoBehaviour
{

	/// <summary>
	/// The random item number.
	/// </summary>
	private int randomItemNumber;
	/// <summary>
	/// The chosen item.
	/// </summary>
	public string chosenItem;
    public int lowEnd;
    public int highEnd;

	public GameObject theSystem;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (theSystem.activeSelf == true && theSystem.GetComponent<ParticleSystem> ().emission.rateOverTime.constant != 0) {
			StartCoroutine (TurnOff());
		}
	}

	//Uses RNG to decide what item is chosen
	public void ChooseItem ()
	{
        randomItemNumber = Random.Range (lowEnd, highEnd);
		if (randomItemNumber == 1) {
			chosenItem = "SpeedUp";
        } else if(randomItemNumber == 2) {
			chosenItem = "SlowDown";
		}
        else if(randomItemNumber == 3){
            chosenItem = "FollowingSnowball";
        }
        else if(randomItemNumber == 4 || randomItemNumber == 5){
            chosenItem = "Snowball";
        }
        else{
            chosenItem = "Sludge";
        }
	}

	private IEnumerator TurnOff(){
		yield return new WaitForSeconds(0.5f);
		var em = theSystem.GetComponent<ParticleSystem> ().emission;
		em.rateOverTime = 0;
	}
}

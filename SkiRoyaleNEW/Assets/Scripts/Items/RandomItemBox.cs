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
    public bool setAllItems;
    public string itemToSetTo;

	public bool start;

	// Use this for initialization
	void Start ()
	{
		start = false;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.GetComponent<SpriteRenderer> ().enabled == false && this.GetComponent<BoxCollider2D> ().enabled == false && start == false) {
			StartCoroutine (wait ());
		}
	}

	void OnDisable(){
		
	}

	private IEnumerator wait(){

		start = true;

		yield return new WaitForSeconds (2);

		this.GetComponent<SpriteRenderer> ().enabled = true;

		this.GetComponent<BoxCollider2D> ().enabled = true;

		start = false;

	}

	//Uses RNG to decide what item is chosen
	public void ChooseItem (int place)
	{
        if(place == 1){
            FirstItems();
        }
        else if(place == 2 || place == 3 || place == 4){
            SecondThirdFourthItems();
        }
        else if(place == 5 || place == 6){
            FifthSixthItems();
        }
        else{
            SeventhEightItems();
        }
	}

    private void FirstItems(){
        randomItemNumber = Random.Range(0, 13);
        if(setAllItems){
            chosenItem = itemToSetTo;
        }
        else{
			if (randomItemNumber == 1 || randomItemNumber == 3 || randomItemNumber == 5 || 
				randomItemNumber == 7 || randomItemNumber == 9 || randomItemNumber == 11 || randomItemNumber == 0)
            {
                chosenItem = "Sludge";
            }
			else if (randomItemNumber == 2 || randomItemNumber == 4 || randomItemNumber == 6 || 
				randomItemNumber == 8 || randomItemNumber == 10 || randomItemNumber == 12 || randomItemNumber == 13)
            {
                chosenItem = "Bomb";
            }
        }
    }

    private void SecondThirdFourthItems(){
        randomItemNumber = Random.Range(0, 21);
        if (setAllItems)
        {
            chosenItem = itemToSetTo;
        }
        else
        {
			if (randomItemNumber == 0 || randomItemNumber == 8)
            {
                chosenItem = "SlowDown";
            }
			else if (randomItemNumber == 1 || randomItemNumber == 9 || randomItemNumber == 15)
            {
                chosenItem = "Sludge";
            }
			else if (randomItemNumber == 2 || randomItemNumber == 10 || randomItemNumber == 16)
            {
                chosenItem = "Bomb";
            }
			else if (randomItemNumber == 3 || randomItemNumber == 11 || randomItemNumber == 17 || randomItemNumber == 20)
            {
                chosenItem = "Snowball";
            }
			else if (randomItemNumber == 4 || randomItemNumber == 12)
            {
                chosenItem = "SpeedUp";
            }
			else if (randomItemNumber == 5 || randomItemNumber == 21)
            {
                chosenItem = "Invinc";
            }
			else if (randomItemNumber == 6 || randomItemNumber == 13 || randomItemNumber == 18)
            {
                chosenItem = "Gun";
            }
			else if(randomItemNumber == 7 || randomItemNumber == 14 || randomItemNumber == 19){
                chosenItem = "Molotov";
            }
        }
    }

    private void FifthSixthItems(){
        randomItemNumber = Random.Range(0, 18);
        if (setAllItems)
        {
            chosenItem = itemToSetTo;
        }
        else
        {
			if (randomItemNumber == 0 || randomItemNumber == 7 || randomItemNumber == 12)
            {
                chosenItem = "SlowDown";
            }
			else if (randomItemNumber == 1 || randomItemNumber == 8 || randomItemNumber == 13 || randomItemNumber == 15)
            {
                chosenItem = "Snowball";
            }
			else if (randomItemNumber == 2 || randomItemNumber == 9)
            {
                chosenItem = "SpeedUp";
            }
			else if (randomItemNumber == 3)
            {
                chosenItem = "FollowingSnowball";
            }
			else if (randomItemNumber == 4 || randomItemNumber == 18)
            {
                chosenItem = "Invinc";
            }
			else if (randomItemNumber == 5 || randomItemNumber == 10 || randomItemNumber == 14 || randomItemNumber == 16)
            {
                chosenItem = "Gun";
            }
			else if(randomItemNumber == 6 || randomItemNumber == 11 || randomItemNumber == 17){
                chosenItem = "Molotov";
            }
        }

    }

    private void SeventhEightItems()
    {
        randomItemNumber = Random.Range(0, 16);
        if (setAllItems)
        {
            chosenItem = itemToSetTo;
        }
        else
        {
			if (randomItemNumber == 0 || randomItemNumber == 4 || randomItemNumber == 7 || 
				randomItemNumber == 8 || randomItemNumber == 10 || randomItemNumber == 12)
            {
                chosenItem = "SpeedUp";
            }
			else if (randomItemNumber == 1 || randomItemNumber == 5 || randomItemNumber == 14)
            {
                chosenItem = "FollowingSnowball";
            }
			else if (randomItemNumber == 2 || randomItemNumber == 16)
            {
                chosenItem = "BlueRocket";
            }
			else if (randomItemNumber == 3 || randomItemNumber == 6 || randomItemNumber == 9 || 
					 randomItemNumber == 11 || randomItemNumber == 13 || randomItemNumber == 15)
            {
                chosenItem = "Invinc";
            }
        }
    }
}

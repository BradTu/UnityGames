/**
 * This script keeps the item boxes at the corners of each player's screen
 * Jason Komoda 10/26/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemBoxUIPlacement : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public Text p1UseItemText;
    public Text p2UseItemText;
    public Text p1SlowedText;
    public Text p2SlowedText;
    public GameController gameController;

	public GameObject theSystem;

    // Use this for initialization
    void Start()
    {
        //Get game controller reference
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        SetItemBoxesPlacement();
        SetUseItemTextPlacement();
        SetSlowedTextPlacement();
    }

    // Update is called once per frame
    void Update()
    {
        ShowFirstItemText();
        ShowSlowedText();
		ParticleGo ();
    }

    //Sets item boxes in corner of screen
    void SetItemBoxesPlacement()
    {
        //player1's item box
        if (gameObject.tag == "P1ItemBox")
        {
            gameObject.transform.position = player1.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.07f, .95f, 1));
        }

        //player2's item box
        if (gameObject.tag == "P2ItemBox")
        {
            gameObject.transform.position = player2.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.93f, 0.95f, 1));
        }

    }

    //Sets item box text in middle of screens
    void SetUseItemTextPlacement()
    {
        //player1's item text
        if (gameObject.tag == "P1UseItemText")
        {
            gameObject.transform.position = player1.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.85f, 1));
            gameObject.GetComponent<Text>().enabled = false;
        }

        //player2's item box
        if (gameObject.tag == "P2UseItemText")
        {
            gameObject.transform.position = player2.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.85f, 1));
            gameObject.GetComponent<Text>().enabled = false;
        }
    }

    //Shows item text on first item pickup
    void ShowFirstItemText()
    {
        if (player1.transform.GetChild(0).GetComponent<PlayerCollision>().p1NumberOfPickups == 1 && !player1.transform.GetChild(0).GetComponent<UseItem>().p1JustUsedItem && 
            gameController.player1Slowed){
                
        }

        //show player1's item text
        else if (player1.transform.GetChild(0).GetComponent<PlayerCollision>().p1NumberOfPickups == 1 && !player1.transform.GetChild(0).GetComponent<UseItem>().p1JustUsedItem)
        {
            p1UseItemText.enabled = true;
        }
        else
        {
            p1UseItemText.enabled = false;
        }

        //show player2's item text
        if (player1.transform.GetChild(0).GetComponent<PlayerCollision>().p1NumberOfPickups == 1 && !player1.transform.GetChild(0).GetComponent<UseItem>().p1JustUsedItem &&
            gameController.player2Slowed){

        }
        else if (player2.transform.GetChild(0).GetComponent<PlayerCollision>().p2NumberOfPickups == 1 && !player2.transform.GetChild(0).GetComponent<UseItem>().p2JustUsedItem)
        {
            p2UseItemText.enabled = true;
        }
        else
        {
            p2UseItemText.enabled = false;
        }
    }

    //Put text in middle of screen
    void SetSlowedTextPlacement()
    {

        //player1's slowed text
        p1SlowedText.transform.position = player1.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.87f, 1));
        p1SlowedText.enabled = false;

        //player2's slowed text
        p2SlowedText.transform.position = player2.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.87f, 1));
       // p2SlowedText.enabled = false;
    }

    //Shows slowed text when slow item is activated
    void ShowSlowedText()
    {
        //when player1 is slowed
        if (gameController.player1Slowed)
        {
            p1SlowedText.text = gameController.p1SlowedText.text;
            p1SlowedText.enabled = true;
        }
        else
        {
            p1SlowedText.enabled = false;
        }

        //when player2 is slowed
        if (gameController.player2Slowed)
        {
            p2SlowedText.text = gameController.p2SlowedText.text;
            p2SlowedText.enabled = true;
        }
        else
        {
            p2SlowedText.enabled = false;
        }
    }
	void ParticleGo(){
		if (theSystem == null) {
			return;
		}
		if (this.transform.childCount > 1) {
			var em = theSystem.GetComponent<ParticleSystem> ().emission;
			em.rateOverTime = 100;
		} else {
			var em = theSystem.GetComponent<ParticleSystem> ().emission;
			em.rateOverTime = 0;
		}
	}
}

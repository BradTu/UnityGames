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
    public Text useItemText;
    public Text slowedText;
    public Text placeText;
    public Text deathText;
    public GameObject missleAlert;
    public GameObject invincAnim;
    public GameObject aimingLine;
    public GameController gameController;
	/// <summary>
	/// The input hold delay.
	/// </summary>
	public float inputHoldDelay = 0.001f;

	private WaitForSeconds inputHoldWait;

	//public GameObject theSystem;

	//sound
    // Use this for initialization
    void Start()
    {

		inputHoldWait = new WaitForSeconds (inputHoldDelay);
        //Get game controller reference
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }


		StartCoroutine(SetItemBoxesPlacement());
        useItemText.enabled = false;
        slowedText.enabled = false;
        deathText.enabled = false;
        missleAlert.GetComponent<SpriteRenderer>().enabled = false;
        invincAnim.GetComponent<SpriteRenderer>().enabled = false;
        aimingLine.GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowFirstItemText();
        ShowSlowedText();
        ShowDeathText();
        ShowTargetAlert();
        ShowAimingLine();
        UpdatePlaceText();
    }

    //Sets UI objects positioning on each player's screen
	private IEnumerator SetItemBoxesPlacement ()
	{
		
		yield return inputHoldWait;
		if (gameObject.tag.Contains("ItemBox"))
		{
			gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.07f, .95f, 1));
		}
        if(gameObject.tag.Contains("missleAlert")){
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.93f, .95f, 1));
        }
        if (gameObject.tag.Contains("InvincAnim"))
        {
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.08f, .075f, 1));
        }
        if (gameObject.tag.Contains("SlowedText"))
        {
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, .1f, 1));
        }
        if (gameObject.tag.Contains("UseItemText"))
        {
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, .1f, 1));
        }
        if (gameObject.tag.Contains("DeathText"))
        {
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(1).GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1));
        }
	}


    //Shows item text on first item pickup
    void ShowFirstItemText()
    {
        if(gameObject.tag.Contains("UseItemText")){
            if (gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<PlayerCollision>().numPickups == 1 && gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<UseItem>().hasItem &&
            !gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<UseItem>().playerSlowed)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }

    //Shows slowed text when slow item is activated
    void ShowSlowedText()
    {
        if(gameObject.tag.Contains("SlowedText")){
            if (gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<UseItem>().playerSlowed)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }

    //Shows slowed text when slow item is activated
    void ShowDeathText()
    {
        if (gameObject.tag.Contains("DeathText"))
        {
            if (gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Player>().isDead)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }

    //Shows red alert if targeted by missle
    void ShowTargetAlert(){
        if(gameObject.tag == "missleAlert"){
            if (gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(0).GetComponent<UseItem>().isTargetedByMissle)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    //Shows red line to aim snowball, molotov, and gun
    void ShowAimingLine(){
        if(gameObject.tag == "AimingLine"){
            if(gameObject.transform.parent.transform.parent.GetChild(0).GetComponent<UseItem>().currentItem == "Snowball" ||
               gameObject.transform.parent.transform.parent.GetChild(0).GetComponent<UseItem>().currentItem == "Molotov" ||
               gameObject.transform.parent.transform.parent.GetChild(0).GetComponent<UseItem>().currentItem == "Gun"){
                gameObject.GetComponent<LineRenderer>().enabled = true;
            }
            else{
                gameObject.GetComponent<LineRenderer>().enabled = false;
            }
        }
    }

    //Updates place text
    void UpdatePlaceText(){
        if(gameObject.tag == "PlaceText"){
            gameObject.GetComponent<Text>().text = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player>().currentPlace.ToString();
        }
    }
}

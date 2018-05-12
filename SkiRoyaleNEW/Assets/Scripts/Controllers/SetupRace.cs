///<summary>
///Made by Brad Tully 10 January 2018
///This class will take in the amount of players being used in the game and will
///dynamically change the number of ai vs actual players playing the game before
///the race starts
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRace : MonoBehaviour {
    //Import the playable characters
    public Player playerOne, playerTwo, playerThree, playerFour;
    //Import the AI characters
	//AI
    public AdvancedAI aiOne, aiTwo, aiThree, aiFour, aiFive, aiSix, aiSeven;
    //Get all of the cameras in the game
    public Camera pOneCam, pTwoCam, pThreeCam, pFourCam, finishCam;
    //Number of people playing on splitscreen
    private float splitScreenPlayers;
    //Import the game controller object
    public GameController gameController;

    // Use this for initialization
    void Start () {
        //Find out how many people are playing the game on splitscreen
        splitScreenPlayers = PlayerPrefs.GetFloat("NumberOfPlayers");

        //Set up the game controller object
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        //Find all of the players in the scene and set them to objects
        GameObject playerOneObj = GameObject.FindWithTag("Player1");
        if (playerOneObj != null)
        {
            playerOne = playerOneObj.GetComponent<Player>();
            playerOne.theName = "Player One";
        }
        
        GameObject playerTwoObj = GameObject.FindWithTag("Player2");
        if (playerTwoObj != null)
        {
            playerTwo = playerTwoObj.GetComponent<Player>();
            playerTwo.theName = "Player Two";
        }

        GameObject playerThreeObj = GameObject.FindWithTag("Player3");
        if (playerThreeObj != null)
        {
            playerThree = playerThreeObj.GetComponent<Player>();
            playerThree.theName = "Player Three";
        }

        GameObject playerFourObj = GameObject.FindWithTag("Player4");
        if (playerFourObj != null)
        {
            playerFour = playerFourObj.GetComponent<Player>();
            playerFour.theName = "Player Four";
        }

        //Find all of the ai in the scene and set them to game objects
        GameObject aiOneObj = GameObject.FindWithTag("AdvancedAI1");
        if (aiOneObj != null)
        {
			aiOne = aiOneObj.GetComponent<AdvancedAI>();
			aiOne.theName = "AdvancedAI1";
        }
		GameObject aiTwoObj = GameObject.FindWithTag("AdvancedAI2");
        if (aiTwoObj != null)
        {
			aiTwo = aiTwoObj.GetComponent<AdvancedAI>();
			aiTwo.theName = "AdvancedAI2";
        }
		GameObject aiThreeObj = GameObject.FindWithTag("AdvancedAI3");
        if (aiThreeObj != null)
        {
			aiThree = aiThreeObj.GetComponent<AdvancedAI>();
			aiThree.theName = "AdvancedAI3";
        }
		GameObject aiFourObj = GameObject.FindWithTag("AdvancedAI4");
        if (aiFourObj != null)
        {
			aiFour = aiFourObj.GetComponent<AdvancedAI>();
			aiFour.theName = "AdvancedAI4";
        }
		GameObject aiFiveObj = GameObject.FindWithTag("AdvancedAI5");
        if (aiFiveObj != null)
        {
			aiFive = aiFiveObj.GetComponent<AdvancedAI>();
			aiFive.theName = "AdvancedAI5";
        }
		GameObject aiSixObj = GameObject.FindWithTag("AdvancedAI6");
        if (aiSixObj != null)
        {
			aiSix = aiSixObj.GetComponent<AdvancedAI>();
			aiSix.theName = "AdvancedAI6";
        }
		GameObject aiSevenObj = GameObject.FindWithTag("AdvancedAI7");
        if (aiSevenObj != null)
        {
			aiSeven = aiSevenObj.GetComponent<AdvancedAI>();
			aiSeven.theName = "AdvancedAI7";
        }

        //Load all of the cameras into objects to be edited later if needed
        GameObject pOneCamObj = GameObject.FindWithTag("P1Camera");
        if (pOneCamObj != null)
        {
            pOneCam = pOneCamObj.GetComponent<Camera>();
        }

        GameObject pTwoCamObj = GameObject.FindWithTag("P2Camera");
        if (pTwoCamObj != null)
        {
            pTwoCam = pTwoCamObj.GetComponent<Camera>();
        }

        GameObject pThreeCamObj = GameObject.FindWithTag("P3Camera");
        if (pThreeCamObj != null)
        {
            pThreeCam = pThreeCamObj.GetComponent<Camera>();
        }

        GameObject pFourCamObj = GameObject.FindWithTag("P4Camera");
        if (pFourCamObj != null)
        {
            pFourCam = pFourCamObj.GetComponent<Camera>();
        }

        GameObject finishCamObj = GameObject.FindWithTag("FinishCam");
        if (finishCamObj != null)
        {
            finishCam = finishCamObj.GetComponent<Camera>();
        }

        //If one person is playing destroy unnecessary characters, remove them from the list and resize player one's camera
        if (splitScreenPlayers == 1.0f)
        {
            Destroy(playerTwoObj);
            Destroy(playerThreeObj);
            Destroy(playerFourObj);
            Destroy(finishCamObj);
            gameController.activePlayers.RemoveAt(1);
            gameController.activePlayers.RemoveAt(1);
            gameController.activePlayers.RemoveAt(1);
            gameController.currentPlayers.RemoveAt(1);
            gameController.currentPlayers.RemoveAt(1);
            gameController.currentPlayers.RemoveAt(1);
            pOneCam.rect = new Rect(0f, 0f, 1f, 1f);
        }
        //If two people are playing destroy unnecessary characters, remove them from the list and resize/ place the cameras for two
        else if (splitScreenPlayers == 2.0f)
        {
            Destroy(playerThreeObj);
            Destroy(playerFourObj);
            Destroy(aiFiveObj);
            Destroy(finishCamObj);
            gameController.activePlayers.RemoveAt(2);
            gameController.activePlayers.RemoveAt(2);
            gameController.activePlayers.RemoveAt(6);
            gameController.currentPlayers.RemoveAt(2);
            gameController.currentPlayers.RemoveAt(2);
            gameController.currentPlayers.RemoveAt(6);
            pOneCam.rect = new Rect(0f, 0f, 0.5f, 1f);
            pTwoCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
        //If three people are playing remove unnecesarry characters, remove them from the list, resize/ place cameras
        else if (splitScreenPlayers == 3.0f)
        {
            Destroy(playerFourObj);
            Destroy(aiFiveObj);
            Destroy(aiSixObj);
            gameController.activePlayers.RemoveAt(3);
            gameController.activePlayers.RemoveAt(7);
            gameController.activePlayers.RemoveAt(7);
            gameController.currentPlayers.RemoveAt(3);
            gameController.currentPlayers.RemoveAt(7);
            gameController.currentPlayers.RemoveAt(7);
            pOneCam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            pTwoCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            pThreeCam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        }
        //If four people are playing destroy unnecessary ai, remove them from the list, and resize/ place the cameras
        else if (splitScreenPlayers == 4.0f)
        {
            Destroy(aiFiveObj);
            Destroy(aiSixObj);
            Destroy(aiSevenObj);
            Destroy(finishCamObj);
            gameController.activePlayers.RemoveAt(8);
            gameController.activePlayers.RemoveAt(8);
            gameController.activePlayers.RemoveAt(8);
            gameController.currentPlayers.RemoveAt(8);
            gameController.currentPlayers.RemoveAt(8);
            gameController.currentPlayers.RemoveAt(8);
            pOneCam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            pTwoCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            pThreeCam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            pFourCam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        }

        Debug.Log("setup race");
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

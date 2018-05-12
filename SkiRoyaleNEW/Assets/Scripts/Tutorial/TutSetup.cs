///<summary>
///Made by Brad Tully 19 April 2018
///This script will load the tutorials with the correct number of players and 
///split up the screen based off of that.
///</summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutSetup : MonoBehaviour {
    //Import the playable characters
    public Player playerOne, playerTwo, playerThree, playerFour;
    //Import cameras
    public Camera pOneCam, pTwoCam, pThreeCam, pFourCam, finishCam;
    //Number of people playing on splitscreen
    private float splitScreenPlayers;
    //Active players list
    public List<Player> activePlayers;

    
    void Start () {
        //Find out how many people are playing the game on splitscreen
        splitScreenPlayers = PlayerPrefs.GetFloat("NumberOfPlayers");

        //Find all of the players in the scene and set them to objects
        GameObject playerOneObj = GameObject.FindWithTag("Player1");
        if (playerOneObj != null)
        {
            playerOne = playerOneObj.GetComponent<Player>();
            playerOne.theName = "Player One";
            activePlayers.Add(playerOne);
        }

        GameObject playerTwoObj = GameObject.FindWithTag("Player2");
        if (playerTwoObj != null)
        {
            playerTwo = playerTwoObj.GetComponent<Player>();
            playerTwo.theName = "Player Two";
            activePlayers.Add(playerTwo);
        }

        GameObject playerThreeObj = GameObject.FindWithTag("Player3");
        if (playerThreeObj != null)
        {
            playerThree = playerThreeObj.GetComponent<Player>();
            playerThree.theName = "Player Three";
            activePlayers.Add(playerThree);
        }

        GameObject playerFourObj = GameObject.FindWithTag("Player4");
        if (playerFourObj != null)
        {
            playerFour = playerFourObj.GetComponent<Player>();
            playerFour.theName = "Player Four";
            activePlayers.Add(playerFour);
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
            activePlayers.RemoveAt(1);
            activePlayers.RemoveAt(1);
            activePlayers.RemoveAt(1);
            pOneCam.rect = new Rect(0f, 0f, 1f, 1f);
        }
        //If two people are playing destroy unnecessary characters, remove them from the list and resize/ place the cameras for two
        else if (splitScreenPlayers == 2.0f)
        {
            Destroy(playerThreeObj);
            Destroy(playerFourObj);
            Destroy(finishCamObj);
            activePlayers.RemoveAt(2);
            activePlayers.RemoveAt(2);
            pOneCam.rect = new Rect(0f, 0f, 0.5f, 1f);
            pTwoCam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
        //If three people are playing remove unnecesarry characters, remove them from the list, resize/ place cameras
        else if (splitScreenPlayers == 3.0f)
        {
            Destroy(playerFourObj);
            activePlayers.RemoveAt(3);
            pOneCam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            pTwoCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            pThreeCam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        }
        //If four people are playing resize/ place the cameras
        else if (splitScreenPlayers == 4.0f)
        {
            Destroy(finishCamObj);
            pOneCam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            pTwoCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            pThreeCam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
            pFourCam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        }
    }
	

}

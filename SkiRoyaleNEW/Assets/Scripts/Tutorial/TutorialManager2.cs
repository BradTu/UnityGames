///<summary>
///Made by Brad Tully 
///This controls the second tutorial. In this one the player learns how to 
///properly use the hard turn. They must successfully complete a left and right
///hard turn to pass the tutorial.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager2 : MonoBehaviour {

    public Player playerOne, playerTwo, playerThree, playerFour;
    public Text theText;
    public Image top, bottom;
    int inc;
    public bool gameOver;
    int count;
    public string thisScene, nextScene;

    //Initialize player, text and other values.
    void Start () {
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

        theText.text = "Any turn that has arrows will require you to use the hard turn. "
                        + "The hard turn is performed with the Bumpers. It will slow you down a lot more than " +
                        "a regular turn, but will give you a boost at the end. Try to follow the arrows. To start the tutorial press 'A'";
        inc = 0;
        count = 0;
        Time.timeScale = 1f;
        gameOver = false;
    }
	
	//Check the game state
	void Update () {
        //Remove the text when the level begins
        if (playerOne.begin == true && inc == 0)
        {
            theText.text = "";
            Destroy(bottom);
            inc++;
        }
        //When game is over load next scene
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(nextScene);
        }
        if (Input.GetButtonDown("P1UseItem"))
        {
            playerOne.begin = true;
        }
        if (Input.GetButtonDown("P2UseItem"))
        {
            playerTwo.begin = true;
        }
        if (Input.GetButtonDown("P3UseItem"))
        {
            playerThree.begin = true;
        }
        if (Input.GetButtonDown("P4UseItem"))
        {
            playerFour.begin = true;
        }
        if (Input.GetButtonDown("P1Back"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

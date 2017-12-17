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

    Player playerOne;
    public Text theText;
    int inc;
    public bool gameOver;
    int count;

    //Initialize player, text and other values.
    void Start () {
        GameObject playerOneObj = GameObject.FindWithTag("Player1");
        if (playerOneObj != null)
        {
            playerOne = playerOneObj.GetComponent<Player>();
            playerOne.theName = "Player One";
        }
        theText.text = "The hard turn will be useful in instances where you need to turn quickly and at a greater angle. "
                        + "The hard turn is performed with the Bumpers. Be warned it will slow you down a lot more than " +
                        "a regular turn. To start the tutorial press 'A'";
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
            inc++;
        }
        //When game is over load next scene
        if (gameOver == true && Input.GetButtonDown("P1UseItem"))
        {
            SceneManager.LoadScene("Tutorial3");
        }
        //Restart scene if player presses b
        if (Input.GetButtonDown("P1Brake"))
        {
            SceneManager.LoadScene("Tutorial2");
        }
        Debug.Log(count);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Slow down time when the player enters trigger zones to let them know
        //when to use the hard turn
        if (collision.gameObject.tag == "P1Sprite" && count == 0)
        {
            theText.text = "HOLD DOWN THE LEFT BUMPER!";
            Time.timeScale = .1f;
            count++;
        }
        else if (collision.gameObject.tag == "P1Sprite")
        {
            theText.text = "HOLD DOWN THE RIGHT BUMPER!";
            Time.timeScale = .1f;
            count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Return time to normal when they exit the trigger zone
        if (collision.gameObject.tag == "P1Sprite")
        {
            Time.timeScale = 1f;
            //theText.text = "";
        }
    }
}

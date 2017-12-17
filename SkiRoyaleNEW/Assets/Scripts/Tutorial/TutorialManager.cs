///<summary>
///Made by Brad Tully
///This controls the first tutorial of the game.
///The player must knock both ai in the scene into the trees to succeed and move on.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    Player playerOne;
    public BasicAI aiOne, aiTwo;
    public float velocity;
    public Text theText;
    public bool gameOver;
    int increment;

    // Fetch all of the game objects, and set some values
    void Start () {
        GameObject playerOneObj = GameObject.FindWithTag("Player1");
        if (playerOneObj != null)
        {
            playerOne = playerOneObj.GetComponent<Player>();
            playerOne.theName = "Player One";
        }
        GameObject aiOneObj = GameObject.Find("AI Tut");
        if (aiOneObj != null)
        {
            aiOne = aiOneObj.GetComponent<BasicAI>();
            aiOne.theName = "AI 1";
        }
        GameObject aiTwoObj = GameObject.Find("AI Tut (1)");
        if (aiTwoObj != null)
        {
            aiTwo = aiTwoObj.GetComponent<BasicAI>();
            aiTwo.theName = "AI 2";
        }
        playerOne.begin = true;
        aiOne.begin = true;
        aiTwo.begin = true;
        playerOne.StopCoroutine("SpeedIncrease");
        velocity = -5f;
        gameOver = false;

        theText.text = "Move side to side using the left joystick. " +
                  "You can hit players into objects and the border to slow them down using your poles by pressing the trigger buttons." +
                  " Try hitting the AI using the trigger buttons. To start press 'A'. To restart press 'B'.";
    }
	
	//Make sure the player doesn't move forward
	void Update () {
        playerOne.forwardVelocity = 0;
        //If both ai are hit off screen the tutorial is over
        if (aiOne.gameObject.transform.position.y <= -4 && aiTwo.gameObject.transform.position.y <= -4)
        {
            theText.text = "Good job! Press 'Start' to move to the next tutorial or 'B' to redo this one";
            gameOver = true;
        }
        //Reload tutorial
        if (Input.GetButtonDown("P1Brake"))
        {
            SceneManager.LoadScene("Tutorial1");
        }
        //Load next tutorial
        if (gameOver == true && Input.GetButtonDown("Jump"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("Tutorial2");
            }
        }
        
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        //When the ai hits the trees knock them back
        if (collision.gameObject.name == "AI Tut")
        {
            collision.gameObject.transform.Translate(transform.up * (velocity * Time.deltaTime), Space.World);
        }
        else if (collision.gameObject.name == "AI Tut (1)")
        {
            collision.gameObject.transform.Translate(transform.up * (velocity * Time.deltaTime), Space.World);
        }
    }
}

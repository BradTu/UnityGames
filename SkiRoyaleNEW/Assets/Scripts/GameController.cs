///<summary>
///Brad Tully 18 October 2017
///This class controlls the rules of the game. ie win/loss states etc
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	/// <summary>
	/// All of the players
	/// </summary>
	Player playerOne, playerTwo, playerThree, playerFour;

	public BasicAI aiOne, aiTwo, aiThree, aiFour, aiFive, aiSix;
	/// <summary>
	/// number of players and gets the number of players that have finished
	/// </summary>
	private int numPlayers, playersPastFinishLine;
	/// <summary>
	/// the is all for the score board.
	/// </summary>
	public Text first, second, third, fourth, fifth, scoreboard, restart, countdown;
	/// <summary>
	/// The placement of the players.
	/// </summary>
	public List<Player> placement;
	/// <summary>
	/// The p1 has item.
	/// </summary>
	public bool p1HasItem;
	/// <summary>
	/// The p2 has item.
	/// </summary>
	public bool p2HasItem;
	/// <summary>
	/// The p1 current item.
	/// </summary>
	public GameObject p1CurrentItem;
	/// <summary>
	/// The p2 current item.
	/// </summary>
	public GameObject p2CurrentItem;
    //Used for countdown
    private int count;
    bool gameOver;
    int[] finishIncrement;

    //public List<string> thePlacement;

	public float playerPlaceY;

    public Text p1SlowedText;
    public Text p2SlowedText;
    public bool player1Slowed;
    public bool player2Slowed;
    public bool finishedCountdown;


    // Use this for initialization
	void Start ()
	{
		//Sets the frame rate to 60
		Application.targetFrameRate = 60;
		numPlayers = 0;
		playersPastFinishLine = 0;
		//how the controller gets player one
		GameObject playerOneObj = GameObject.FindWithTag ("Player1");
		if (playerOneObj != null) {
			playerOne = playerOneObj.GetComponent<Player> ();
			playerOne.theName = "Player One";
			numPlayers++;
		}
		//how the controller gets player two
		GameObject playerTwoObj = GameObject.FindWithTag ("Player2");
		if (playerTwoObj != null) {
			playerTwo = playerTwoObj.GetComponent<Player> ();
			playerTwo.theName = "Player Two";
			numPlayers++;
		}

		GameObject aiOneObj = GameObject.FindWithTag("AI 1");
		if (aiOneObj != null) {
			aiOne = aiOneObj.GetComponent<BasicAI> ();
            aiOne.theName = "AI 1";
            numPlayers++;
		}

		GameObject aiTwoObj = GameObject.FindWithTag("AI 2");
		if (aiTwoObj != null) {
			aiTwo = aiTwoObj.GetComponent<BasicAI> ();
            aiTwo.theName = "AI 2";
            numPlayers++;
        }

		GameObject aiThreeObj = GameObject.FindWithTag("AI 3");
		if (aiThreeObj != null) {
			aiThree = aiThreeObj.GetComponent<BasicAI> ();
            aiThree.theName = "AI 3";
            numPlayers++;
        }

        GameObject aiFourObj = GameObject.FindWithTag("AI 4");
		if (aiFourObj != null) {
			aiFour = aiFourObj.GetComponent<BasicAI> ();
            aiFour.theName = "AI 4";
            numPlayers++;
        }

		GameObject aiFiveObj = GameObject.FindWithTag("AI 5");
		if (aiFiveObj != null) {
			aiFive = aiFiveObj.GetComponent<BasicAI> ();
            aiFive.theName = "AI 5";
            numPlayers++;
        }

		GameObject aiSixObj = GameObject.FindWithTag("AI 6");
		if (aiSixObj != null) {
			aiSix = aiSixObj.GetComponent<BasicAI> ();
            aiSix.theName = "AI 6";
            numPlayers++;
        }

        //how the controller gets player three
        //		GameObject playerThreeObj = GameObject.FindWithTag ("Player3");
        //		if (playerThreeObj != null) {
        //			playerThree = playerThreeObj.GetComponent<Player> ();
        //			playerThree.name = "Player Three";
        //			numPlayers++;
        //		}
        //		//how the controller gets player four
        //		GameObject playerFourObj = GameObject.FindWithTag ("Player4");
        //		if (playerFourObj != null) {
        //			playerFour = playerFourObj.GetComponent<Player> ();
        //			playerFour.name = "Player Four";
        //			numPlayers++;
        //		}

        placement = new List<Player>();
        placement.Capacity = numPlayers;
        //thePlacement.Capacity = numPlayers;

        count = 3;
        StartCoroutine("Countdown");

        gameOver = false;

        finishIncrement = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (gameOver == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("Ski_Race1");
            }
        }
		FirstPlace ();
	}

	/// <summary>
	/// this is fixed update and it is used for the physics update
	/// </summary>
	private void FixedUpdate ()
	{
		endRace (playersPastFinishLine);
	}

    //------------------------------------------------TEMPORARY-------------------------------------------------------------------------
    //Temporary end race mehtod will put the strings of the finishers of top four in order
    /*public void endRace(int finishers)
    {
        if (playersPastFinishLine == numPlayers)
        {
            for (int i = 0; i < thePlacement.Count; i++)
            {
                if (i == 0)
                {
                    first.text = "1) " + thePlacement[i];
                }
                else if (i == 1)
                {
                    second.text = "2) " + thePlacement[i];
                }
                else if (i == 2)
                {
                    third.text = "3) " + thePlacement[i];
                }
                else if (i == 3)
                {
                    fourth.text = "4) " + thePlacement[i];
                }
                else if (i == 4)
                {
                    fifth.text = "5) " + thePlacement[i];
                }
            }
            scoreboard.text = "Scoreboard";
            restart.text = "Press the Start Button or Space to Restart";
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(2);
            }
        }
    }*/
    //------------------------------------------------TEMPORARY-------------------------------------------------------------------------

    //Ends the race when players pass the finish line (a trigger
    public void endRace (int finishers)
	{
		if (playersPastFinishLine == numPlayers) {
			for (int i = 0; i < placement.Count; i++) {
				if (i == 0) {
					first.text = "1) '" + placement[i].theName + "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds;
				} else if (i == 1) {
					second.text = "2) '" + placement[i].theName + "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds;
				} else if (i == 2) {
					third.text = "3) '" + placement[i].theName + "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds;
				} else if (i == 3) {
					fourth.text = "4) '" + placement[i].theName + "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds;
				} else if (i == 4) {
                    fifth.text = "5) '" + placement[i].theName + "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds;
                }
            }
            gameOver = true;
			scoreboard.text = "Scoreboard";
            restart.text = "Press the Start Button or Space to Restart";
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(2); 
            }
		}
	}

    //This has a three second countdown for the beginning of the race
    private IEnumerator Countdown()
    {
        while (true)
        {
            //Make sure none of the players/ ai update update countdown text
            playerOne.begin = false;
            playerTwo.begin = false;
            aiOne.begin = false;
            aiTwo.begin = false;
            aiThree.begin = false;
            aiFour.begin = false;
            aiFive.begin = false;
            aiSix.begin = false;
            countdown.text = count.ToString();
            count--;
            //Start the race end the coroutine
            if (count == -1)
            {
                countdown.text = "Go!";
                playerOne.begin = true;
                playerTwo.begin = true;
                aiOne.begin = true;
                aiTwo.begin = true;
                aiThree.begin = true;
                aiFour.begin = true;
                aiFive.begin = true;
                aiSix.begin = true;
                countdown.text = "";
                playerOne.StartCoroutine("timer", false);
                playerTwo.StartCoroutine("timer", false);
                aiOne.StartCoroutine("timer", false);
                aiTwo.StartCoroutine("timer", false);
                aiThree.StartCoroutine("timer", false);
               	aiFour.StartCoroutine("timer", false);
                aiFive.StartCoroutine("timer", false);
                aiSix.StartCoroutine("timer", false);
                break;
            }
            yield return new WaitForSeconds(1.05f);
        }
        yield return new WaitForSeconds(1f);
        finishedCountdown = true;
    }

	//See who passes the finish line
	private void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "P1Sprite" && finishIncrement[0] == 0) {
			playerOne.finished = true;
			playersPastFinishLine++;
			//playerOne.place = playersPastFinishLine;
			placement.Add(playerOne);
            //thePlacement.Add(playerOne.theName);
            finishIncrement[0] = 1;
        } else if (collision.gameObject.tag == "P2Sprite" && finishIncrement[1] == 0) {
			playerTwo.finished = true;
			playersPastFinishLine++;
			//playerTwo.place = playersPastFinishLine;
			placement.Add(playerTwo);
            //thePlacement.Add(playerTwo.theName);
            finishIncrement[1] = 1;
        } /* else if (collision.gameObject.tag == "P3Sprite") {
			playerThree.finished = true;
			playersPastFinishLine++;
			//playerThree.place = playersPastFinishLine;
			placement.Add (playerThree);
            //thePlacement.Add(playerThree.theName);
        } else if (collision.gameObject.tag == "P4Sprite") {
			playerFour.finished = true;
			playersPastFinishLine++;
			//playerFour.place = playersPastFinishLine;
			placement.Add (playerFour);
            //thePlacement.Add(playerFour.theName);
        }*/
        //------------------------------------------------TEMPORARY-------------------------------------------------------------------------
        else if (collision.gameObject.tag == "AI 1" && finishIncrement[2] == 0)
        {
            aiOne.finished = true;
            aiOne.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiOne);
            //thePlacement.Add(aiOne.theName);
            finishIncrement[2] = 1;
        }
        else if (collision.gameObject.tag == "AI 2" && finishIncrement[3] == 0)
        {
            aiTwo.finished = true;
            aiTwo.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiTwo);
            //thePlacement.Add(aiTwo.theName);
            finishIncrement[3] = 1;
        }
        else if (collision.gameObject.tag == "AI 3" && finishIncrement[4] == 0)
        {
            aiThree.finished = true;
            aiThree.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiThree);
            //thePlacement.Add(aiThree.theName);
            finishIncrement[4] = 1;
        }
        else if (collision.gameObject.tag == "AI 4" && finishIncrement[5] == 0)
        {
            aiFour.finished = true;
            aiFour.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiFour);
            //thePlacement.Add(aiFour.theName);
            finishIncrement[5] = 1;
        }
        else if (collision.gameObject.tag == "AI 5" && finishIncrement[6] == 0)
        {
            aiFive.finished = true;
            aiFive.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiFive);
            //thePlacement.Add(aiFive.theName);
            finishIncrement[6] = 1;
        }
        else if (collision.gameObject.tag == "AI 6" && finishIncrement[7] == 0)
        {
            aiSix.finished = true;
            aiSix.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiSix);
            //thePlacement.Add(aiSix.theName);
            finishIncrement[7] = 1;
        }
        //------------------------------------------------TEMPORARY-------------------------------------------------------------------------
    }

    //needs cases for if some players are and are not in game.
    void FirstPlace(){
		playerPlaceY = Mathf.Max (playerOne.transform.position.y, playerTwo.transform.position.y, aiOne.transform.position.y, aiTwo.transform.position.y, aiThree.transform.position.y, aiFour.transform.position.y, aiFive.transform.position.y, aiSix.transform.position.y);
	}

}

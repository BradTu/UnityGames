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
	/// All of the players
	Player playerOne, playerTwo, playerThree, playerFour;
	// AI
	public AdvancedAI aiOne, aiTwo, aiThree, aiFour, aiFive, aiSix, aiSeven;
	/// number of players and gets the number of players that have finished
	public int numPlayers, playersPastFinishLine, deadPlayersNum;
	/// the is all for the score board.
	public Text first, second, third, fourth, fifth, scoreboard, restart, countdown;
	/// The placement of the players.
	public List<Player> placement, currentPlayers, deadPlayers;
    // List of active players/ ai 
    public List<GameObject> activePlayers;
	/// The p1 has item.
	public bool p1HasItem;
	/// The p2 has item.
	public bool p2HasItem;
	/// The p1 current item.>
	public GameObject p1CurrentItem;
	/// The p2 current item.
	public GameObject p2CurrentItem;
    //Used for countdown
    private int count;
    public bool gameOver;
    int[] finishIncrement;
	public float playerPlaceY;
    public Text slowedText;
    public bool playerSlowed;
    public bool finishedCountdown;
    //Used for loading this scene
    public string sceneName, nextRace;
    //Used to move the players/ai towards a final point
    public Vector3[] finishSpots;
    //Used to put up scoreboard when all players pass finish line
    int numHumans;
    public List<GameObject> placementList;
    private bool copiedPlayerList;
    public bool isGrandPrix, isTimeTrial;
    private GrandPrix grandPrix;
    public Transform canvasBack;


    // Use this for initialization
	void Start ()
	{
		//Sets the frame rate to 60
		//Application.targetFrameRate = 60;
		numPlayers = 8;
		playersPastFinishLine = 0;
        numHumans = 0;
        //how the controller gets player one
        GameObject playerOneObj = GameObject.FindWithTag ("Player1");
		if (playerOneObj != null) {
			playerOne = playerOneObj.GetComponent<Player> ();
			playerOne.theName = "Player One";
            activePlayers.Add(playerOneObj.transform.GetChild(0).gameObject);
            currentPlayers.Add(playerOne);
		}
		//how the controller gets player two
		GameObject playerTwoObj = GameObject.FindWithTag ("Player2");
		if (playerTwoObj != null) {
			playerTwo = playerTwoObj.GetComponent<Player> ();
			playerTwo.theName = "Player Two";
            activePlayers.Add(playerTwoObj.transform.GetChild(0).gameObject);
            currentPlayers.Add(playerTwo);
        }

        GameObject playerThreeObj = GameObject.FindWithTag("Player3");
        if (playerThreeObj != null)
        {
            playerThree = playerThreeObj.GetComponent<Player>();
            playerThree.theName = "Player Three";
            activePlayers.Add(playerThreeObj.transform.GetChild(0).gameObject);
            currentPlayers.Add(playerThree);
        }

        GameObject playerFourObj = GameObject.FindWithTag("Player4");
        if (playerFourObj != null)
        {
            playerFour = playerFourObj.GetComponent<Player>();
            playerFour.theName = "Player Four";
            activePlayers.Add(playerFour.transform.GetChild(0).gameObject);
            currentPlayers.Add(playerFour);
        }
        GameObject aiOneObj = GameObject.FindWithTag("AdvancedAI1");
		if (aiOneObj != null) {
			aiOne = aiOneObj.GetComponent<AdvancedAI> ();
			aiOne.theName = "AdvancedAI1";
            activePlayers.Add(aiOneObj);
            currentPlayers.Add(aiOne);
        }
		GameObject aiTwoObj = GameObject.FindWithTag("AdvancedAI2");
		if (aiTwoObj != null) {
			aiTwo = aiTwoObj.GetComponent<AdvancedAI> ();
			aiTwo.theName = "AdvancedAI2";
            activePlayers.Add(aiTwoObj);
            currentPlayers.Add(aiTwo);
        }
		GameObject aiThreeObj = GameObject.FindWithTag("AdvancedAI3");
		if (aiThreeObj != null) {
			aiThree = aiThreeObj.GetComponent<AdvancedAI> ();
			aiThree.theName = "AdvancedAI3";
            activePlayers.Add(aiThreeObj);
            currentPlayers.Add(aiThree);
        }
		GameObject aiFourObj = GameObject.FindWithTag("AdvancedAI4");
		if (aiFourObj != null) {
			aiFour = aiFourObj.GetComponent<AdvancedAI> ();
			aiFour.theName = "AdvancedAI4";
            activePlayers.Add(aiFourObj);
            currentPlayers.Add(aiFour);
        }
		GameObject aiFiveObj = GameObject.FindWithTag("AdvancedAI5");
		if (aiFiveObj != null) {
			aiFive = aiFiveObj.GetComponent<AdvancedAI> ();
			aiFive.theName = "AdvancedAI5";
            activePlayers.Add(aiFiveObj);
            currentPlayers.Add(aiFive);
        }
		GameObject aiSixObj = GameObject.FindWithTag("AdvancedAI6");
		if (aiSixObj != null) {
			aiSix = aiSixObj.GetComponent<AdvancedAI> ();
			aiSix.theName = "AdvancedAI6";
            activePlayers.Add(aiSixObj);
            currentPlayers.Add(aiSix);
        }
		GameObject aiSevenObj = GameObject.FindWithTag("AdvancedAI7");
        if (aiSevenObj != null){
            aiSeven = aiSevenObj.GetComponent<AdvancedAI>();
			aiSeven.theName = "AdvancedAI7";
            activePlayers.Add(aiSevenObj);
            currentPlayers.Add(aiSeven);
        }
        placementList = activePlayers;
        placement = new List<Player>();
        placement.Capacity = numPlayers;

        count = 3;
        StartCoroutine("Countdown");

        gameOver = false;

        finishIncrement = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        if (PlayerPrefs.GetInt("GrandPrixMode") == 1)
        {
            isGrandPrix = true;
            GameObject grandPrixObj = GameObject.FindWithTag("GrandPrix");
            if (grandPrixObj != null)
            {
                grandPrix = grandPrixObj.GetComponent<GrandPrix>();
            }
        }
        else
        {
            isGrandPrix = false;
            GameObject grandPrixObj = GameObject.FindWithTag("GrandPrix");
            if (grandPrixObj != null)
            {
                grandPrix = grandPrixObj.GetComponent<GrandPrix>();
                grandPrix.DestroyThisObject();
            }
        }

        if (PlayerPrefs.GetInt("TimeTrialMode") == 1)
        {
            isTimeTrial = true;
        }
        else
        {
            isTimeTrial = false;
            GameObject ttObj = GameObject.FindWithTag("TimeTrial");
            if (ttObj != null)
            {
                Destroy(ttObj);
            }
        }

        Debug.Log("game controller");

		if (deadPlayers.Count > 0) {
			deadPlayers.Clear();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
        copiedPlayerList = true;
        if (gameOver == true && isGrandPrix == false)
        {
            if (Input.GetButtonDown("P1UseItem"))
            {
                SceneManager.LoadScene(sceneName);
            }
            else if (Input.GetButtonDown("P1Brake"))
            {
                SceneManager.LoadScene("RaceMenu");
            }
        }
        else if (gameOver == true && isGrandPrix == true)
        {
            if (Input.GetButtonDown("P1UseItem"))
            {
                grandPrix.UpdateScore();
                SceneManager.LoadScene(nextRace);
            }
        }
        //Debug.Log("Framerate: " + (1.0f / Time.deltaTime));
	}

	/// <summary>
	/// this is fixed update and it is used for the physics update
	/// </summary>
	private void FixedUpdate ()
	{
        PlacementAlgorithm();	
        endRace (playersPastFinishLine);
	}

    //Ends the race when players pass the finish line (a trigger
    public void endRace (int finishers)
	{
		if ((playersPastFinishLine == numPlayers || numHumans == PlayerPrefs.GetFloat("NumberOfPlayers") || (playersPastFinishLine + deadPlayersNum) == numPlayers) && isGrandPrix == false) {

			if (activePlayers.Count > 0) {
				for (int i = 0; i < activePlayers.Count; i++) {
					activePlayers [i].GetComponent<AdvancedAI> ().Stop ();
					placement.Add (activePlayers [i].gameObject.GetComponent<AdvancedAI>());
				}
				activePlayers.Clear ();
			}
			activePlayers.Clear ();
			if(deadPlayers.Count != 0){
				deadPlayers.Reverse ();
				for (int i = 0; i < deadPlayers.Count; i++) {
					placement.Add (deadPlayers [i]);
					deadPlayers.Remove (deadPlayers [i]);
				}
			}
			for (int i = 0; i < placement.Count; i++) {
                canvasBack.gameObject.SetActive(true);
                if (i == 0) {
					first.text = "1) '" + placement[i].theName /*+ "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds*/;
				} else if (i == 1) {
					second.text = "2) '" + placement[i].theName /*+ "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds*/;
                } else if (i == 2) {
					third.text = "3) '" + placement[i].theName /*+ "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds*/;
                } else if (i == 3) {
					fourth.text = "4) '" + placement[i].theName /*+ "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds*/;
                } else if (i == 4) {
                    fifth.text = "5) '" + placement[i].theName /*+ "' Time - Min: " + placement[i].minutes + " Sec: " + placement[i].seconds*/;
                }
            }
            gameOver = true;
			scoreboard.text = "Scoreboard";
            if (isGrandPrix == false)
            {
                restart.text = "Press the 'A' to Restart or 'B' to return to race selection screen";
            }
            else
            {
                restart.text = "Press the 'A' to begin the next race";
            }
		}
		else if (((playersPastFinishLine == numPlayers) || (playersPastFinishLine + deadPlayersNum == numPlayers) || (numHumans == PlayerPrefs.GetFloat("NumberOfPlayers"))) && isGrandPrix == true)
        {
			if (activePlayers.Count > 0) {
				for (int i = 0; i < activePlayers.Count; i++) {
					activePlayers [i].GetComponent<AdvancedAI> ().Stop ();
					placement.Add (activePlayers [i].gameObject.GetComponent<AdvancedAI>());
				}
				activePlayers.Clear ();
			}

			if(deadPlayers.Count != 0){
				deadPlayers.Reverse ();
				for (int i = 0; i < deadPlayers.Count; i++) {
					placement.Add (deadPlayers [i]);
					deadPlayers.Remove (deadPlayers [i]);
				}
			}


            for (int i = 0; i < placement.Count; i++)
            {
                canvasBack.gameObject.SetActive(true);
                if (i == 0){
                    first.text = "1) '" + placement[i].theName + " + 10 Points";
                }else if (i == 1){
                    second.text = "2) '" + placement[i].theName + " + 8 Points";
                }else if (i == 2){
                    third.text = "3) '" + placement[i].theName + " + 6 Points";
                }else if (i == 3){
                    fourth.text = "4) '" + placement[i].theName + " + 5 Points";
                }else if (i == 4){
                    fifth.text = "5) '" + placement[i].theName + " + 4 Points";
                }
            }
            gameOver = true;
            scoreboard.text = "Scoreboard";
            if (isGrandPrix == false)
            {
                restart.text = "Press the 'A' to Restart or 'B' to return to race selection screen";
            }
            else
            {
                restart.text = "Press the 'A' to begin the next race";
            }
        }
    }

    //This has a three second countdown for the beginning of the race
    private IEnumerator Countdown()
    {
        while (true)
        {
            //Make sure none of the players/ ai update update countdown text
            countdown.text = count.ToString();
            count--;
			//figure out what this does
			//commented out by joe b/c it was breaking the races.
//            for (int i = 0; i < 8; i++)
//            {
//                if (activePlayers[i].tag.Contains("Sprite"))
//                {
//                    activePlayers[i].GetComponentInParent<Player>().begin = false;
//                    activePlayers[i].GetComponentInParent<Player>().StartCoroutine("timer", false);
//                }
//                else
//                {
//					activePlayers[i].GetComponent<BasicAI>().begin = false;
//					activePlayers[i].GetComponent<BasicAI>().StartCoroutine("timer", false);
//                }
//            }
            //Start the race end the coroutine
            if (count == -1)
            {
                countdown.text = "";
                for (int i = 0; i < 8; i++)
                {
                    if (activePlayers[i].tag.Contains("Sprite"))
                    {
                        activePlayers[i].GetComponentInParent<Player>().begin = true;
                        activePlayers[i].GetComponentInParent<Player>().StartCoroutine("timer", false);
                    }
                    else
                    {
                        activePlayers[i].GetComponent<Player>().begin = true;
						activePlayers[i].GetComponent<AdvancedAI>().StartCoroutine("timer", false);
                    }
                }
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
		if ((collision.gameObject.tag == "P1Sprite" && finishIncrement[0] == 0) && (collision.gameObject.transform.GetComponentInParent<Player>().isDead == false) ) {
			playerOne.finished = true;
			playersPastFinishLine++;
			placement.Add(playerOne);
            playerOne.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[0] = 1;
            activePlayers.Remove(playerOne.gameObject.transform.GetChild(0).gameObject);
            numHumans++;
        }
		else if ((collision.gameObject.tag == "P2Sprite" && finishIncrement[1] == 0) && (collision.gameObject.transform.GetComponentInParent<Player>().isDead == false)) {
			playerTwo.finished = true;
			playersPastFinishLine++;
			placement.Add(playerTwo);
            playerTwo.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[1] = 1;
            activePlayers.Remove(playerTwo.gameObject.transform.GetChild(0).gameObject);
            numHumans++;
        }
		else if ((collision.gameObject.tag == "P3Sprite" && finishIncrement[2] == 0) && (collision.gameObject.transform.GetComponentInParent<Player>().isDead == false)) {
			playerThree.finished = true;
			playersPastFinishLine++;
			placement.Add (playerThree);
            playerThree.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[2] = 1;
            activePlayers.Remove(playerThree.gameObject.transform.GetChild(0).gameObject);
            numHumans++;
        } 
		else if ((collision.gameObject.tag == "P4Sprite" && finishIncrement[3] == 0) && (collision.gameObject.transform.GetComponentInParent<Player>().isDead == false)) {
			playerFour.finished = true;
			playersPastFinishLine++;
			placement.Add (playerFour);
            playerFour.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[3] = 1;
            activePlayers.Remove(playerFour.gameObject.transform.GetChild(0).gameObject);
            numHumans++;
        }
		else if ((collision.gameObject.tag == "AdvancedAI1" && finishIncrement[4] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiOne.finished = true;
            aiOne.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiOne);
            aiOne.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[4] = 1;
            activePlayers.Remove(aiOne.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI2" && finishIncrement[5] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiTwo.finished = true;
            aiTwo.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiTwo);
            aiTwo.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[5] = 1;
            activePlayers.Remove(aiTwo.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI3" && finishIncrement[6] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiThree.finished = true;
            aiThree.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiThree);
            aiThree.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[6] = 1;
            activePlayers.Remove(aiThree.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI4" && finishIncrement[7] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiFour.finished = true;
            aiFour.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiFour);
            aiFour.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[7] = 1;
            activePlayers.Remove(aiFour.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI5" && finishIncrement[8] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiFive.finished = true;
            aiFive.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiFive);
            aiFive.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[8] = 1;
            activePlayers.Remove(aiFive.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI6" && finishIncrement[9] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiSix.finished = true;
            aiSix.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiSix);
            aiSix.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[9] = 1;
            activePlayers.Remove(aiSix.gameObject);
        }
		else if ((collision.gameObject.tag == "AdvancedAI7" && finishIncrement[10] == 0) && (collision.gameObject.GetComponent<AdvancedAI>().isDead == false))
        {
            aiSeven.finished = true;
            aiSeven.isPatrolling = false;
            playersPastFinishLine++;
            placement.Add(aiSeven);
            aiSeven.finishSpot = finishSpots[playersPastFinishLine - 1];
            finishIncrement[10] = 1;
            activePlayers.Remove(aiSeven.gameObject);
        }
    }

    private void PlacementAlgorithm()
    {
        GameObject temp;
        for (int i = 0; i < placementList.Count - 1; i++)
        {
            if (placementList[i].transform.position.y < placementList[i + 1].transform.position.y)
            {
                if (placementList[i + 1].gameObject.tag.Contains("Sprite"))
                {
                    if (!placementList[i + 1].GetComponentInParent<Player>().finished)
                    {
                        temp = placementList[i];
                        placementList[i] = placementList[i + 1];
                        placementList[i + 1] = temp;
                    }
                }
                else
                {
                    if (!placementList[i + 1].GetComponent<AdvancedAI>().finished)
                    {
                        temp = placementList[i];
                        placementList[i] = placementList[i + 1];
                        placementList[i + 1] = temp;
                    }
                }
            }
        }
        for (int i = 0; i < placementList.Count; i++)
        {
			if (placementList [i].gameObject.tag.Contains ("Sprite")) {
				placementList [i].gameObject.GetComponentInParent<Player> ().currentPlace = i + playersPastFinishLine + 1;
			} else {
				placementList [i].gameObject.GetComponentInParent<AdvancedAI> ().currentPlace = i + playersPastFinishLine + 1;
			}
        }

		if(deadPlayers.Count >= 0){
			for (int i = 0; i < deadPlayers.Count; i++) {
				if (deadPlayers [i].gameObject.tag.Contains ("Player")) {
					deadPlayers [i].gameObject.GetComponent<Player> ().currentPlace = 8;
				} 
				if (deadPlayers [i].gameObject.tag.Contains ("AI")) {
					deadPlayers [i].gameObject.GetComponent<AdvancedAI> ().currentPlace = 8;
				}
			}
		}
    }

    //needs cases for if some players are and are not in game.  
	//might be able to use that list in some way??
//    void FirstPlace(){
//		playerPlaceY = Mathf.Max (playerOne.transform.position.y, playerTwo.transform.position.y, aiOne.transform.position.y, aiTwo.transform.position.y, aiThree.transform.position.y, aiFour.transform.position.y, aiFive.transform.position.y, aiSix.transform.position.y);
//	}

}

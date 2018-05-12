///<summary>
///Made by Brad Tully; 8 May 2018
///This script will allow the player to begin time trial mode.
///No item boxes on the map, no other players/ ai. Keep track of the
///top 5 times on each racetrack.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeTrial : MonoBehaviour {
	public Player playerOne;
    public Text countdown, first, second, third, current, title, restart;
    public GameObject scoreboardback;
    public int count;
    public Vector3 finishSpot;
    public GameController gc;
    public int[] scores;
    private bool raceOver;
    public string levelName;

    // Use this for initialization
    void Start () {
        count = 3;
        StartCoroutine("Countdown");
        raceOver = false;

        //get all of the players/ai and destroy them all but player one
        GameObject playerOneObj = GameObject.FindWithTag("Player1");
        if (playerOneObj != null)
        {
            playerOne = playerOneObj.GetComponent<Player>();
            playerOne.theName = "Player One";
        }
        GameObject playerTwoObj = GameObject.FindWithTag("Player2");
        if (playerTwoObj != null)
        {
            Destroy(playerTwoObj);
        }
        GameObject playerThreeObj = GameObject.FindWithTag("Player3");
        if (playerThreeObj != null)
        {
            Destroy(playerThreeObj);
        }
        GameObject playerFourObj = GameObject.FindWithTag("Player4");
        if (playerFourObj != null)
        {
            Destroy(playerFourObj);
        }
        GameObject aiOneObj = GameObject.FindWithTag("AdvancedAI1");
        if (aiOneObj != null)
        {
            Destroy(aiOneObj);
        }
        GameObject aiTwoObj = GameObject.FindWithTag("AdvancedAI2");
        if (aiTwoObj != null)
        {
            Destroy(aiTwoObj);
        }
        GameObject aiThreeObj = GameObject.FindWithTag("AdvancedAI3");
        if (aiThreeObj != null)
        {
            Destroy(aiThreeObj);
        }
        GameObject aiFourObj = GameObject.FindWithTag("AdvancedAI4");
        if (aiFourObj != null)
        {
            Destroy(aiFourObj);
        }
        GameObject aiFiveObj = GameObject.FindWithTag("AdvancedAI5");
        if (aiFiveObj != null)
        {
            Destroy(aiFiveObj);
        }
        GameObject aiSixObj = GameObject.FindWithTag("AdvancedAI6");
        if (aiSixObj != null)
        {
            Destroy(aiSixObj);
        }
        GameObject aiSevenObj = GameObject.FindWithTag("AdvancedAI7");
        if (aiSevenObj != null)
        {
            Destroy(aiSevenObj);
        }

        //Destroy the gamecontroller, all finishing and scoreboard will be handled in this script
        GameObject gameControllerObj = GameObject.FindWithTag("GameController");
        if (gameControllerObj != null)
        {
            gc = gameControllerObj.GetComponent<GameController>();
            finishSpot = gc.finishSpots[0];
            Destroy(gameControllerObj);
        }

        //Get rid of all the item boxes
        GameObject itemboxes = GameObject.FindWithTag("ItemBoxes");
        if (itemboxes != null)
        {
            Destroy(itemboxes);
        }

        //Load in the high scores of the current scene
        scores = new int[3];
        if (levelName == "Ski_Race1")
        {
            scores[0] = PlayerPrefs.GetInt("r1FirstScore");
            scores[1] = PlayerPrefs.GetInt("r1SecondScore");
            scores[2] = PlayerPrefs.GetInt("r1ThirdScore");
        }
        else if (levelName == "Ski_Race2")
        {
            scores[0] = PlayerPrefs.GetInt("r2FirstScore");
            scores[1] = PlayerPrefs.GetInt("r2SecondScore");
            scores[2] = PlayerPrefs.GetInt("r2ThirdScore");
        }
        else if (levelName == "Ski_Rac3")
        {
            scores[0] = PlayerPrefs.GetInt("r3FirstScore");
            scores[1] = PlayerPrefs.GetInt("r3SecondScore");
            scores[2] = PlayerPrefs.GetInt("r3ThirdScore");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (raceOver == true)
        {
            if (Input.GetButtonDown("P1UseItem"))
            {
                SceneManager.LoadScene(levelName);
            }
            else if (Input.GetButtonDown("P1Brake"))
            {
                SceneManager.LoadScene("RaceMenu");
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
            //Start the race end the coroutine
            if (count == -1)
            {
                countdown.text = "";
                playerOne.begin = true;
                playerOne.StartCoroutine("timer", false);
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }

    //Update new fastest times
    private void setRecords(int min, int sec)
    {
        int total = (min * 60) + sec;
        int temp, temp2;
        if (levelName == "Ski_Race1")
        {
            if (total < scores[0] || scores[0] == 0)
            {
                temp = scores[0];
                scores[0] = total;
                temp2 = scores[1];
                scores[1] = temp;
                scores[2] = temp2;
            }
            else if (total < scores[1] || scores[1] == 0)
            {
                temp = scores[1];
                scores[1] = total;
                scores[2] = temp;
            }
            else if (total < scores[2] || scores[2] == 0)
            {
                scores[2] = total;
            }
            PlayerPrefs.SetInt("r1FirstScore", scores[0]);
            PlayerPrefs.SetInt("r1SecondScore", scores[1]);
            PlayerPrefs.SetInt("r1ThirdScore", scores[2]);
        }
        else if (levelName == "Ski_Race2")
        {
            if (total < scores[0] || scores[0] == 0)
            {
                temp = scores[0];
                scores[0] = total;
                temp2 = scores[1];
                scores[1] = temp;
                scores[2] = temp2;
            }
            else if (total < scores[1] || scores[1] == 0)
            {
                temp = scores[1];
                scores[1] = total;
                scores[2] = temp;
            }
            else if (total < scores[2] || scores[2] == 0)
            {
                scores[2] = total;
            }
            PlayerPrefs.SetInt("r2FirstScore", scores[0]);
            PlayerPrefs.SetInt("r2SecondScore", scores[1]);
            PlayerPrefs.SetInt("r2ThirdScore", scores[2]);
        }
        else if (levelName == "Ski_Race3")
        {
            if (total < scores[0] || scores[0] == 0)
            {
                temp = scores[0];
                scores[0] = total;
                temp2 = scores[1];
                scores[1] = temp;
                scores[2] = temp2;
            }
            else if (total < scores[1] || scores[1] == 0)
            {
                temp = scores[1];
                scores[1] = total;
                scores[2] = temp;
            }
            else if (total < scores[2] || scores[2] == 0)
            {
                scores[2] = total;
            }
            PlayerPrefs.SetInt("r3FirstScore", scores[0]);
            PlayerPrefs.SetInt("r3SecondScore", scores[1]);
            PlayerPrefs.SetInt("r3ThirdScore", scores[2]);
        }
    }

    //Update the scoreboard on screen
    private void updateScoreboard(int min, int sec)
    {
        //public Text countdown, first, second, third, current, title, restart;
        scoreboardback.gameObject.SetActive(true);
        title.text = "Scoreboard";
        first.text = "1) Min: " + Mathf.Floor(scores[0] / 60) + " Sec: " + (scores[0] % 60);
        second.text = "2) Min: " + Mathf.Floor(scores[1] / 60) + " Sec: " + (scores[1] % 60);
        third.text = "3) Min: " + Mathf.Floor(scores[2] / 60) + " Sec: " + (scores[2] % 60);
        current.text = "This race- Min: " + min + " Sec: " + sec;
        restart.text = "Press the 'A' to Restart or 'B' to return to race selection screen";
    }

    //End race when the player passes the finish line
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1Sprite")
        {
            playerOne.finished = true;
            playerOne.finishSpot = finishSpot;
            raceOver = true;
            setRecords(playerOne.minutes, playerOne.seconds);
            updateScoreboard(playerOne.minutes, playerOne.seconds);
        }
    }


}

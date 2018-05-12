///<summary>
///Made by Brad Tully 29 March 2018
///This class will be used for the grand prix game mode. The players will go through each race
///once and accumulate points based off of their finishing places.
///Point breakdown by place: 10, 8, 6, 5, 4, 3, 2, 1
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandPrix : MonoBehaviour {
    //this is used as a way to store names and scores for the grand prix
    public struct nameAndScore
    {
        public string theName;
        public int score;
        public string getName() { return theName; }
        public int getScore() { return score; }
    }

    GameController gameController;
    public int[] pointByPlace;
    Canvas canvasObj;
    Text first, second, third, fourth, fifth, sixth, seventh, eighth;
    public nameAndScore[] theScoreList;

    //Make sure it doesn't get destroyed between scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        theScoreList = new nameAndScore[8];
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        pointByPlace = new int[] { 10, 8, 6, 5, 4, 3, 2, 1 };

        //Load in names to the score list with all scores at 0
        for (int i = 0; i < theScoreList.Length; i++)
        {
            theScoreList[i].theName = gameController.currentPlayers[i].theName;
            theScoreList[i].score = 0;
            //Debug.Log(theScoreList[i].theName + " " + theScoreList[i].score);
        }
    }
	
	
	void Update () {
        
	}

    //Updates the score after each race
    public void UpdateScore()
    {
        GameObject gco = GameObject.FindWithTag("GameController");
        if (gco != null)
        {
            gameController = gco.GetComponent<GameController>();
        }
        for (int i = 0; i < theScoreList.Length; i++)
        {
            for (int j = 0; j < theScoreList.Length; j++) {
                if (theScoreList[i].getName() == gameController.placement[j].theName)
                {
                    theScoreList[i].score += pointByPlace[j];
                    Debug.Log(theScoreList[i].getName() + " " + gameController.placement[j].theName + " " + pointByPlace[j] + " " + theScoreList[i].getScore());
                }
            }
        }
    }

    //Sort list of finishing spots
    public void FinalPlacementSort()
    {
        nameAndScore temp;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (theScoreList[i].getScore() > theScoreList[j].getScore())
                {
                    temp.theName = theScoreList[i].getName();
                    temp.score = theScoreList[i].getScore();
                    theScoreList[i].score = theScoreList[j].getScore();
                    theScoreList[i].theName = theScoreList[j].getName();
                    theScoreList[j].score = temp.getScore();
                    theScoreList[j].theName = temp.getName();
                }
            }
        }
    }

    //Posts the final scoreboard
    public void PostScore()
    {
        GameObject firstObj = GameObject.FindWithTag("First");
        if (firstObj != null)
        {
            first = firstObj.GetComponent<Text>();
        }

        GameObject secondObj = GameObject.FindWithTag("Second");
        if (secondObj != null)
        {
            second = secondObj.GetComponent<Text>();
        }

        GameObject thirdObj = GameObject.FindWithTag("Third");
        if (thirdObj != null)
        {
            third = thirdObj.GetComponent<Text>();
        }

        GameObject fourthObj = GameObject.FindWithTag("Fourth");
        if (fourthObj != null)
        {
            fourth = fourthObj.GetComponent<Text>();
        }

        GameObject fifthObj = GameObject.FindWithTag("Fifth");
        if (fifthObj != null)
        {
            fifth = fifthObj.GetComponent<Text>();
        }

        GameObject sixthObj = GameObject.FindWithTag("Sixth");
        if (sixthObj != null)
        {
            sixth = sixthObj.GetComponent<Text>();
        }

        GameObject seventhObj = GameObject.FindWithTag("Seventh");
        if (seventhObj != null)
        {
            seventh = seventhObj.GetComponent<Text>();
        }

        GameObject eighthObj = GameObject.FindWithTag("Eighth");
        if (eighthObj != null)
        {
            eighth = eighthObj.GetComponent<Text>();
        }

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
            {
                first.text = "1) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 1)
            {
                second.text = "2) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 2)
            {
                third.text = "3) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 3)
            {
                fourth.text = "4) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 4)
            {
                fifth.text = "5) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 5)
            {
                sixth.text = "6) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 6)
            {
                seventh.text = "7) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
            else if (i == 7)
            {
                eighth.text = "8) '" + theScoreList[i].getName() + "' Score: " + theScoreList[i].getScore();
            }
        }
    }

    //Use this to destroy the grand prix object
    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }

}

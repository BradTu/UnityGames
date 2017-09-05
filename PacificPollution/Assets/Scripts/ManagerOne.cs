//Brad Tully
//5 June 2017
//Manages scene one of the game

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerOne : MonoBehaviour {
    public float trashCount, trashTotal;
    public int seconds = 10;
    public Text timeLeft;
    public Text trashText;
    public Text endLevel;
    public float trashRatio;
    public bool gameOver;

    //Changes the text on screen that shows the number of trash picked up
    void setText()
    {
        //Set to the amount of trash picked up
        trashText.text = "Trash Count: " + trashCount + " / " + trashTotal;
    }

    //Timer counts down from a specified amount of seconds
    private IEnumerator timer(int time)
    {
        //Decrease time and set that time to the GUI
        while (time > 0)
        {
            time--;
            timeLeft.text = "Time Left: " + time;
            yield return new WaitForSeconds(1);
        }
        //End scene
        if (time <= 0)
        {
            endLevel.text = "Press 'N' to load the next level";
        }
    }

    //Updates the timer
    private void timerUpdate()
    {
        timer(seconds);
    }

    // Use this for initialization
    void Start () {
        trashCount = 0;
        endLevel.text = "";
        setText();
        StartCoroutine("timer", seconds);
        gameOver = false;
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        timerUpdate();
        setText();
        trashRatio = trashCount / trashTotal;
        if (endLevel.text == "Press 'N' to load the next level")
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        if (gameOver == true && Input.GetKeyDown(KeyCode.N))
        {
            PlayerPrefs.SetFloat("TheRatio", trashRatio);
            Application.LoadLevel("Runner Scene");
        }
    }



}

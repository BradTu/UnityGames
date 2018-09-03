//Brad Tully
//5 June 2017
//Manages scene one of the game

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerOne : MonoBehaviour {
    public float trashCount, trashTotal;
    public int seconds;
    public Text timeLeft;
    public Text trashText;
    public Text endLevel;
    public float trashRatio;
    public bool gameOver;
    public GameObject trashPrefab;
    public GameObject trashPrefab2;
    public GameObject trashPrefab3;
    public GameObject trashPrefab4;
    public GameObject trashPrefab5;
    Quaternion theRotation;
    float rand;
    public GameObject image;

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
            timeLeft.text = "Time Left: " + time + " sec";
            yield return new WaitForSeconds(1);
        }
        //End scene
        if (time <= 0)
        {
            image.SetActive(true);
            endLevel.text = "Press the space bar to load the next level";
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
        trashTotal = PlayerPrefs.GetInt("LevelValue") * 10 + 10;
        endLevel.text = "";
        setText();
        StartCoroutine("timer", seconds);
        gameOver = false;
        Time.timeScale = 1;

        for (int i = 0; i < trashTotal; i++)
        {
            rand = Random.Range(1, 6);
            Vector3 position = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.3f, 3.0f), 0);
            theRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            if (rand == 1)
            {
                Instantiate(trashPrefab, position, theRotation);
            }
            else if (rand == 2)
            {
                Instantiate(trashPrefab2, position, theRotation);
            }
            else if (rand == 3)
            {
                Instantiate(trashPrefab3, position, theRotation);
            }
            else if (rand == 4)
            {
                Instantiate(trashPrefab4, position, theRotation);
            }
            else if (rand == 5)
            {
                Instantiate(trashPrefab5, position, theRotation);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timerUpdate();
        setText();
        trashRatio = trashCount / trashTotal;
        if (endLevel.text == "Press the space bar to load the next level")
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        if (gameOver == true && Input.GetKeyDown("space"))
        {
            PlayerPrefs.SetFloat("TheRatio", trashRatio);
            Debug.Log(trashRatio);
            PlayerPrefs.SetFloat("TotalTrash", PlayerPrefs.GetFloat("TotalTrash") + trashCount);
            SceneManager.LoadScene("Runner Scene");
        }
    }



}

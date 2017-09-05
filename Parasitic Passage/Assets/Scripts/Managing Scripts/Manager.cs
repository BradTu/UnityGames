//Made by Brad Tully
//Parasitic Passage
//Controls the state of each level

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Manager : MonoBehaviour {
    public Text bugCount;
    public Text endLevel;
    public Text timeLeft;
    public string nextLevel;
    public int winningAmount = 5;
    public int seconds = 10;
    int count = 0;
    public string saveFileNameOne, saveFileNameTwo;
    public bool startScreen;

    // Use this for initialization
    void Start () {
        if (startScreen == false)
        {
            endLevel.text = "";
            setText();
            StartCoroutine("timer", seconds);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (startScreen == false)
        {
            timerUpdate();
            if (restartLevel(endLevel) == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            if (endLevel.text == "Good job! Press 'N' to load the next level" && Input.GetKeyDown(KeyCode.N) == true)
            {
                Application.LoadLevel(nextLevel);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.LoadLevel("Start Screen");
            }
        }
    }

    //Changes the text on screen that shows the number of bugs infected
    void setText()
    {
        //Number of infected bugs saved in the Parasite script (Player)
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        Parasite p = obj.GetComponent<Parasite>();
        //Set to the amount of bugs infected
        bugCount.text = "Bug Count: " + p.count.ToString() + " / " + winningAmount;
        //Winning condition for level
        if (p.count == winningAmount)
        {
            endLevel.text = "Good job! Press 'N' to load the next level";
        }
    }

    //Timer counts down from a specified amount of seconds
    private IEnumerator timer(int time)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        Parasite p = obj.GetComponent<Parasite>();
        //Decrease time and set that time to the GUI
        while (time > 0)
        {
            time--;
            timeLeft.text = "Time Left: " + time;
            yield return new WaitForSeconds(1);
        }
        //Losing condition
        if (time <= 0 && p.count < winningAmount)
        {
            endLevel.text = "You Lose: R to restart";
            //Time.timeScale = 0;
        }
    }

    //Updates the timer
    private void timerUpdate()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        Parasite p = obj.GetComponent<Parasite>();
        setText();
        //If a new bug is infected restart the timer
        if (p.count > count)
        {
            StopCoroutine("timer");
            count = p.count;
            StartCoroutine("timer", seconds + 1);
        }
        timer(seconds);
    }

    //Once the losing condition is true reset the level when the player presses "R"
    private bool restartLevel(Text t)
    {
        bool tf = false;
        if (t.text == "You Lose: R to restart")
        {
            tf = true;
            if (Input.GetKeyDown(KeyCode.R) == true)
            {
                Application.LoadLevel(Application.loadedLevel); 
            }
        }
        return tf;
    }

    // this can be passed to a button that calls this method when clicked
    public void OnSaveClickOne()
    {
        SaveGame saveGame = new SaveGame();

        // then write it out
        SaveGameModel(saveGame, saveFileNameOne);
    }

    // this can be passed to a button that calls this method when clicked
    public void OnSaveClickTwo()
    {
        SaveGame saveGame = new SaveGame();

        // then write it out
        SaveGameModel(saveGame, saveFileNameTwo);
    }

    // this can be passed to a button that calls this method when clicked
    public void OnLoadClickOne()
    {
        // load game does all the work of calling loaddata
        LoadGame(saveFileNameOne);
    }

    // this can be passed to a button that calls this method when clicked
    public void OnLoadClickTwo()
    {
        // load game does all the work of calling loaddata
        LoadGame(saveFileNameTwo);
    }

    //Save to a filestream
    public void SaveGameModel(SaveGame save, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        // then create a file stream that can be opened or created, with write access to it
        FileStream fs = File.OpenWrite(Application.persistentDataPath + "/" + filename + ".dat");

        // note that we store the data from our game model (this object)
        // first in the SaveGame instance, think of SaveGame like a file
        save.StoreData(this);

        // then we can serialize it to the disk using Serialize and
        // we serialize the SaveGame object. 
        bf.Serialize(fs, save);

        // close the file stream
        fs.Close();
    }

    //Loads game from filestream
    public void LoadGame(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.OpenRead(Application.persistentDataPath + "/" + filename + ".dat");

        // deserialize the save game--this will throw an exception if we can't
        // deserialize from the file stream
        SaveGame saveGame = (SaveGame)bf.Deserialize(fs);

        // we assume we have access to the game model
        saveGame.LoadData(this);

        // close the file stream
        fs.Close();
    }

}

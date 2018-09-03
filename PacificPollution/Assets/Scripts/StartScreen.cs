//Brad Tully
//5 June 2017
//This class controlls the start screen of the game

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {
    public Text trashRecord;
    public Text levelRecord;

    //Load first level
    public void LoadScene(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

    public void setLevelValue()
    {
        PlayerPrefs.SetInt("LevelValue", 1);
        PlayerPrefs.SetFloat("TotalTrash", 0f);
    }

    //Quit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Use this for initialization
    void Start () {
        trashRecord.text = "Most Trash Picked Up: " + PlayerPrefs.GetFloat("TrashRecord");
        levelRecord.text = "Highest Level Reached: " + PlayerPrefs.GetInt("LevelRecord");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

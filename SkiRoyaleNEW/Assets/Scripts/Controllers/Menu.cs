using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Changes scenes 
/// </summary>

public class Menu : MonoBehaviour
{
    public string levelName;
    public string previousScene;
    public GrandPrix gp;

	void Start ()
	{
        Time.timeScale = 1f;
        GameObject grandPrixObj = GameObject.FindWithTag("GrandPrix");
        if (grandPrixObj != null)
        {
            gp = grandPrixObj.GetComponent<GrandPrix>();
            gp.DestroyThisObject();
        }
}


	void Update ()
	{
		if (Input.GetButtonDown("P1UseItem"))
        {
            LoadSceneByName(levelName);
        }
        if (Input.GetButtonDown("P1Brake"))
        {
            LoadSceneByName(previousScene);
        }
	}

	//Load level by number
	public void LoadScene (int levelNumber)
	{
		SceneManager.LoadScene (levelNumber);
	}

	//Load controls screen
	public void LoadControls (int levelNumber)
	{
		SceneManager.LoadScene (levelNumber);
	}

    //Save the number of players in the game for dynamic loading in the next scene
    public void NumberOfPlayers(float numPlayers)
    {
        PlayerPrefs.SetFloat("NumberOfPlayers", numPlayers);
    }

	//Exit game
	public void ExitGame ()
	{
		Application.Quit ();
	}

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    //If it is grand prix mode this is equal to 1 else 0
    public void GrandPrixMode(int gp)
    {
        PlayerPrefs.SetInt("GrandPrixMode", gp);
    }

    //If it is time trial mode this is equal to 1 else 0
    public void TimeTrialMode(int tt)
    {
        PlayerPrefs.SetInt("TimeTrialMode", tt);
    }
}

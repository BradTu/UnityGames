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

	void Start ()
	{
        Time.timeScale = 1f;
    }


	void Update ()
	{
		if (Input.GetButtonDown("P1UseItem"))
        {
            LoadSceneByName(levelName);
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

	//Exit game
	public void ExitGame ()
	{
		Application.Quit ();
	}

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}

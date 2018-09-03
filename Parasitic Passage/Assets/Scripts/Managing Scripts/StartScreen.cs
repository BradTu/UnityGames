//Made by Brad Tully
//Parasitic Passage
//Load first level

using UnityEngine;
using System.Collections;


public class StartScreen : MonoBehaviour {

    //Load first level
    public void LoadScene(string levelname)
    {
        Application.LoadLevel(levelname);
    }

    public void LoadOne()
    {
        Application.LoadLevel(PlayerPrefs.GetString("Level"));
    }

    //Exit game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   
}

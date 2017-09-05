//Brad Tully
//5 June 2017
//This class controlls the start screen of the game

using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

    //Load first level
    public void LoadScene(string levelname)
    {
        Application.LoadLevel(levelname);
    }

    //Quit the game
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

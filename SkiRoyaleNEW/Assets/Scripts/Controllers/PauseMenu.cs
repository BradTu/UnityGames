///<summary>
///Made by Brad Tully 19 April 2018
///This will allow the player to pause the game and bring up a menu that
///will allow them to quit to main menu, resume, and look at the controls.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public Transform canvas, eventSystem;
    public Text controlText;
    public Image controlBackground;
    public Button controlBackButton, controlMenu, resumeGame, quitGame;

    //Resume the race
    public void resumeRace()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    //Bring up the controls
    public void controls()
    {
        controlMenu.gameObject.SetActive(false);
        resumeGame.gameObject.SetActive(false);
        quitGame.gameObject.SetActive(false);
        controlBackground.gameObject.SetActive(true);
        controlText.gameObject.SetActive(true);
        controlBackButton.gameObject.SetActive(true);
        controlBackButton.Select();
    }

    //Quit and return to main menu
    public void quitRace()
    {
        SceneManager.LoadScene("Menu");
    }

    //Return to the pause screen
    public void backToPauseMenu()
    {
        controlBackground.gameObject.SetActive(false);
        controlText.gameObject.SetActive(false);
        controlBackButton.gameObject.SetActive(false);
        controlMenu.gameObject.SetActive(true);
        resumeGame.gameObject.SetActive(true);
        quitGame.gameObject.SetActive(true);
        resumeGame.Select();
    }

    //Make sure the canvas and event system are off
    private void Start()
    {
        canvas.gameObject.SetActive(false);
        eventSystem.gameObject.SetActive(false);
    }

    //Check for pause button
    void Update () {
		if (Input.GetButtonDown("Jump"))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                Time.timeScale = 0f;
                canvas.gameObject.SetActive(true);
                eventSystem.gameObject.SetActive(true);
                resumeGame.Select();
            }
            else
            {
                canvas.gameObject.SetActive(false);
                controlBackground.gameObject.SetActive(false);
                controlText.gameObject.SetActive(false);
                controlBackButton.gameObject.SetActive(false);
                controlMenu.gameObject.SetActive(true);
                resumeGame.gameObject.SetActive(true);
                quitGame.gameObject.SetActive(true);
                Time.timeScale = 1f;
            }
        }
	}
}

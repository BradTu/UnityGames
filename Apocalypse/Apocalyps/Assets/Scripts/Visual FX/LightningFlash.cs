/**
 * This script manages how often lightning will strike when it is raining
 * Jason Komoda 10/4/17
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LightningFlash : MonoBehaviour {

    AudioSource thunderAudio;   // sound that goes along with every flashh
	GameController gameController;

    // create a public CanvasGroup

    public CanvasGroup myCG;
    private bool flash = false; // whether to lightning flash or not
    private int randomNum;      // random number that decides when to flash

    // Use this for initialization
    void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
        thunderAudio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		//if it's raining, then do lightning/thunder too
		if (gameController.isStorm)
		{
			randomNum = Random.Range(1, 500);
			if (randomNum == 1)
			{
				Lightning();
			}
		}

        // sets up the white flash on screen
        if (flash)
        {
            myCG.alpha = myCG.alpha - .25f;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    // turn screen white for a split second, and play sound
    public void Lightning()
    {
        flash = true;
        thunderAudio.Play();
        myCG.alpha = 1;
    }
}

///<summary>
///Made by Brad Tully 
///This class is used for the players in the tutorials.
///It is simplified to work for showing simple mechanics of the game
///</summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialPlayer : Player {

	// Use this for initialization
	void Start () {
        startVelocity = forwardVelocity;
        skiing = true;
        StartCoroutine("SpeedIncrease", skiing);
        finished = false;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        hit = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("P1UseItem"))
        {
            begin = true;
        }
        if (begin == true)
        {
            releaseBrake();
            //hardTurnUp();
        }
	}

    private void FixedUpdate()
    {
        if (begin == true)
        {
            Moving();
            SpeedIncrease(skiing);
        }
    }

    
}

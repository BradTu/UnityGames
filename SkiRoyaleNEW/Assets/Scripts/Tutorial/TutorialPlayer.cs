using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialPlayer : Player {

	// Use this for initialization
	void Start () {
        startVelocity = forwardVelocity;
        skiing = true;
        StartCoroutine("SpeedIncrease", skiing);
        //StartCoroutine ("timer", finished);
        finished = false;

        //Hard turn values set
        hardDirection = 0;
        hardDirInc = 2.5f * directionIncrement;
        hardSideVelInc = 3 * sideVelInc;
        hardFVelDec = 8 * velocityDecrease;
        hardDirMax = 70;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        //begin = false;

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
            Moving();
            releaseBrake();
            hardTurnUp();
            SpeedIncrease(skiing);
        }
	}

    private void FixedUpdate()
    {
        
    }

    
}

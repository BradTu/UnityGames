///<summary>
///Made by Brad Tully 8 March 2018
///This is a door that can be opened or closed by hitting an obstacle
///button
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MovingObj {
    private bool opening, closing;
    public bool left, right;
    private int direction, increment;
    private float angle;
    private bool isActive;

    // Use this for initialization
    void Start () {
        opening = false;
        closing = false;
        if (left == true && right == false)
        {
            direction = 1;
        }
        if (right == true && left == false)
        {
            direction = -1;
        }
        increment = 1;
        isActive = false;
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        open();
        close();
        waitToOpen();
	}

    //Called when the button is pressed, begins to close door
    public override void turnOn()
    {
        closing = true;
        if (isActive == false)
        {
            StartCoroutine("close");
        }
        isActive = true;
    }

    //Opens up the door
    private IEnumerator open()
    {
        while (opening == true)
        {
            angle += increment * direction * -1;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            if (angle == 0)
            {
                opening = false;
                isActive = false;
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    //Closes off a part of the track
    private IEnumerator close()
    {
        while (closing == true)
        {
            if (isActive == true)
            {
                Debug.Log(angle);
                angle += increment * direction;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                if (angle == 90 || angle == -90)
                {
                    closing = false;
                    StartCoroutine("waitToOpen");
                }
            }
            yield return new WaitForSeconds(.01f);
        }
    }

    //Time waited after door closes to open
    private IEnumerator waitToOpen()
    {
        bool tf = true;
        int i = 0;
        while (tf == true)
        {
            if (i == 3)
            {
                opening = true;
                StartCoroutine("open");
                tf = false;
            }
            i++;
            yield return new WaitForSeconds(1f);
        }
    }
}

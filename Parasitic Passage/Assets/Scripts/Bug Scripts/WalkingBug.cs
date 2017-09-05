//Made by Brad Tully
//Parasitic Passage
//This class is a parent to all bugs that walk

using UnityEngine;
using System.Collections;

public class WalkingBug : Bug {
    //Tells whether or not the bug is in a surface only it can travel through
    public bool inSpecialSurface;

    //Player input controls position of bug
    public void playerControl(bool controlStatus, bool inOthersurface)
    {
        if (controlStatus == true)
        {
            partsys.Emit(1);
            patrolVelocity = 0;
            //Check horizontal movement
            horizontalSpeed = Input.GetAxis("Horizontal");
            //Positive x direction
            if (horizontalSpeed > .0001f)
            {
                //moveSpeed.y = 0;
                if (velocity < 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
                moveSpeed.y = 0;
                //Make it face right
                direction = 1;
                transform.localScale = new Vector2(xScale, yScale);
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Negative x direction
            else if (horizontalSpeed < -.0001f)
            {
                //moveSpeed.y = 0;
                if (velocity > 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
                moveSpeed.y = 0;
                //Make the bug face left
                direction = -1;
                transform.localScale = new Vector2(-xScale, yScale);
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Stand still
            else if (horizontalSpeed == 0.000f)
            {
                moveSpeed.x = 0;
                moveSpeed.y = 0;
                transform.position = transform.position;
            }
            //Check vertical movement
            verticalSpeed = Input.GetAxis("Vertical");
            //Negateive y direction
            if (verticalSpeed < -.0001f)
            {
                if (velocity > 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.y = velocity;
                moveSpeed.x = 0;
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Check if it is in special surface
            if (inOthersurface == true)
            {
                //Allows bug to climb up
                if (verticalSpeed > .0001f)
                {
                    if (velocity < 0)
                    {
                        velocity = velocity * -1;
                    }
                    moveSpeed.y = velocity;
                    moveSpeed.x = 0;
                    transform.position = transform.position + (moveSpeed * Time.deltaTime);
                }
                //Stay still
                else if (verticalSpeed == 0.000f)
                {
                    moveSpeed.x = 0;
                    moveSpeed.y = 0;
                    transform.position = transform.position;
                }
            }
        }
    }


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

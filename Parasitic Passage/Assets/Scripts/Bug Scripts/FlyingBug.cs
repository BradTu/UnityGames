//Made by Brad Tully
//Parasitic Passage
//Parent to all flying bugs

using UnityEngine;
using System.Collections;

public class FlyingBug : Bug {
    
    //Flying bug has the ability to move up and down
    public override void playerControl(bool controlStatus)
    {
        if (controlStatus == true)
        {
            //partsys.Emit(1);
            //Check horizontal movement
            horizontalSpeed = Input.GetAxis("Horizontal");
            //Positive x direction
            if (horizontalSpeed > .01f)
            {
                moveSpeed.y = 0;
                if (velocity < 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
                //Make it face the right
                direction = 1;
                transform.localScale = new Vector2(xScale, yScale);
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Make it move in the negative x direction
            else if (horizontalSpeed < -.01f)
            {
                moveSpeed.y = 0;
                if (velocity > 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
                //Make it face left
                direction = -1;
                transform.localScale = new Vector2(-xScale, yScale);
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Stay in place
            else
            {
                transform.position = transform.position;
            }
            //Check vertical movement
            verticalSpeed = Input.GetAxis("Vertical");
            //Positive y direction
            if (verticalSpeed > .01f)
            {
                if (velocity < 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.y = velocity;
                moveSpeed.x = 0;
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Negative y direction
            else if (verticalSpeed < -.01f)
            {
                if (velocity > 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.y = velocity;
                moveSpeed.x = 0;
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Stay still
            else
            {
                transform.position = transform.position;
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

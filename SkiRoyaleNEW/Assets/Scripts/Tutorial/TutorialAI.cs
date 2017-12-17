using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAI : BasicAI {

	// Use this for initialization
	void Start () {
        hit = false;
        startVelocity = forwardVelocity;
        skiing = true;

        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        minutes = 0;
        seconds = 0;
        begin = true;
        finished = false;
    }
	
	// Update is called once per frame
	void Update () {
            //Moving();
            //RubberBand();
            /*if (finished == false)
            {
                Moving();
                RubberBand();
            }*/

            if (((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x > 1) || ((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x < -1))))
            {
                this.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
    }

    private void FixedUpdate()
    {
        if (((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x > 1) || ((hit == false) && (this.GetComponentInChildren<Rigidbody2D>().velocity.x < -1))))
        {
            this.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}

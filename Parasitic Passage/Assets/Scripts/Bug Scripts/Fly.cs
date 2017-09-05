//Made by Brad Tully
//Parasitic Passage
//Controls the fly object

using UnityEngine;
using System.Collections;

public class Fly : FlyingBug {

    // Use this for initialization
    void Start () {
        isControlled = false;
        temp = false;
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        xScale2 = transform.localScale.x;
        yScale2 = transform.localScale.y;
        zScale = transform.localScale.z;
        direction = 1;
        partsys = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isControlled == false)
        {
            pat();
        }
        playerControl(isControlled);
        launch();
    }

    //Check entering triggers
    void OnTriggerEnter2D(Collider2D col)
    {
        if (callEnterOnce <= 0)
        {
            //Increment the bugs infected
            if (col.gameObject.tag == "Player")
            {
                Parasite p = col.gameObject.GetComponent<Parasite>();
                p.count++;
            }
        }
    }

    //Check staying triggers
    void OnTriggerStay2D(Collider2D col)
    {
        //If the fly is in a web it gets stuck
        if (col.gameObject.tag == "Web")
        {
            transform.position = col.transform.position;
        }
        //Make the fly controllable by the Player
        if (col.gameObject.tag == "Player")
        {
            if (temp == false)
            {
                Parasite p = col.gameObject.GetComponent<Parasite>();
                p.isControlled = false;
                p.velocity = velocity;
                p.transform.position = transform.position;
                isControlled = true;
                p.isPuked = false;
                p.isLaunched = false;
                if (isLaunched == true)
                {
                    p.direction = direction;
                    p.isPuked = true;
                    p.playSpitSound();
                    Destroy(gameObject);
                }
            }
        }
    }

    //Check exiting triggers
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Parasite p = col.gameObject.GetComponent<Parasite>();
            p.transform.position = transform.position + (moveSpeed * Time.deltaTime);
        }
    }

    //Overrides the launch to allow the bug to "puke" the parasite downwards instead of forward
    public override void launch()
    {
        if (isControlled == true)
        {
            if (Input.GetButtonDown("Jump") == true)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                Parasite p = obj.GetComponent<Parasite>();
                isLaunched = true;
                p.isPuked = true;
            }
        }
    }
}

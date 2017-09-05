//Made by Brad Tully
//Parasitic Passage
//Controls the bee object

using UnityEngine;
using System.Collections;

public class Bee : FlyingBug {

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (isControlled == false)
        {
            pat();
        }
        playerControl(isControlled);
        launch();
    }

    //Increment bugs infected
    void OnTriggerEnter2D(Collider2D col)
    {
        if (callEnterOnce <= 0)
        {
            if (col.gameObject.tag == "Player")
            {
                Parasite p = col.gameObject.GetComponent<Parasite>();
                p.count++;
                callEnterOnce++;
            }
        }
    }

    //Check staying triggers
    void OnTriggerStay2D(Collider2D col)
    {
        //If the bee is in a spider's web it can't move
        if (col.gameObject.tag == "Web")
        {
            transform.position = col.transform.position;
        }
        //Make the bee player controlled
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
                    p.isLaunched = true;
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
            p.transform.position = transform.position;
        }
    }
}

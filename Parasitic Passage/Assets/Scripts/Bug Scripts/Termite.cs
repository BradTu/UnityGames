//Made by Brad Tully
//Parasitic Passage
//This class is for the termite bug

using UnityEngine;
using System.Collections;

public class Termite : WalkingBug {
    Animator anim;
    public bool polygonIgnore;

    //Control the animation playing
    public void animationState(float vel, float vel2, float pv)
    {
        if (vel >= .001f || vel2 >= 0.001f || pv >= 0.001f)
        {
            anim.SetFloat("Speed", 2.0f);
        }
        else if (vel < .001f && vel2 < .001f && pv < .001f)
        {
            anim.SetFloat("Speed", 0.0f);
        }
    }

    // Use this for initialization
    void Start () {
        isControlled = false;
        temp = false;
        inSpecialSurface = false;
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        xScale2 = transform.localScale.x;
        yScale2 = transform.localScale.y;
        zScale = transform.localScale.z;
        anim = GetComponent<Animator>();
        direction = 1;
        partsys = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        animationState(Mathf.Abs(horizontalSpeed), Mathf.Abs(verticalSpeed), Mathf.Abs(patrolVelocity));
        playerControl(isControlled, inSpecialSurface);
        launch();
        if (isControlled == false)
        {
            pat();
        }   
    }

    //Check entering triggers
    void OnTriggerEnter2D(Collider2D col)
    {
        //When the termite is in a tree allow it to travel around the trunk of it
        if (col.gameObject.tag == "tree")
        {
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        if (callEnterOnce <= 0)
        {
            //Increment bugs infected
            if (col.gameObject.tag == "Player")
            {
                Parasite p = col.gameObject.GetComponent<Parasite>();
                p.count++;
            }
        }
    }

    //Check the staying triggers
    void OnTriggerStay2D(Collider2D col)
    {
        //Make sure the termite can climb a tree when in it
        if (col.gameObject.tag == "tree")
        {
            if (polygonIgnore == true)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<PolygonCollider2D>());
            }
            if (polygonIgnore == false)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<BoxCollider2D>());
            }
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        //Make the termite player controlled
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
        //If the termite exits the tree gravity turns back on 
        if (col.gameObject.tag == "tree")
        {
            inSpecialSurface = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravScale;
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), 
            col.gameObject.GetComponent<PolygonCollider2D>(), false);
        }
    }
}

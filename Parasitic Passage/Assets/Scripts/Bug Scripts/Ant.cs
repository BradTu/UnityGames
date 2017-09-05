//Made by Brad Tully
//Parasitic Passage
//Controls the ant object

using UnityEngine;
using System.Collections;

public class Ant : WalkingBug {
    Animator anim;

    //Controls the animation state
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
        anim = GetComponent<Animator>();
        zScale = transform.localScale.z;
        direction = 1;
        partsys = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        animationState(Mathf.Abs(horizontalSpeed), Mathf.Abs(verticalSpeed), Mathf.Abs(patrolVelocity));
        if (isControlled == false)
        {
            pat();
        }
        playerControl(isControlled, inSpecialSurface);
        launch();
    }

    //Increment the total bugs infected
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<EdgeCollider2D>());
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        if (callEnterOnce <= 0)
        {
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
        //Allows the ant to travel through the ground
        if (col.gameObject.tag == "ground")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), col.gameObject.GetComponent<EdgeCollider2D>());
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        //Give the ant player control
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

    //Reset the ant when it isn't underground so it can't move upwards
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            inSpecialSurface = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravScale;
        }
    }
}

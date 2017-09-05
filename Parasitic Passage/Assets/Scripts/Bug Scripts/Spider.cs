//Made by Brad Tully
//Parasitic Passage
//This controls the spider bug

using UnityEngine;
using System.Collections;

public class Spider : WalkingBug {
    public GameObject webPrefab;
    protected GameObject createdWeb;
    Vector3 webPosition;
    public int numWebs;
    Quaternion theRotation;
    Animator anim;

    //Allows the spider to create webs by clicking on the screen
    void webSpawn(bool controlled)
    {
        if (controlled == true)
        {
            //Left mouse button pressed and a total number of webs
            if (Input.GetButtonDown("Fire1") == true && numWebs < 4)
            {
                theRotation.z = 0;
                theRotation.y = 0;
                theRotation.x = 0;
                theRotation.w = 0;
                //Determine where the mouse was pressed and instantiate a web object
                webPosition = Input.mousePosition;
                webPosition = Camera.main.ScreenToWorldPoint(webPosition);
                webPosition.z = 0;
                Instantiate(webPrefab, webPosition, theRotation);
                numWebs++;
            }
        }
    }

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
        if (isControlled == false)
        {
            pat();
        }
        playerControl(isControlled, inSpecialSurface);
        webSpawn(isControlled);
        launch();
    }

    //Check when the spider enters a web and when the Parasite infects the spider
    void OnTriggerEnter2D(Collider2D col)
    {
        //Allows the spider to climb the web
        if (col.gameObject.tag == "Web")
        {
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        if (callEnterOnce <= 0)
        {
            //Increase the amount of bugs infected
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
        //Make sure the spider can continually climb the web
        if (col.gameObject.tag == "Web")
        {
            inSpecialSurface = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        //Make the spider controllable by the player
        if (col.gameObject.tag == "Player")
        {
            if (temp == false)
            {
                Parasite p = col.gameObject.GetComponent<Parasite>();
                p.isControlled = false;
                p.transform.position = transform.position;
                p.velocity = velocity;
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
        //When the spider leaves the web reset gravity and make it fall
        if (col.gameObject.tag == "Web")
        {
            inSpecialSurface = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravScale;
        }
    }


}

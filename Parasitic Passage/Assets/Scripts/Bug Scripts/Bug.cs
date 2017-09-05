//Made by Brad Tully
//Parasitic Passage
//Parent to all playable bugs and parasite

using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {
    [HideInInspector]
    public Vector3 moveSpeed;
    [HideInInspector]
    public Vector3 rotation;
    public int velocity, patrolVelocity;
    public bool isControlled, isPatrolling, temp, isLaunched;
    [HideInInspector]
    public float horizontalSpeed = 0;
    [HideInInspector]
    public float verticalSpeed = 0;
    [HideInInspector]
    public float xScale, yScale, xScale2, yScale2, zScale;
    [HideInInspector]
    public int callEnterOnce = 0;
    public float direction = 1;
    public int gravScale = 20;
    public AudioClip spitSound;
    public AudioSource source;
    public Vector3[] Waypoints;
    public bool Loop = true;
    public int CurrentIdx =-1;
    public Vector3 CurrentWaypoint = Vector3.zero;
    public ParticleSystem partsys;
    float shakeTime, shakeAmount;

    //Source: https://www.reddit.com/r/Unity2D/comments/2uzhps/enemy_walking_from_point_to_point/
    //Patrol between points
    //Edits by Brad Tully
    public void GetNextWaypoint()
    {
        CurrentIdx++;
        direction = direction * -1;
        xScale2 = xScale2 * -1;
        transform.localScale = new Vector2(xScale2, yScale2);
        if (CurrentIdx >= Waypoints.Length)
        {
            if (Loop)
            {
                CurrentIdx = CurrentIdx % Waypoints.Length;
            }
        }
        if (CurrentIdx < Waypoints.Length)
        {
            CurrentWaypoint = Waypoints[CurrentIdx];
        }
    }

    //Source: https://www.reddit.com/r/Unity2D/comments/2uzhps/enemy_walking_from_point_to_point/
    //Edits by Brad Tully
    public void pat()
    {
        if (transform.position != CurrentWaypoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint, patrolVelocity * Time.deltaTime);
        }
        else
        { 
            GetNextWaypoint();
        }
    }

    //Launches the Parasite from the current bug to infect another if successful
    public virtual void launch()
    {
        //Check if the bug is controlled by the player
        if (isControlled == true) {
            //If space bar is hit launch the Parasite
            if (Input.GetButtonDown("Jump") == true)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                Parasite p = obj.GetComponent<Parasite>();
                p.isLaunched = true;
                isLaunched = true;
            }
        }
    }

    //When true the bug is under user control, false it isn't
    public virtual void playerControl(bool tf)
    {
        if (tf == true)
        {
            //Check horizontal movement
            horizontalSpeed = Input.GetAxis("Horizontal");
            //Moving in the positive direction
            if (horizontalSpeed > .01f)
            {
                moveSpeed.y = 0;
                if (velocity < 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
                transform.position = transform.position + (moveSpeed * Time.deltaTime);
            }
            //Moving in the negative direction
            else if (horizontalSpeed < -.01f)
            {
                moveSpeed.y = 0;
                if (velocity > 0)
                {
                    velocity = velocity * -1;
                }
                moveSpeed.x = velocity;
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

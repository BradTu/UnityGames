///<summary>
///Brad Tully 4 October 2017
///This is the rabbit class it has 2 health points
///If the player gets close to it, it will try to run away
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animal
{
    public Vector3[] runPoints;
    public bool looping = false;
    public int runPointCycle = -1;
    public Vector3 currentRunPoint = Vector3.zero;
    private int temp = 0;
    public Animator animator;
    public Collider2D theCollider, circleCol;
    public Sprite bloodSplatter;
    public float xMin, xMax, yMin, yMax;
    public int one;

    // Initialize the waypoints, health, and set it to patrolling
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Waypoints[i] = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        }
        CurrentWaypoint = Waypoints[0];
        isPatrolling = true;
        runVelocity = 8;
        isMoving = true;
        right = transform.localScale.x;
        left = transform.localScale.x * -1;
        theCollider = gameObject.GetComponent<BoxCollider2D>();
        circleCol = gameObject.GetComponent<CircleCollider2D>();
        one = 0;
    }

    //Use this instead to save processing time
    private void FixedUpdate()
    {
        patrol(isPatrolling);
        scatter(isPatrolling);
        if (patrolVelocity == 0 && runVelocity == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        //animator.SetBool("isRunning", isMoving);
    }

    //If the player gets close to the rabbit it will stop patrolling and run
    public void runAwayFromPlayer()
    {
        isPatrolling = false;
        for (int i = 0; i < 3; i++)
        {
            runPoints[i] = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        }
        currentRunPoint = runPoints[0];
        if (currentRunPoint.x < transform.position.x)
        {
            transform.localScale = new Vector3(left, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector3(right, transform.localScale.y);
        }
    }


    //This method moves the animal to its next way point
    public void NextPoint()
    {
        runPointCycle++;
        if (runPointCycle >= runPoints.Length)
        {
            if (looping)
            {
                runPointCycle = runPointCycle % runPoints.Length;
            }
        }
        if (runPointCycle < runPoints.Length)
        {
            currentRunPoint = runPoints[runPointCycle];
        }
    }

    //This checks if the animal has reached its next waypoint if it has it will cycle to the next
    public void scatter(bool patrolling)
    {
        if (patrolling == false)
        {
            if (transform.position != currentRunPoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentRunPoint, runVelocity * Time.deltaTime);
            }
            else
            {
                //runPoints[runPointCycle] = new Vector3(Random.Range(8.0f, 40.0f), Random.Range(8.0f, 40.0f));
                NextPoint();
                if (currentRunPoint.x < transform.position.x)
                {
                    transform.localScale = new Vector3(left, transform.localScale.y);
                }
                else
                {
                    transform.localScale = new Vector3(right, transform.localScale.y);
                }
            }
        }
    }

    // If the player or ai the circular trigger around the rabbit it will begin to run away
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite") || collision.gameObject.tag.Contains("AI"))
        {
            runAwayFromPlayer();
            looping = true;
            Loop = false;
        }
    }

    // If the player or ai exits the circular trigger around the rabbit it will go back to patrolling
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite") || collision.gameObject.tag.Contains("AI"))
        {
            isPatrolling = true;
            looping = false;
            Loop = true;
        }
    }

    //Destroy the rabbit and leave a blood splatter when it is run over
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite") || collision.gameObject.tag.Contains("AI"))
        {
            if (one == 0)
            {
                one++;
                theCollider.enabled = false;
                circleCol.enabled = false;
                gameObject.GetComponent<Animator>().enabled = false;
                transform.localScale = new Vector3(6f, 6f, 6f);
                gameObject.GetComponent<SpriteRenderer>().sprite = bloodSplatter;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                isPatrolling = false;
                patrolVelocity = 0;
                runVelocity = 0;
                looping = false;
            }
        }
    }

}

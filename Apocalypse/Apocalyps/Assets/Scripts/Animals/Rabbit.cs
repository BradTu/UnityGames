///<summary>
///Brad Tully 4 October 2017
///This is the rabbit class it has 2 health points
///If the player gets close to it, it will try to run away
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : AnimalParent
{
    public Vector3[] runPoints;
    public bool looping = false;
    public int runPointCycle = -1;
    public Vector3 currentRunPoint = Vector3.zero;
    private int temp = 0;
    public Animator animator;

    // Initialize the waypoints, health, and set it to patrolling
    void Start()
    {
        health = 2;
        for (int i = 0; i < 3; i++)
        {
            Waypoints[i] = new Vector3(Random.Range(waypointMin, waypointMax), Random.Range(waypointMin, waypointMax));
        }
        CurrentWaypoint = Waypoints[0];
        isPatrolling = true;
        runVelocity = 8;
        isMoving = true;
        right = transform.localScale.x;
        left = transform.localScale.x * -1;
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
        animator.SetBool("isRunning", isMoving);
    }

    //If the player gets close to the rabbit it will stop patrolling and run
    public void runAwayFromPlayer()
    {
        isPatrolling = false;
        for (int i = 0; i < 3; i++)
        {
            runPoints[i] = new Vector3(Random.Range(waypointMin, waypointMax), Random.Range(waypointMin, waypointMax));
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

    // If the player enters the circular trigger around the rabbit it will begin to run away
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runAwayFromPlayer();
            looping = true;
            Loop = false;
        }
    }

    // If the player exits the circular trigger around the rabbit it will go back to patrolling
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPatrolling = true;
            looping = false;
            Loop = true;
        }
    }

    //Decrement health when hit by axe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            health--;
        }
    }

}

///<summary>
///Brad Tully 4 October 2017
///This is the deer class it starts out patrolling has 3 health points
///If the player gets close to it, it will attack
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : AnimalParent
{
    public Player thePlayer;
    public Animator animator;

    // Initialize the waypoints, health, and set it to patrolling
    void Start()
    {
        health = 3;
        for (int i = 0; i < 3; i++)
        {
            Waypoints[i] = new Vector3(Random.Range(waypointMin, waypointMax), Random.Range(waypointMin, waypointMax));
        }
        CurrentWaypoint = Waypoints[0];
        isPatrolling = true;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            thePlayer = playerObject.GetComponent<Player>();
        }
        runVelocity = 3;
        right = transform.localScale.x;
        left = transform.localScale.x * -1;
        isMoving = true;
    }


    //Use this instead to save processing time
    private void FixedUpdate()
    {
        patrol(isPatrolling);
        if (isPatrolling == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, runVelocity * Time.deltaTime);
            //change direction based on which way its moving
            if (thePlayer.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(left, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector3(right, transform.localScale.y);
            }
        }
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

    //If player enters the trigger of the deer it will stop patrolling and attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPatrolling = false;
        }
    }

    //If the player leaves the deer's immediate area the deer will leave them alone and patrol
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPatrolling = true;
            if (CurrentWaypoint.x < transform.position.x)
            {
                transform.localScale = new Vector3(left, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector3(right, transform.localScale.y);
            }
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
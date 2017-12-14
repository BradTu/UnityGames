///<summary>
///Brad Tully 4 October 2017
///This is the parent class of the animals (Deer and Rabbit)
///It will control the patrolling aspect of the animals
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalParent : MonoBehaviour
{
    [HideInInspector]
    public Vector3 moveSpeed;
    [HideInInspector]
    public Vector3 rotation;
    public int runVelocity, patrolVelocity;
    public bool isPatrolling, isMoving;
    [HideInInspector]
    public float xScale, yScale, xScale2, yScale2, zScale;
    public Vector3[] Waypoints;
    public bool Loop = true;
    public int CurrentIdx = -1;
    public Vector3 CurrentWaypoint = Vector3.zero;
    public int health;
    public float waypointMin, waypointMax;
    public float left, right;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Source: https://www.reddit.com/r/Unity2D/comments/2uzhps/enemy_walking_from_point_to_point/
    //This method moves the animal to its next way point
    //Edits by Brad Tully
    public void GetNextWaypoint()
    {
        CurrentIdx++;
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
    //This checks if the animal has reached its next waypoint if it has it will cycle to the next
    //Edits by Brad Tully
    public void patrol(bool patrolling)
    {
        if (isPatrolling == true)
        {
            if (transform.position != CurrentWaypoint)
            {
                if (CurrentWaypoint.x < transform.position.x)
                {
                    transform.localScale = new Vector3(left, transform.localScale.y);
                }
                else
                {
                    transform.localScale = new Vector3(right, transform.localScale.y);
                }
                transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint, patrolVelocity * Time.deltaTime);
            }
            else
            {
                GetNextWaypoint();
                //Change direction based on which way it is moving
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
    }


}

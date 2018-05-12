///<summary>
///Brad Tully 1 March 2018
///This is the parent class of the animals
///It will control the patrolling aspect of the animals
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [HideInInspector]
    public Vector3 moveSpeed;
    [HideInInspector]
    public Vector3 rotation;
    public int runVelocity, patrolVelocity;
    public bool isPatrolling, isMoving;
    [HideInInspector]
    public float xScale, yScale, xScale2, yScale2, zScale, rotZ;
    public Vector3[] Waypoints;
    public bool Loop = true;
    public int CurrentIdx = -1;
    public Vector3 CurrentWaypoint = Vector3.zero;
    private Vector3 difference;
    public int health, increment;
    public float left, right;

    private void Start()
    {
        increment = 0;
    }

    //Change the waypoints that the animal is moving towards
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

    //Moves the animal towards the current waypoint
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
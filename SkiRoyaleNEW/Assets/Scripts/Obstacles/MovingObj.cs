///<summary>
///Brad Tully 8 March 2018
///This is the class for objects that will be moving set pieces and
///can be turned on/ off by players hitting buttons. It is a more simplified
///version of the Animal.cs script
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObj : MonoBehaviour
{
    [HideInInspector]
    public Vector3 moveSpeed;
    [HideInInspector]
    public Vector3 rotation;
    public int runVelocity, patrolVelocity;
    public bool isPatrolling, isMoving;
    public Vector3[] Waypoints;
    public bool Loop = true;
    public int CurrentIdx = -1;
    public Vector3 CurrentWaypoint = Vector3.zero;
    public float waypointMin, waypointMax;

    private void Start()
    {
        
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
                transform.position = Vector3.MoveTowards(transform.position, CurrentWaypoint, patrolVelocity * Time.deltaTime);
            }
            else
            {
                GetNextWaypoint();
            }
        }
    }

    public virtual void turnOff() { }

    public virtual void turnOn() { }


}
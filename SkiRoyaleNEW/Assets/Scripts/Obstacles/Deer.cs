///<summary>
///Brad Tully 1 March 2018
///This is the deer class it patrols around an area and
///will get in the way of the player
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Animal
{
    //public Player thePlayer;
    public Animator animator;

    // Initialize the waypoints, health, and set it to patrolling
    void Start()
    {
        CurrentWaypoint = Waypoints[0];
        isPatrolling = true;
        right = transform.localScale.x;
        left = transform.localScale.x * -1;
        isMoving = true;
    }

    //Use this instead to save processing time
    private void FixedUpdate()
    {
        patrol(isPatrolling);
        animator.SetBool("isRunning", isMoving);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

}

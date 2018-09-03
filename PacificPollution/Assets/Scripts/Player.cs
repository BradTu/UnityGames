//Brad Tully
//5 June 2017
//Script that controls the turtle/ player

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Vector3 moveSpeed;
    public float verticalSpeed;
    public float velocity;
    public int health;
    float increment = 0;
    public AudioClip eatSound;
    public AudioSource source;

    //Allows the player to control the turtle
    public void playerControl()
    {
        //Check vertical movement
        verticalSpeed = Input.GetAxis("Vertical");
        //Positive y direction
        if (verticalSpeed > .01f)
        {
            if (velocity < 0)
            {
                velocity = velocity * -1;
            }
            moveSpeed.y = velocity;
            moveSpeed.x = 0;
            transform.position = transform.position + (moveSpeed * Time.deltaTime);
        }
        //Negative y direction
        else if (verticalSpeed < -.01f)
        {
            if (velocity > 0)
            {
                velocity = velocity * -1;
            }
            moveSpeed.y = velocity;
            moveSpeed.x = 0;
            transform.position = transform.position + (moveSpeed * Time.deltaTime);
        }
        //Stay still
        else
        {
            transform.position = transform.position;
        }
    }

    // Use this for initialization
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerControl();
    }

    //Check if the player comes in contact with trash 
    //When they do decrease health put the trash in the stomach sprite and slow down the player
    void OnTriggerEnter2D(Collider2D col)
    {
        //Allows the spider to climb the web
        if (col.gameObject.tag == "Trash")
        {
            TrashRunner t = col.gameObject.GetComponent<TrashRunner>();
            health = health - 20;
            increment = increment + .5f;
            source.PlayOneShot(eatSound);
            t.speed = 0;
            t.moveSpeed.x = 0;
            t.GetComponent<SpriteRenderer>().sortingOrder = 3;
            t.transform.position = new Vector3(-6, -5 + increment, 0);
            velocity = Mathf.Abs(velocity);
            velocity = velocity - 2;
        }
    }

}

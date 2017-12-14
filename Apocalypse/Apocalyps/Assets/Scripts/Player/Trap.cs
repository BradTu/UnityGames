///<summary>
///Brad Tully 5 October 2017
///This class is a trap and will trap one deer or rabbit
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float xPosition, yPosition;
    public GameController gameController;
    private int temp;


    // Use this for initialization
    void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        temp = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Trap the deer and rabbit if it collides only works once
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (temp == 0)
        {
            if (collision.gameObject.tag == "Deer")
            {
                Deer d = collision.gameObject.GetComponent<Deer>();
                d.runVelocity = 0;
                d.patrolVelocity = 0;
                d.isPatrolling = false;
                d.transform.position = new Vector3(xPosition, yPosition);
                temp++;
            }
            else if (collision.gameObject.tag == "Rabbit")
            {
                Rabbit r = collision.gameObject.GetComponent<Rabbit>();
                r.runVelocity = 0;
                r.patrolVelocity = 0;
                r.isPatrolling = false;
                r.transform.position = new Vector3(xPosition, yPosition);
                temp++;
            }
        }
    }
}

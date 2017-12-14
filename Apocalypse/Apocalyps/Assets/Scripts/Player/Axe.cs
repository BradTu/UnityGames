///<summary>
///Brad Tully 4 October 2017
///This is the class for the axe object, it is a child of the player and has an attack method
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Player
{
    public float thrust;
    public Rigidbody2D rigidBod;


    // Use this for initialization
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }

    // Attack with animation and trigger will fill out when there is an attack animation
    void attack()
    {

    }
}
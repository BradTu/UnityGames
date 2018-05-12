///<summary>
///Made by Brad Tully 9 March 2018
///Blades that move back and forth spinning. They can be turned on or off
///by a button. The deal damage to anyone they hit.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MovingObj {
    private int angle;
    public int startVelocity;
    private bool isActive;

	// Use this for initialization
	void Start () {
        angle = 0;
        startVelocity = patrolVelocity;
        isPatrolling = true;
        isActive = false;
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        Spin();
        patrol(isPatrolling);
    }
    

    //Rotate the blades
    void Spin()
    {
        angle += 3;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    //Continue moving them
    public override void turnOn()
    {
        patrolVelocity = startVelocity;
        isActive = false;
    }

    //Stop the blades from moving
    public override void turnOff()
    {
        if (isActive == false)
        {
            patrolVelocity = 0;
            StartCoroutine("timeOff");
            isActive = true;
        }
    }

    //Wait for x amount of time after someone hits the button
    private IEnumerator timeOff()
    {
        bool tf = true;
        int i = 0;
        while (tf == true)
        {
            Debug.Log(i);
            if (i == 3)
            {
                tf = false;
                turnOn();
            }
            i++;
            yield return new WaitForSeconds(1f);
        }
    }

    //Take health away from players/ ai when they hit the saw blade
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            collision.gameObject.GetComponentInParent<Player>().health -= 3;
        }
        else if (collision.gameObject.tag.Contains("AI"))
        {
            collision.gameObject.GetComponent<AdvancedAI>().health -= 2;
        }
    }
}

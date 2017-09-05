//Made by Brad Tully
//Parasitic Passage
//This is the player object controls the parasite

using UnityEngine;
using System.Collections;

public class Parasite : Bug {
    public bool isPuked = false;
    public int count = 0;
    int dist = 0;
    ParticleSystem ps;

    //Makes a sound when launched
    public void playSpitSound()
    {
        source.PlayOneShot(spitSound);
        ps.Emit(20);
    }

    // Use this for initialization
    void Start () {
        isControlled = true;
        isLaunched = false;
        source = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        //Check if the player is controlling the parasite
        playerControl(isControlled);

        //Check if the parasite is puked out by a fly
        if (isPuked == true)
        {
            moveSpeed.x = 0;
            moveSpeed.y = -10;
            moveSpeed.z = 0;
            transform.position = transform.position + (moveSpeed * Time.deltaTime);
        }
        //Check if the parasite is launched by the other bugs can move a set amount
        if (isLaunched == true && dist < 50)
        {
            if (direction < 0 && velocity > 0)
            {
                velocity = -1 * velocity;
            }
            if (direction > 0 && velocity < 0)
            {
                velocity = velocity * -1;
            }
            moveSpeed.y = 0;
            moveSpeed.x = velocity;
            transform.position = transform.position + (moveSpeed * Time.deltaTime);
            dist++;
        }
        //Reset the distance variable if it reached max dist or is in another bug
        if (dist == 50 || isLaunched == false)
        {
            //ps.Stop();
            isLaunched = false;
            dist = 0;
        }
    }


}

///<summary>
///Made by Brad Tully; 27 February 2018
///This is the script for the speed boost platform.
///When the player or ai ride over this it will give them a 
///temporary speed boost.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour {
    public int speedBoost;

    //When a player or ai rides over the platform speed them up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Sprite"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedBoost);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            collision.gameObject.GetComponentInParent<Player>().Invoke("StopSpeedBoost", .75f);
        }
		else if (collision.tag.Equals("AdvancedAI1") || collision.tag.Equals("AdvancedAI2") || collision.tag.Equals("AdvancedAI3") || 
			collision.tag.Equals("AdvancedAI4") || collision.tag.Equals("AdvancedAI5") || collision.tag.Equals("AdvancedAI6") || 
			collision.tag.Equals("AdvancedAI7"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedBoost);
            collision.gameObject.GetComponent<AdvancedAI>().Invoke("StopSpeedBoost", .25f);
        }
    }
}

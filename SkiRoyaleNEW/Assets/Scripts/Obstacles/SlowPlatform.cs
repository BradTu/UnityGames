///<summary>
///Made by Brad Tully; 27 February 2018
///This is the script for the slow down platform.
///When the player or ai ride over this it will slow them
///down temporarily.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlatform : MonoBehaviour {
    public int slowDown;

    //When a player or ai rides over the platform slow them down
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Sprite"))
        {
            if (!collision.gameObject.GetComponent<UseItem>().isInvinc){
                collision.gameObject.GetComponentInParent<Player>().forwardVelocity = slowDown;
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }
		else if (collision.tag.Equals("AdvancedAI1") || collision.tag.Equals("AdvancedAI2") || collision.tag.Equals("AdvancedAI3") ||
				collision.tag.Equals("AdvancedAI4") || collision.tag.Equals("AdvancedAI15") || collision.tag.Equals("AdvancedAI6") ||
				collision.tag.Equals("AdvancedAI7"))
        {
            if (!collision.gameObject.GetComponent<AdvancedAIItemSystem>().isInvinc){
                collision.gameObject.GetComponent<AdvancedAI>().forwardVelocity = slowDown;
            }
        }
    }
}

/**
 * This script keeps track of AI collision/triggers
 * Jason Komoda 11/2/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//wait to make it a child for now
public class AICollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when AIs enter sludge triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Sludge"){
            gameObject.GetComponent<BasicAI>().forwardVelocity = 5;
        }
    }

    //when AIs stay in sludge triggers
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Sludge"){
            gameObject.GetComponent<BasicAI>().forwardVelocity = 5;
        }
    }
}

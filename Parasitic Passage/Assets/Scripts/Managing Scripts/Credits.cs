//Made by Brad Tully
//Parasitic Passage
//Return to start screen

using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.R) == true)
        {
            Application.LoadLevel("Start Screen");
        }
	}
}

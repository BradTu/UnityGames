/**
 * This script keeps track of when to show/hide the sun depending if weather mode is drought or not
 * Jason Komoda 10/4/17
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {

		//show sun if weather is drought
		if (gameController.isDrought)
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		//else hide sun
		else {
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {
	
	public GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text = gameController.playerHealth.ToString();
	}
}

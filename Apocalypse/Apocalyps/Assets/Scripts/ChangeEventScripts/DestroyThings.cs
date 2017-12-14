using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThings : MonoBehaviour {
	public GameController gameController;

	private int randomNum;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}


	void OnEnable(){
		GameController.OnWeatherChange += Kill;
	}

	void OnDisable(){
		GameController.OnWeatherChange -= Kill;
	}

	void Kill(){
		if (gameController.isDrought == true) {
			if (this.gameObject.tag == "Water") {
				Destroy (this.gameObject);
			}
			randomNum = Random.Range (1, 5);
			switch (randomNum) {
			case 1:
				Destroy (this.gameObject);
				break;

			case 2:
				Destroy (this.gameObject);
				break;

			case 3:
				Destroy (this.gameObject);
				break;

			case 4:
				//Debug.Log ("SURVIVED");
				break;

			default:
				Debug.Log ("no number");
				break;
			}
		}
	}
}

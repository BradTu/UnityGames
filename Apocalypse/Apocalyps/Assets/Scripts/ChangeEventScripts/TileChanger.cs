using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChanger : MonoBehaviour {

	public GameController gameController;

	public SpriteRenderer theSpriteRenderer;

	public Sprite dirt;

	public Sprite grass;

	private int randomNum;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
	}
	
	void OnEnable(){
		GameController.OnWeatherChange += Change;
	}

	void OnDisable(){
		GameController.OnWeatherChange -= Change;
	}

	void Change(){
		if (gameController.isDrought == true) {
			randomNum = Random.Range (1, 5);
			switch (randomNum) {
			case 1:
				theSpriteRenderer.sprite = dirt;
				break;

			case 2:
				theSpriteRenderer.sprite = dirt;
				break;

			case 3:
				theSpriteRenderer.sprite = dirt;
				break;

			case 4:
				theSpriteRenderer.sprite = grass;
				break;

			default:
				Debug.Log ("no number");
				break;
			}
		}
		if (gameController.isStorm == true) {
			randomNum = Random.Range (1, 5);
			switch (randomNum) {
			case 1:
				theSpriteRenderer.sprite = grass;
				break;

			case 2:
				theSpriteRenderer.sprite = dirt;
				break;

			case 3:
				theSpriteRenderer.sprite = dirt;
				break;

			case 4:
				theSpriteRenderer.sprite = grass;
				break;

			default:
				Debug.Log ("no number");
				break;
			}
		}
	}
}

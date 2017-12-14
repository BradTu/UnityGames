using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public GameController gameController;

	public Spear spear;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}


	}

	public void Twig(){
		if (gameController.twigCount > 3) {
			gameController.axeCount++;
			gameController.twigCount -= 3;
			spear.hitCount = 10;

		}
	}
	
	public void Water(){
		if (gameController.waterCount >= 1) {
		if (gameController.playerThirst >= 7) {
			gameController.playerThirst = 7;
		} else {
			gameController.playerThirst++;
                gameController.waterCount--;
		}

		}
	}

	public void Meat(){
		if (gameController.foodCount >= 1) {
			if (gameController.playerHunger >= 7) {
				gameController.playerHunger = 7;
			} else {
				gameController.playerHunger++;
                gameController.foodCount--;
			}
		}
	}

	public void Axe(){

	}

	public void Spear(){

	}
}

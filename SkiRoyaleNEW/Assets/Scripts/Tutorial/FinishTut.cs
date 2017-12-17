using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTut : MonoBehaviour {

    public TutorialManager2 gameController;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<TutorialManager2>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1Sprite")
        {
            gameController.theText.text = "Good job! Press 'A' to move to the next tutorial or 'B' to redo this one.";
            gameController.gameOver = true;
            Time.timeScale = .01f;
        }
    }
}

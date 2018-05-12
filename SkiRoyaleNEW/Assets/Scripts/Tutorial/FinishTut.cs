///<summary>
///Brad Tully 
///This class controls the end of the hard turn tutorials
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTut : MonoBehaviour {

    public TutorialManager2 gameController;
    public Vector3 finSpot;

	// Use this for initialization
	void Start () {
        /*GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<TutorialManager2>();
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //When the player crosses the finish line move them to a finish spot
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite"))
        {
            //gameController.gameOver = true;
            collision.gameObject.GetComponentInParent<Player>().finished = true;
            collision.gameObject.GetComponentInParent<Player>().begin = false;
            collision.gameObject.GetComponentInParent<Player>().finishSpot = finSpot;
            collision.gameObject.GetComponentInParent<Player>().StopAllCoroutines();
            collision.gameObject.GetComponentInParent<Player>().forwardVelocity = 0;
            collision.gameObject.GetComponentInParent<Player>().sidewaysVelocity = 0;
        }
    }
}

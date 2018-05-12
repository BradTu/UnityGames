/**
 * This script manages the death sequence when players die
 * Jason Komoda 3/15/18
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject blackScreen;
    private float time = 2.0f;
    private bool startedFade = false;
    public GameController gameController;
	public Transform deathPoint;

    // Use this for initialization
    void Start()
    {
        //Get game controller reference
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checking to see if player dies
		if(gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player>().health <= 0 && gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player>().isDead == false){
            gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player>().isDead = true;
			gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player> ().Stop ();
			gameObject.transform.parent.transform.parent.transform.GetChild(0).transform.position = deathPoint.position;
			gameObject.transform.parent.transform.parent.transform.GetChild (0).GetComponent<Animator> ().SetBool ("Dead", true);
			gameController.deadPlayers.Insert (0, gameObject.transform.parent.transform.parent.GetComponent<Player>());
            gameController.activePlayers.Remove(gameObject.transform.parent.transform.parent.transform.GetChild(0).gameObject);
			gameController.deadPlayersNum++;
			gameObject.transform.parent.transform.parent.transform.GetChild (0).GetComponent<UseItem> ().currentItem = null;
			gameObject.transform.parent.transform.parent.transform.GetChild (0).GetComponent<UseItem> ().canUseItem = false;
			gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player> ().deadFinish = true;
        }

        //start to fade screen if player dies
		if(!startedFade && gameObject.transform.parent.transform.parent.gameObject.GetComponent<Player>().isDead == true){
			FadeToBlack();
			startedFade = true;
		}
    }

    //fade screen
    void FadeToBlack()
    {
        blackScreen.GetComponent<Image>().enabled = true;
        blackScreen.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        blackScreen.GetComponent<Image>().CrossFadeAlpha(1.0f, time, false);
    }
}

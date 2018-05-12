using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAI : MonoBehaviour {

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
		if (gameObject.GetComponent<AdvancedAI>().health <= 0 && gameObject.GetComponent<AdvancedAI>().isDead == false)
        {
            gameObject.GetComponent<AdvancedAI>().isDead = true;
			gameObject.GetComponent<AdvancedAI> ().Stop ();
			gameObject.GetComponent<Animator> ().SetBool ("Dead", true);
			gameObject.transform.position = deathPoint.position;
			gameObject.GetComponent<AdvancedAI> ().deadFinish = true;
			gameObject.GetComponent<AdvancedAIItemSystem> ().currentItem = null;
			gameObject.GetComponent<AdvancedAIItemSystem> ().preparingThrow = false;
			gameController.deadPlayers.Insert(0, gameObject.GetComponent<AdvancedAI>());
            gameController.activePlayers.Remove(gameObject);
			gameController.deadPlayersNum++;
            for (int i = 0; i < gameController.activePlayers.Count; i++)
            {
                Debug.Log(gameController.activePlayers[i].name);
            }
        }
    }
}

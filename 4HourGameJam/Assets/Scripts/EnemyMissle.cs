using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : MonoBehaviour {

	public Vector3 movementSpeed;
	public GameController gameController;

    // Use this for initialization
    void Start () {
		movementSpeed = new Vector3 (-15.0f, 0, 0);
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		transform.position = transform.position + (movementSpeed * Time.deltaTime);

		if (transform.position.x < -11) {
			Destroy (gameObject);
		}
		if (transform.position.x > 11) {
			Destroy (gameObject);
		}
		if (transform.position.y < -11) {
			Destroy (gameObject);
		}
		if (transform.position.y > 11) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
            Destroy(gameObject);
		}
	}
}

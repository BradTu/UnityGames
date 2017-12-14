using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

	public float movementSpeed;
	public GameObject prefab;

    void Start () {

	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);

		if (transform.position.x < -11) {
			Destroy (prefab);
		}
		if (transform.position.x > 11) {
			Destroy (prefab);
		}
		if (transform.position.y < -11) {
			Destroy (prefab);
		}
		if (transform.position.y > 11) {
			Destroy (prefab);
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		if (collider.gameObject.tag == "Enemy") {
            Destroy (prefab);
		}

		if (collider.gameObject.tag == "SpinningEnemy") {
            Destroy (prefab);
		}

		if (collider.gameObject.tag == "ShootingEnemy") {
            Destroy (prefab);
		}

		if (collider.gameObject.tag == "Target") {;
            Destroy (prefab);
		}

		if (collider.gameObject.tag == "Earth") {
            Destroy (prefab);
		}

		if (collider.gameObject.tag == "EndOfLevel") {
            Destroy (prefab);
		}

	}

}

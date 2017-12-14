///<summary>
///Brad Tully 4 October 2017
///This is the class for the spear object, it is a child of the player and has an attack method
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Player {
    public float thrust;
    public Rigidbody2D rigidBod;

	public GameObject meat;

	public GameObject twig;

	public bool attacking = false;

	public float attackSpeed;

	public float rotateSpeed;

	private Vector3 theVector3;

	public int hitCount;


    // Use this for initialization
    void Start () {
        rigidBod = GetComponent<Rigidbody2D>();
		attacking = false;
		theVector3 = new Vector3 (0.078f, -0.023f, 0);
		hitCount = 10;
    }

	
	// Update is called once per frame
	void Update () {
		if (hitCount > 0) {
			attack ();
		} else {
			GetComponent<CircleCollider2D> ().enabled = false;
		}

	}

    // Attack with animation and trigger will fill out when there is an attack animation
    void attack()
    {
		if (Input.GetMouseButtonUp (0) == true) {
			attacking = true;
		}
		if (attacking == true) {
			transform.Translate (Vector3.right * attackSpeed * Time.deltaTime, Space.Self);
			GetComponent<CircleCollider2D> ().enabled = true;
		}

		if (transform.localPosition.x >= theVector3.x) {
			transform.localPosition = new Vector3 (-0.085f, -0.023f, 0);
			attacking = false;
			GetComponent<CircleCollider2D> ().enabled = false;
		}
    }

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Deer" || collision.gameObject.tag == "Rabbit") {
			Instantiate(meat, collision.gameObject.transform.position, Quaternion.identity);
			Destroy (collision.gameObject);
			hitCount--;
		}

		if (collision.gameObject.tag == "Tree"){
			Instantiate(twig, collision.gameObject.transform.position, Quaternion.identity);
			Destroy (collision.gameObject);
			hitCount--;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the player bullet move
public class Rotate : MonoBehaviour {
	public Vector3 difference;

	public float rotZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
	}
}

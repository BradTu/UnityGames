using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera control.
/// This is the scrip that will control that camera following the player
/// </summary>

public class CameraControl : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if ((player.transform.position.y <= 1) && (player.transform.position.x <= 4)) {
			this.transform.position = new Vector3 (6, 2.2f, this.transform.position.z);
		} else if ((player.transform.position.y >= 30.75) && (player.transform.position.x <= 4)) {
			this.transform.position = new Vector3 (6, 32, this.transform.position.z);
		} else if ((player.transform.position.y >= 30.75) && (player.transform.position.x >= 26)) {
			this.transform.position = new Vector3 (28.1f, 32, this.transform.position.z);
		}
		else if ((player.transform.position.y <= 1) && (player.transform.position.x >= 26)) {
			this.transform.position = new Vector3 (28.1f, 2.2f, this.transform.position.z);
		}
		else if (player.transform.position.y <= 1) {
			this.transform.position = new Vector3 (player.transform.position.x + offset.x, 2.2f, this.transform.position.z);
		} else if (player.transform.position.x <= 4) {
			this.transform.position = new Vector3 (6, player.transform.position.y + offset.y, this.transform.position.z);
		} else if (player.transform.position.y >= 30.75) {
			this.transform.position = new Vector3 (player.transform.position.x + offset.x, 32, this.transform.position.z);
		} else if (player.transform.position.x >= 26) {
			this.transform.position = new Vector3 (28.1f, player.transform.position.y + offset.y, this.transform.position.z);
		}else {
			transform.position = player.transform.position + offset;
		}
	}
}

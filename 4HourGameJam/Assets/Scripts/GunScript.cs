using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {


	public Transform bulletPosition;

	public Transform bulletSpawn;

	public float inputHoldDelay = 0.1f;

	private WaitForSeconds inputHoldWait;  

	public bool fire;

	void Start(){
		fire = true;

		inputHoldWait = new WaitForSeconds (inputHoldDelay);
	}

	void Update(){
		Spawn();
	}


	void Spawn(){
		if (fire == false) {
			return;
		}
		
		if ((Input.GetMouseButtonDown (0) == true)) {
			Instantiate(bulletSpawn, bulletPosition.position, bulletPosition.rotation);
			StartCoroutine (WaitForFire());
		}
	}

	 IEnumerator WaitForFire (){
		fire = false;

		yield return inputHoldWait;

		fire = true;
	}

}

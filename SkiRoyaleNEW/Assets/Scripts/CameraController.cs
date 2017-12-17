///<summary>
///Brad Tully
///11 October 2017
///This controls the position of the camera
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	/// <summary>
	/// The player.
	/// </summary>
	public GameObject thePlayer;
	/// <summary>
	/// The player sprite.
	/// </summary>
	public GameObject thePlayerSprite;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3 (thePlayerSprite.transform.position.x, thePlayerSprite.transform.position.y + 7f, -10f);
	}
}

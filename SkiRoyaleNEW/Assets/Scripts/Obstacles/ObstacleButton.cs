///<summary>
///Made by Brad Tully 8 March 2018
///This is the script for the button that players can hit with their
///pole to turn on/ off different interactable obstacles on the track
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleButton : MonoBehaviour {
    //True to turn things on, false to turn them off
    public bool onOrOff;
    //List of obstacles that can be turned on/ off
    public List<MovingObj> theObstacles;

	public Collider2D otherCol;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Coll(){
		if (onOrOff == true)
		{
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            for (int i = 0; i < theObstacles.Capacity; i++)
			{
				theObstacles[i].turnOn();
			}
		}
		else
		{
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            for (int i = 0; i < theObstacles.Capacity; i++)
			{
				theObstacles[i].turnOff();
			}
		}
	}
}

///<summary>
///Made by Brad Tully 9 March 2018
///This object is two bars with spikes on the end
///that move back and forth crushing anything in between
///it can be turned on/ off with a button.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MovingObj {

	// Use this for initialization
	void Start () {
        isPatrolling = true;
	}
	
	// Update is called once per frame
	void Update () {
        patrol(isPatrolling);
	}


}

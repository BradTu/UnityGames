///<summary>
///Made by Brad Tully 4 April 2018
///This script will execute a few of the functions in grand prix for the final scoreboard scene
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGrandPrix : MonoBehaviour {
    GrandPrix gp;
	
	void Start () {
        GameObject gpObj = GameObject.FindWithTag("GrandPrix");
        if (gpObj != null)
        {
            gp = gpObj.GetComponent<GrandPrix>();
        }
        gp.FinalPlacementSort();
        gp.PostScore();
        gp.DestroyThisObject();
    }
}

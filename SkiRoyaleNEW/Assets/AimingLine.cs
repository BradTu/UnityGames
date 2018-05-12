using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingLine : MonoBehaviour {

    private LineRenderer line;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //position where line starts (player sprite position)
        line.SetPosition(0, gameObject.transform.parent.transform.parent.GetChild(0).transform.position);

        //position where line ends (direction player sprite is facing)
        line.SetPosition(1, gameObject.transform.parent.transform.parent.GetChild(0).transform.position +
                                gameObject.transform.parent.transform.parent.GetChild(0).transform.up *28);
	}
}

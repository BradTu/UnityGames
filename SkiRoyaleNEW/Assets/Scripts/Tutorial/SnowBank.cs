///<summary>
///Brad Tully
///If the player hits a snow bank restart the tutorial level
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowBank : MonoBehaviour {
    public string thisScene;
    public Vector3 startingSpot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Sprite"))
        {
            collision.gameObject.transform.position = startingSpot;
            collision.gameObject.GetComponentInParent<Player>().begin = false;
            collision.gameObject.GetComponentInParent<Player>().forwardVelocity = 0f;
            collision.gameObject.GetComponentInParent<Player>().sidewaysVelocity = 0f;
            collision.gameObject.GetComponentInParent<Player>().direction = 0f;
        }
    }
}

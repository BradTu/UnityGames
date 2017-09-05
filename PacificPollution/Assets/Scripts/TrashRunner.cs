//Brad Tully
//5 June 2017
//Piece of trash in scene two give them a random rotation and speed moving to the left

using UnityEngine;
using System.Collections;

public class TrashRunner : MonoBehaviour {
    public float speed;
    public Vector3 moveSpeed;

	// Use this for initialization
	void Start () {
        speed = Random.Range(-1, -12);
        moveSpeed.x = speed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + (moveSpeed * Time.deltaTime);
        if (transform.position.x <= -7)
        {
            Destroy(gameObject);
        }
    }
}

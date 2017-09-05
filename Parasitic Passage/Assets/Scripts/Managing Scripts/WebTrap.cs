//Made by Brad Tully
//Parasitic Passage
//Spawns a web in an area unknown to the player

using UnityEngine;
using System.Collections;

public class WebTrap : MonoBehaviour
{
    public GameObject webPrefab;
    protected GameObject createdWeb;
    Vector3 webPosition;
    Quaternion theRotation;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn the web when the player (parasite) enters the trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            theRotation.z = 0;
            theRotation.y = 0;
            theRotation.x = 0;
            theRotation.w = 0;
            //Determine where the player is and spawn a web there
            webPosition.x = col.transform.position.x;
            webPosition.y = col.transform.position.y;
            webPosition.z = 0;
            Instantiate(webPrefab, webPosition, theRotation);
        }
    }
}
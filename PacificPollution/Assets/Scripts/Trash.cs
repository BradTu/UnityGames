//Brad Tully
//5 June 2017
//Trash object for scene one deletes and increments score when pressed

using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour {
    Vector3 rotationSpeed;
    Vector3 moveSpeed;
    float angle;
    public AudioClip pickupSound;
    public AudioSource sourceObj;

    // Use this for initialization
    void Start () {
        rotationSpeed.x = 0;
        rotationSpeed.y = 0;
        rotationSpeed.z = Random.Range(-3.0f, 3.0f);
        angle = Random.Range(1.0f, 3.0f);

        GameObject source = GameObject.FindWithTag("Sound");
        if (source != null)
        {
            sourceObj = source.GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationSpeed * Time.deltaTime, angle);
	}

    void OnMouseDown()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Manager");
        ManagerOne mo = obj.GetComponent<ManagerOne>();
        if (mo.gameOver == false)
        {
            mo.trashCount = mo.trashCount + 1;
            sourceObj.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}

//Brad Tully
//5 June 2017
//Spawns trash objects on the right side of the screen in scene two

using UnityEngine;
using System.Collections;

public class TrashPrefab : MonoBehaviour {
    public GameObject trashPrefab;
    public GameObject trashPrefab2;
    public GameObject trashPrefab3;
    public GameObject trashPrefab4;
    public GameObject trashPrefab5;
    public float spawnRatio;
    Quaternion theRotation;
    float rand;

    //Spawns random piece of trash in a random position right of the screen with random rotation
    public IEnumerator trashSpawn(float ratio)
    {
        while (true)
        {
            rand = Random.Range(1, 6);
            Vector3 position = new Vector3(8, Random.Range(-5.0f, 2.25f), 0);
            theRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            if (rand == 1)
            {
                Instantiate(trashPrefab, position, theRotation);
            }
            else if (rand == 2)
            {
                Instantiate(trashPrefab2, position, theRotation);
            }
            else if (rand == 3)
            {
                Instantiate(trashPrefab3, position, theRotation);
            }
            else if (rand == 4)
            {
                Instantiate(trashPrefab4, position, theRotation);
            }
            else if (rand == 5)
            {
                Instantiate(trashPrefab5, position, theRotation);
            }
            yield return new WaitForSeconds(ratio + .25f);
        }
    }

    // Use this for initialization
    void Start () {
        spawnRatio = PlayerPrefs.GetFloat("TheRatio", 0);
        StartCoroutine("trashSpawn", spawnRatio);
	}
	
	// Update is called once per frame
	void Update () {
        trashSpawn(spawnRatio);
	}
}

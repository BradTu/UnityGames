//Brad Tully
//5 June 2017
//Manages scene two

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerTwo : MonoBehaviour {
    public int health;
    double score;
    public Text scoreText;
    public Text healthText;
    public Text endLevel;

    //Keeps track of the score increases by 10 every second
    private IEnumerator scoreTracker()
    {
        while (true)
        {
            score = score + 10;
            scoreText.text = score + " Points";
            yield return new WaitForSeconds(1);
        }
    }

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        StartCoroutine("scoreTracker");
    }
	
	// Update is called once per frame
	void Update () {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        Player p = obj.GetComponent<Player>();
        health = p.health;
        healthText.text = "Health: " + health + " / 5";
        if (p.health <= 0)
        {
            endLevel.text = "There is an estimated 5.25 trillion pieces of plastic garbage in the ocean." +
                 " 85% of plastic used isn't recycled. About 10% of garbage ends up in the ocean. (National Geographic)" +
                 " An estimated 100,000 sea mammals and sea turtles die each year from plastic in the ocean. (Whale and Dolphin Conservation) " +
                 "Do your part, recycle and properly dispose of waste. Drink water from the tap with reusable water bottles." +
                 " To restart from the trash pickup scene press 'R', to restart this scene press 'P'." + 
                 "Press 'M' to return to the start screen.";
            StopCoroutine("scoreTracker");
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel("Pickup Scene");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                Application.LoadLevel("Start Screen");
            }
        }
        scoreTracker();
    }
}

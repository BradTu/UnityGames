//Brad Tully
//5 June 2017
//Manages scene two

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerTwo : MonoBehaviour {
    public int health;
    double time;
    public Text scoreText;
    public Text healthText;
    public Text endLevel;
    public Text trashScore;
    public Text levelScore;
    public GameObject img;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        time = 30;
        StartCoroutine("countdown");
    }
	
	// Update is called once per frame
	void Update () {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        Player p = obj.GetComponent<Player>();
        health = p.health;
        healthText.text = "Health: " + health + " %";
        if (p.health <= 0)
        {
            StopCoroutine("countdown");
            Time.timeScale = 0;
            img.SetActive(true);
            endLevel.text = "Sorry you didn't make the journey :-( Press the space bar to return to the menu.";
            trashScore.text = "Trash picked up: " + PlayerPrefs.GetFloat("TotalTrash");
            levelScore.text = "Level: " + PlayerPrefs.GetInt("LevelValue");
            if (Input.GetKeyDown("space"))
            {
                if (PlayerPrefs.GetInt("LevelValue") > PlayerPrefs.GetInt("LevelRecord"))
                {
                    PlayerPrefs.SetInt("LevelRecord", PlayerPrefs.GetInt("LevelValue"));
                }
                if (PlayerPrefs.GetFloat("TotalTrash") > PlayerPrefs.GetFloat("TrashRecord"))
                {
                    PlayerPrefs.SetFloat("TrashRecord", PlayerPrefs.GetFloat("TotalTrash"));
                }
                PlayerPrefs.SetInt("LevelValue", 1);
                PlayerPrefs.SetFloat("TotalTrash", 0f);
                SceneManager.LoadScene("Start Screen");
            }
        }
        else if (time <= 0)
        {
            StopCoroutine("countdown");
            Time.timeScale = 0;
            img.SetActive(true);
            endLevel.text = "Good Job! Press the space bar to continue.";
            if (Input.GetKeyDown("space"))
            {
                PlayerPrefs.SetInt("LevelValue", PlayerPrefs.GetInt("LevelValue") + 1);
                SceneManager.LoadScene("Pickup Scene");
            }
        }
        countdown();
    }

    //Keeps track of the score increases by 10 every second
    private IEnumerator countdown()
    {
        while (true)
        {
            time--;
            scoreText.text = "Time Left: " + time + " sec";
            yield return new WaitForSeconds(1);
        }
    }
}

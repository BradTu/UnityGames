/**
 * This script manages each player/ai health bar throughout the race
 * Jason Komoda 3/27/18
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Slider healthBar;

	// Use this for initialization
	void Start () {
        healthBar.maxValue = 100;
        healthBar.minValue = 0;
        if(gameObject.transform.parent.transform.parent.gameObject.tag.Contains("Sprite")){
            healthBar.value = gameObject.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<Player>().health;
        }
        else{
			//Debug.Log(gameObject.transform.parent.transform.parent.gameObject.GetComponent<AdvancedAI>().health);
            healthBar.value = gameObject.transform.parent.transform.parent.gameObject.GetComponent<AdvancedAI>().health;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.parent.transform.parent.gameObject.tag.Contains("Sprite"))
        {
            healthBar.value = gameObject.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<Player>().health;
        }
        else
        {
            healthBar.value = gameObject.transform.parent.transform.parent.gameObject.GetComponent<AdvancedAI>().health;
        }
	}
}

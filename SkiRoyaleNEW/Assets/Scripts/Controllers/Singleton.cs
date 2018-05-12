/**
 * This script is uses the Singleton to see if gameObjects already exist in scenes
 * Jason Komoda 11/9/17
 */

using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

    private static Singleton instance = null;
    public static Singleton Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Ski_Race1" || SceneManager.GetActiveScene().name == "Ski_Race2" || 
           SceneManager.GetActiveScene().name == "Ski_Race3"){
            Destroy(this.gameObject);
        }
    }
}

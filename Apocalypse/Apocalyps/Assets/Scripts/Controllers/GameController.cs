using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Game controller.
/// this is the script that controls much of the game logic and board set up
/// </summary>
public class GameController : MonoBehaviour {
	public Text amountTextTwig;
	public Text amountTextWater;
	public Text amountTextMeat;
	public Text amountTextAxe;
	public Text amountTextSpear;
    public Text amountTextHunger;
    public Text amountTextThirst;
    public Text hsTimer;
    public Text youLose;

    public LevelLayout layout;

	//these are to have the numbers for the amount of stuff in the inventory
	public int twigCount = 0;
	public int axeCount = 1;
	public int waterCount = 0;
	public int foodCount = 0;
	public int spearCount = 0;

	public int trapCount;
    public int hsTimerSeconds;


	public int playerHealth, playerHunger, playerThirst;
	public float gameTimer;
	public bool timerStarted;

	public bool isStorm;
	public bool isDrought;
	public bool spawnFire;

	//variables used for weather change timings
	private float nextActionTime = 0.0f;
	public float period = 10.0f;

	public delegate void WeatherChange ();
	public static event WeatherChange OnWeatherChange;

	public Spear theSpear;

	public GameObject axeObject;

	// Use this for initialization
	void Start () {
		timerStarted = true;
		isStorm = true;
		playerHealth = 100;
        playerHunger = 7;
        playerThirst = 7;
        StartCoroutine("hungerThirstTimer", hsTimerSeconds);
    }

	void Awake(){
		layout = GetComponent<LevelLayout> ();
		InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		

		
		//increases game timer every second
		if (timerStarted){
			gameTimer += Time.deltaTime;
		}

		//change weather condition depending on time
		if (Time.time > nextActionTime)
		{
			nextActionTime = Time.time + period;
			if (isStorm && gameTimer >= 10)
			{
				isDrought = true;
				//spawnFire = true;
				isStorm = false;
				layout.Respawn ();
				OnWeatherChange ();
			}
			else {
				isStorm = true;
				isDrought = false;
				OnWeatherChange ();
				layout.Respawn ();
			}
		}
        hungerThirstTimer(hsTimerSeconds);

        UpdateInventoryText ();

        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            youLose.text = "Sorry You Died :-(";
        }
		AxeLogic ();

	}
	/// <summary>
	/// Inits the game.
	/// </summary>
	void InitGame(){
		layout.SetUpScene ();
	}

	void AxeLogic(){
		
		if (theSpear.hitCount <= 0) {
			if (axeCount <= 0) {
				axeCount = 0;
				return;
			}
			axeCount--;
			if (axeCount > 0) {
				theSpear.hitCount = 10;
			} else {
				axeCount = 0;
				axeObject.SetActive (false);
			}
		} else {
			axeObject.SetActive (true);
		}
	}

	//shows timer on screen in 0:00 format
	void OnGUI()
	{
		int minutes = Mathf.FloorToInt(gameTimer / 60F);
		int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
		string formatTime = string.Format("{0:0}:{1:00}", minutes, seconds);

		GUI.color = Color.black;
		GUI.Label(new Rect(10, 10, 250, 100), formatTime);
	}



	void UpdateInventoryText(){
		amountTextTwig.text = "" + twigCount;
		amountTextWater.text = "" + waterCount;
		amountTextMeat.text = "" + foodCount;
		amountTextAxe.text = "" + axeCount;
		amountTextSpear.text = "" + spearCount;
        amountTextHunger.text = "Hunger: " + playerHunger.ToString();
        amountTextThirst.text = "Thirst: " + playerThirst.ToString();
	}


    //Timer counts down from a specified amount of seconds
    //When it runs out hunger and thirst will be decremented
    private IEnumerator hungerThirstTimer(int time)
    {
        //Decrease time and set that time to the GUI
        while (time >= 0)
        {
            time--;
            hsTimer.text = "Timer: " + time;
            yield return new WaitForSeconds(1);
            //When time hits 0 reset it and decrement hunger and thirst
            //If they're at 0 decrement health
            if (time <= 0)
            {
                time = hsTimerSeconds;
                //Decrement hunger
                if (playerHunger > 0)
                {
                    playerHunger--;
                    //Increase health
                    if (playerHealth <= 90)
                    {
                        playerHealth += 5;
                    }
                }
                //At 0 decrement health
                else
                {
                    playerHealth -= 10;
                }
                //Decrement thirst
                if (playerThirst > 0)
                {
                    playerThirst--;
                    //Increase health
                    if (playerHealth <= 90)
                    {
                        playerHealth += 5;
                    }
                }
                //At 0 decrement health
                else
                {
                    playerHealth -= 10;
                }

            }
        }
    }


}

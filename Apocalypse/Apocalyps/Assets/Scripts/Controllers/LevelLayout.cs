using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// Level layout.
/// This is the script that generates the grid that is the world.
/// </summary>

//this is to make this go off in editor mode for testing
//[ExecuteInEditMode]
public class LevelLayout : MonoBehaviour {
	/// <summary>
	/// Count.
	/// This is a struct that you can set the min and max of the number of objects spawned.
	/// </summary>
	[Serializable]
	public class Count{
		public int theMax;
		public int theMin;

		public Count(int min, int max){
			theMax = max;
			theMin = min;
		}
	}

	public GameController gameController;
	public int theColumns;
	public int theRows;
	public int objectCount;
	public Count animalCount;
	public Count treeCount;
	public Count waterCount;
	public Count fireCount;
	public GameObject[] grassTiles;
	public GameObject[] waterTiles;
	public GameObject boarderTiles;
	public GameObject[] animal;
	public GameObject[] tree;
	public GameObject[] fire;
	public List<Vector2> gridPos;
	public List<GameObject> fireList;

	//this is used to keep the inspector clean.  Will child everything to the board.
	private Transform board;

	public int amountSpawned;


	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		//gridPos = new List<Vector2> ();
		amountSpawned = gridPos.Count;
	}

	/// <summary>
	/// Creates the list.
	/// this is used to create the list of grid positions. 
	/// 
	/// </summary>
	void CreateList(){
		gridPos.Clear ();

		for (int i = 0; i < theColumns; i++) {
			for (int j = 0; j < theRows; j++) {
				gridPos.Add (new Vector2 (i, j));
			}
		}
	}
	/// <summary>
	/// Sets up board.
	/// this will set up the under layer of the board, the grass in this case.  It'll give the base of placing other objects on top of it
	/// </summary>
	void SetUpBoard(){
		board = new GameObject ("Board").transform;

		for (int i = - 1; i < theColumns + 1; i++) {
			for (int j = - 1; j < theRows + 1; j++) {
				GameObject spawnObject = grassTiles [Random.Range (0, grassTiles.Length)];
				//this is to make the boarder
				if (i == -1 || i == theColumns || j == -1 || j == theRows) {
					spawnObject = boarderTiles;
				}

				GameObject instance = Instantiate (spawnObject, new Vector2 (i * 2, j * 2), Quaternion.identity);

				instance.transform.SetParent (board);
			}
		}
	}

	/// <summary>
	/// Randomizes the position.
	/// this is to set other objects on top of the grass, like water, trees, and other stuff.  
	/// Will make sure that is it spawns on any point of interest, the river, the rock and what not, it'll get deleted or moved
	/// </summary>
	/// <returns>The position.</returns>
	Vector2 RandomPosition(){
		int randomIndex = Random.Range (0, gridPos.Count);
		Vector2 randomPos = gridPos [randomIndex];
		gridPos.RemoveAt (randomIndex);
		amountSpawned--;
		return randomPos;
	}

	/// <summary>
	/// Objects the layout.
	/// </summary>
	/// <param name="tiles">Tiles.</param>
	/// <param name="max">Max.</param>
	/// <param name="min">Minimum.</param>
void ObjectLayout(GameObject[] tiles, int max, int min)
{
	objectCount = Random.Range(min, max + 1);
		for (int i = 0; i < objectCount; i++)
		{
			float randomNum;
			randomNum = Random.Range(-.5f, .5f);
			Vector2 randomPos = RandomPosition();
			GameObject tileToUse = tiles[Random.Range(0, tiles.Length)];
			Vector2 posToPlaceAt = new Vector2((randomPos.x * 2) + randomNum, (randomPos.y * 2) + randomNum);

			//Places fire and adds it to a list
			//Quaternion.Euler(-90, 0, 0) rotates it so it's visible in game
			if (tiles[0].gameObject.tag == "Fire")
			{
				GameObject fireInstance = Instantiate(tileToUse, posToPlaceAt, Quaternion.Euler(-90, 0, 0)) as GameObject;
				fireList.Add(fireInstance);
			}

			//Places rest of tiles
			else
			{
				Instantiate(tileToUse, posToPlaceAt, Quaternion.identity);
			}
		}
	}
	/// <summary>
	/// Sets up scene.
	/// This is called from the controller to set up the scene from the start
	/// </summary>
	public void SetUpScene(){
		SetUpBoard ();
		CreateList ();
		ObjectLayout (waterTiles, waterCount.theMax, waterCount.theMin);
		ObjectLayout (animal, animalCount.theMax, animalCount.theMin);
		ObjectLayout (tree, treeCount.theMax, treeCount.theMin);
	}

	public void Respawn(){
		Debug.Log ("HHHH");
		if (amountSpawned <= 0 || gameController.gameTimer < 10) {
			return;
		}
		Debug.Log ("H");
		if (gameController.isStorm == true) {
			Debug.Log ("HEH");
			animalCount = new Count (10, 5);
			treeCount = new Count(15, 10);
			waterCount = new Count(15, 10);
			ObjectLayout (waterTiles, waterCount.theMax, waterCount.theMin);
			ObjectLayout (animal, animalCount.theMax, animalCount.theMin);
			ObjectLayout (tree, treeCount.theMax, treeCount.theMin);
		}

		if (gameController.isDrought == true) {
			Debug.Log ("fire");
			ObjectLayout(fire, fireCount.theMax, fireCount.theMin);
		}
	}

	//called every frame
	void Update(){
		//destroy all fire when switching to storm
		if (gameController.isStorm)
		{
			foreach (GameObject fireInstance in fireList)
			{
				Destroy(fireInstance);
			}
		}
	}
}

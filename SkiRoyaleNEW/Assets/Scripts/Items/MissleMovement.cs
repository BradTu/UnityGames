/**
 * This script contains the logic of how both missles move (which player to target)
 * Jason Komoda 11/3/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour
{
    private Transform targetPosition;
    public GameObject playerInFirst;
    public List<Transform> targets;
    GameController gameController;
    public List<GameObject> actualTargets;
    public List<GameObject> targetPlayers;
    public List<Transform> targetTransforms;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController.activePlayers.Count > 0)
        {
            if (gameController.activePlayers[0].tag.Contains("Player"))
            {
                playerInFirst = gameController.activePlayers[0].gameObject.transform.GetChild(0).gameObject;
            }
            else
            {
                playerInFirst = gameController.activePlayers[0].gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Get all players/AI in front of the player
    public List<Transform> GetTargets()
    {
        targetPlayers = new List<GameObject>(gameController.activePlayers);
        targetTransforms = new List<Transform>();

        //Removes self gameObject from target list
        for (int i = 0; i < targetPlayers.Count; i++){
            if (targetPlayers[i].tag == gameObject.tag)
            {
                targetPlayers.Remove(targetPlayers[i]);
            }
        }

        //copies list of targets to new list
        actualTargets = new List<GameObject>(targetPlayers);

        //put transforms of targets into new list only if the y value is greater
        for (int i = 0; i < actualTargets.Count; i++)
        {
            if (actualTargets[i].gameObject.transform.position.y > gameObject.transform.position.y)
            {
                targetTransforms.Add(actualTargets[i].transform);
            }
        }
        return targetTransforms;
    }

    //finds closest transform in list
    public Transform FindClosest(List<Transform> enemyTargets)
    {
        
        Transform closest = null;
        Vector3 dirToTarget;
        float closetDistanceSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform potentialTarget in enemyTargets)
        {
            dirToTarget = potentialTarget.position - currentPos;
            float distSqrToTarget = dirToTarget.sqrMagnitude;
            if (distSqrToTarget < closetDistanceSqr)
            {
                closetDistanceSqr = distSqrToTarget;
                closest = potentialTarget;
            }
        }
        return closest;
    }

    //finds the player in first place
    public Transform FindPlayerInFirst()
    {
        for (int i = 0; i < gameController.activePlayers.Count; i++)
        {
            if (playerInFirst.transform.position.y < gameController.activePlayers[i].transform.position.y)
            {
                playerInFirst = gameController.activePlayers[i].gameObject;
            }
        }
        targetPosition = playerInFirst.transform;
        return targetPosition;
    }
}

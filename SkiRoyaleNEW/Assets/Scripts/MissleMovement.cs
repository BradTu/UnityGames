using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour
{

    private GameObject player1;
    private GameObject player2;
    private GameObject AI1;
    private GameObject AI2;
    private GameObject AI3;
    private GameObject AI4;
    private GameObject AI5;
    private GameObject AI6;
    private Vector3 myPosition;
    private Transform targetPosition;
    public Transform[] targets;

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        AI1 = GameObject.FindGameObjectWithTag("AI 1");
        AI2 = GameObject.FindGameObjectWithTag("AI 2");
        AI3 = GameObject.FindGameObjectWithTag("AI 3");
        AI4 = GameObject.FindGameObjectWithTag("AI 4");
        AI5 = GameObject.FindGameObjectWithTag("AI 5");
        AI6 = GameObject.FindGameObjectWithTag("AI 6");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetTargets()
    {
        if (gameObject.tag == "P1Sprite")
        {
            GameObject[] targetObjects = { player2, AI1, AI2, AI3, AI4, AI5, AI6};
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "P2Sprite")
        {
            GameObject[] targetObjects = { player1, AI1, AI2, AI3, AI4, AI5, AI6};
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 1")
        {
            GameObject[] targetObjects = { player1, player2, AI2, AI3, AI4, AI5, AI6 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 2")
        {
            GameObject[] targetObjects = { player1, player2, AI1, AI3, AI4, AI5, AI6 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 3")
        {
            GameObject[] targetObjects = { player1, AI1, AI2, player2, AI4, AI5, AI6 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 4")
        {
            GameObject[] targetObjects = { player1, AI1, AI2, AI3, player2, AI5, AI6 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 5")
        {
            GameObject[] targetObjects = { player1, AI1, AI2, AI3, AI4, player2, AI6 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
        else if (gameObject.tag == "AI 6")
        {
            GameObject[] targetObjects = { player1, AI1, AI2, AI3, AI4, AI5, player2 };
            targets = new Transform[targetObjects.Length];
            for (int i = 0; i < targetObjects.Length; i++)
            {
                targets[i] = targetObjects[i].transform;
            }
        }
    }

    public Transform FindClosest(Transform[] enemyTargets)
    {
        float closestDistance = (enemyTargets[0].position - gameObject.GetComponentInParent<Transform>().position).sqrMagnitude;
        int targetNumber = 0;
        for (int i = 0; i < enemyTargets.Length; i++)
        {
            float thisDistance = (enemyTargets[i].position - gameObject.GetComponentInParent<Transform>().position).sqrMagnitude;
            if (thisDistance < closestDistance)
            {
                closestDistance = thisDistance;
                targetNumber = i;
            }
        }
        return enemyTargets[targetNumber];
    }
}

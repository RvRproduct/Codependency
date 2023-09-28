using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class BeatLevel : MonoBehaviour
{
    public GameObject goalPoint;
    public GameObject playerOne;
    public GameObject playerTwo;

    private float playerDist = 4f;
    private float followBounds = 3f;
    private float goalDist = 1f;
    private bool reachedGoal;

    void Update()
    {
        
    }

    void SetPositionGoal()
    {
        if (reachedGoal)
        {
            if (playerDist > followBounds)
            {
                float xPosPlayerOne = Vector2.MoveTowards(playerOne.transform.position, goalPoint.transform.position, goalDist * Time.deltaTime).x;
                playerOne.transform.position = new Vector2(xPosPlayerOne, playerOne.transform.position.y);

                float xPosPlayerTwo = Vector2.MoveTowards(playerTwo.transform.position, playerOne.transform.position, playerDist * Time.deltaTime).x;
                playerTwo.transform.position = new Vector2(xPosPlayerTwo, playerTwo.transform.position.y);
               
            }
        }
        
            

    }
}

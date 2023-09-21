using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameSpawner : MonoBehaviour
{ 
    public float shameThreshold = 4f;
    public float maxThreshold = 30f;
    public float baseShameInterval = 4f;
    public float minShameInterval = 1f;
    [SerializeField] Controller controller;
    [SerializeField] GameObject shameBubble;
    float timePassed = 0f;

    float threshDiff;
    float maxTimeDelta;

    // Start is called before the first frame update
    void Start()
    {
        threshDiff = maxThreshold - shameThreshold;
        maxTimeDelta = baseShameInterval - minShameInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        // spawn shame one bubble at a time if player close to partner
        if (controller.playerDist < shameThreshold)
        {
            if (timePassed >= baseShameInterval) {
                SpawnBubble();
                timePassed = 0f;
            }
        }
        else
        {
            // spawn more proportionally to the distance over the threshold, down to a minimum of 1 second
            float distDiff = controller.playerDist - shameThreshold;
            float deltaTime = maxTimeDelta * (distDiff / threshDiff);

            if (timePassed >= baseShameInterval - Mathf.Min(deltaTime, maxTimeDelta))
            {
                SpawnBubble();
                timePassed = 0f;
            }
        }
    }

    void SpawnBubble()
    {
        // pick a random pos anywhere around the spawner
        float xDif = Random.Range(-1f, 1f);
        float yDif = Random.Range(-1f, 1f);
        Vector2 spawnPoint = new Vector2(transform.position.x + xDif, transform.position.y + yDif);

        GameObject newBubble = Instantiate(shameBubble, spawnPoint, Quaternion.identity);
        newBubble.GetComponent<ShameBubble>().player = controller.activePlayer;
        newBubble.GetComponent<ShameBubble>().partner = controller.nonActivePlayer;
    }
}

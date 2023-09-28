using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    public GameObject endscreen;
    public Controller controller;
    private Transform over;
    ColorChange colorShifter;
    ColorFlash colorFlasher;

    // shame stuff
    public float SHAME_LIMIT = 20;
    public float shameDamage = 1;
    public float healDist = 3;
    public float healRate = 2;
    float shameLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        colorShifter = gameObject.GetComponent<ColorChange>();
        colorFlasher = gameObject.GetComponent <ColorFlash>();
        over = endscreen.transform.Find("end");
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.playerDist <= healDist)
        {
            shameLevel -= healRate * Time.deltaTime;
            colorShifter.UpdateColor(shameLevel / SHAME_LIMIT);

        }
        shameLevel = Mathf.Max(0, shameLevel);
    }

    //detection of touch with spikes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            Lose();
        }
    }

    private void Lose()
    {
        Time.timeScale = 0f;
        Debug.Log("You Lose");
        over.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            Lose();
        }
    }

    public void ShamePlayer()
    {
        // adjust health
        shameLevel += shameDamage;

        // adjust color
        colorShifter.UpdateColor(shameLevel / SHAME_LIMIT);

        // color flash
        colorFlasher.FlashColor();

        // if too much shame, gameOver
        if (shameLevel >= SHAME_LIMIT)
        {
            Lose();
        }
    }

}

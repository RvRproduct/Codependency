using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    //public GameObject endscreen;
    //private Transform over;
    public int bulletspeed;
    public int lifespan;
    // Start is called before the first frame update
    void Start()
    {
        //over = endscreen.transform.Find("end");
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * Time.deltaTime * bulletspeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cubeB")
        {
           // Lose();
        }
    }
    private void Lose()
    {
        //Time.timeScale = 0f;
        //Debug.Log("You Lose");
        //over.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeB")
        {
            //Lose();
        }
    }
}

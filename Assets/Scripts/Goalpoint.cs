using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalpoint : MonoBehaviour
{
    bool cubeA;
    bool cubeB;
    public GameObject endscreen;
    private Transform over;
    // Start is called before the first frame update
    void Start()
    {
        over = endscreen.transform.Find("end");
    }

    // Update is called once per frame
    void Update()
    {
        Win();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "cubeA")
        {
            cubeA = true;
        }
        if (collision.gameObject.tag == "cubeB")
        {
            cubeB = true;
        }
    }

    private void Win()
    {
        if (cubeA && cubeB) 
        {
            Time.timeScale = 0f;
            Debug.Log("You Win");
            over.gameObject.SetActive(true);
        }
    }
}

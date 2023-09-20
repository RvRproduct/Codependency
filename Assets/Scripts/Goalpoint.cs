using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goalpoint : MonoBehaviour
{
    bool cubeA;
    bool cubeB;
    public GameObject endscreen;
    private Transform over;
    // Start is called before the first frame update
    void Start()
    {
        over = endscreen.transform.Find("win");
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cubeA")
        {
            cubeA = false;
        }
        if (collision.gameObject.tag == "cubeB")
        {
            cubeB = false;
        }
    }

    private void Win()
    {
        if (cubeA && cubeB) 
        {
            //Time.timeScale = 0f;
            Debug.Log("You Win");
            over.gameObject.SetActive(true);
        }
    }
    public void Playagain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Start Menu");
    }
}

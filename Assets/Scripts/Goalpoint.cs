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
    public int lvl;
    private float counter = 3f;
    private bool goals;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        goals = false;
        Time.timeScale = 1.0f;
        over = endscreen.transform.Find("win");

    }

    // Update is called once per frame
    void Update()
    {
        if (cubeA && cubeB)
        {
            goals = true; 
            animator.SetTrigger("goal");
            //counter -= Time.deltaTime;
        }
        if (goals)
        {
            counter -= Time.deltaTime;
        }
        //counter -= Time.deltaTime;
        if (counter < 0)
        {
            Win();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA")
        {
            cubeA = true;
        }
        if (collision.gameObject.tag == "cubeB")
        {
            cubeB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
        
        if (lvl == 1)
            {
                //Time.timeScale = 0f;
                SceneManager.LoadScene("Text 2");
            }
         else if (lvl == 2) {
                //Time.timeScale = 0f;
                SceneManager.LoadScene("Text 3");
            }
         else
            {
                //Time.timeScale = 0f;
                //Debug.Log("You Win");
                SceneManager.LoadScene("Text End");
                //over.gameObject.SetActive(true);
            }

    }
    public void Playagain()
    {
        Time.timeScale = 1.0f;
        if (lvl == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (lvl == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (lvl == 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Start Menu");
    }
}

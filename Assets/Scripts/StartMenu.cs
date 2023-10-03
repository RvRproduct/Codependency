using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject block;
    private float counter = 1.6f;
    private bool started;

    void Update()
    {
        if (started) 
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                SceneManager.LoadScene("Text 1");
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
        Destroy(block);
        started = true;
    }
}

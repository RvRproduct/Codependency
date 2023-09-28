using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject block;
    private float counter = 1f;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        //AudioSource.PlayClipAtPoint(clip, this.transform.position, 1f);
        Time.timeScale = 1.0f;
        Destroy(block);
        //counter -= Time,deltatime;
        started = true;
        //SceneManager.LoadScene("Text 1");

    }
}

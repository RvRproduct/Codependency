using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //AudioSource.PlayClipAtPoint(clip, this.transform.position, 1f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;
using Touch = UnityEngine.Touch;

public class NextScene : MonoBehaviour
{

    // private Scene[] scenes;
    private List<string> sceneNames;
    private Scene currentScene;
    private string nextScene;

    void Start()
    {
        sceneNames = new List<string>();
        currentScene = SceneManager.GetActiveScene();
        sceneNames.Add("Start Menu");
        sceneNames.Add("Level 1");
        sceneNames.Add("Text 1");
        sceneNames.Add("Level 1");
        sceneNames.Add("Text 2");
        sceneNames.Add("Level 2");
        sceneNames.Add("Text 3");
        sceneNames.Add("Level 3");
        sceneNames.Add("Text End");

        //foreach (Scene scene in scenes)
        //{
        //    Debug.Log(scene.name);
        //    sceneNames.Add(scene.name);
        //}

        TypeControlInScene();
    }

    void Update()
    {
        ScreenTapMoveScene();
    }


    void TypeControlInScene()
    {
        foreach (string scene in sceneNames)
        {

            if (currentScene.name == scene)
            {
                if (sceneNames.Count > sceneNames.IndexOf(scene) + 1)
                {
                    nextScene = sceneNames[sceneNames.IndexOf(scene) + 1];
                }
                else
                {
                    nextScene = sceneNames[0];
                }
                
                Debug.Log(nextScene);
            }
        }
    }

    void ScreenTapMoveScene()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.tapCount > 0)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTutorial()
    {
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
    }
}

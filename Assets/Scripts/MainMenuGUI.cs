using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject mainSprites;
    [SerializeField] GameObject controlsOne;

    // Start is called before the first frame update
    void Start()
    {
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        mainSprites.SetActive(true);
        controlsOne.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTutorial()
    {
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        mainSprites.SetActive(false);
        controlsOne.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void HideTutorial()
    {
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        mainSprites.SetActive(true);
        controlsOne.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
}

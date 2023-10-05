using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] Button pauseButton;
    [SerializeField] GameObject GameUi;
    [SerializeField] GameObject GetCanvas;

    public int level;
    public bool isPaused;
    private Controller controller;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = Controller.Instance;

        // overall game values
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        // set GUI visibility
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        helpScreen.SetActive(false);
    }

    public void LoseGame()
    {
        controller.menuActive = true;
        Time.timeScale = 0f;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void WinLevel()
    {

        if (level == 1)
        {
            SceneManager.LoadScene("Text 2");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Text 3");
        }
        else
        {
            SceneManager.LoadScene("Text End");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        if (level == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        else if (level == 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Start Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        isPaused = true;
        pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        Invoke(nameof(DelayTouchControls), 0.1f);
        
        pauseButton.gameObject.SetActive(true);
    }
    public void DisplayTutorial()
    {
        helpScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void HideTutorial()
    {
        helpScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    void DelayTouchControls()
    {
        isPaused = false;
    }

    void UpdateGameUiSize()
    {
        GameUi.GetComponent<RectTransform>().sizeDelta = GetCanvas.GetComponent<RectTransform>().sizeDelta;
    }
}

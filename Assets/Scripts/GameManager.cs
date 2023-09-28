using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text followingText;

    public int level;
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
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        followingText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.followPlayer)
        {
            followingText.text = "Following";
        }
        else
        {
            followingText.text = "Not Following";
        }
    }

    public void LoseGame()
    {
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
}

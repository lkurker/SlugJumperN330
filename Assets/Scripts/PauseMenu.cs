using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour { 

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

 
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                if(EndLevel.levelIsComplete == false)
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("OpeningScene");

        //we will user playerprefs to set the current level into a "saved state" so that the player can continue where they left off
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("lastLevelPlayed", currentLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");

        //we will do the same thing as when you load the main menu
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("lastLevelPlayed", currentLevel);
        Debug.Log(currentLevel);
    }
}

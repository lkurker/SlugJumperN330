using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    int lastLevelPlayed;
    
    

    // play button
    public void playGame() 
    {
    // change to the game scene
        // we will base which level the player will continue on from the playerprefs
        if(PlayerPrefs.GetInt("lastLevelPlayed") == 1)
        {
            SceneManager.LoadScene("LevelOne");
        }
        else if(PlayerPrefs.GetInt("lastLevelPlayed") == 3)
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if (PlayerPrefs.GetInt("lastLevelPlayed") == 4)
        {
            SceneManager.LoadScene("LevelThree");
        }
        else if (PlayerPrefs.GetInt("lastLevelPlayed") == 5)
        {
            SceneManager.LoadScene("LevelFive");
        }
        else if (PlayerPrefs.GetInt("lastLevelPlayed") == 6)
        {
            SceneManager.LoadScene("LevelSix");
        }
        else if (PlayerPrefs.GetInt("lastLevelPlayed") == 7)
        {
            SceneManager.LoadScene("LevelSeven");
        }
        else
        {
            SceneManager.LoadScene("LevelOne");
        }

        // Debug.Log("Launch lvl 1");
    }

    // load button
    public void chooseLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    //quit button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    // level select screen
    // level one button
    public void levelOneSelect()
    {
        // change to level one game scene
        SceneManager.LoadScene("LevelOne");
        // Debug.Log("Launch lvl 1");
    }
    // level two button
    public void levelTwoSelect()
    {
        // change to level two game scene
        SceneManager.LoadScene("LevelTwo");
        // Debug.Log("Launch lvl 2");
    }

    // level three button
    public void levelThreeSelect()
    {
        // change to level three game scene
        SceneManager.LoadScene("LevelThree");
        // Debug.Log("Launch lvl 3");
    }

    //level four button
    public void levelFourSelect()
    {
        SceneManager.LoadScene("LevelFive");
    }

    //level five button
    public void levelFiveSelect()
    {
        SceneManager.LoadScene("LevelSix");
    }

    //level six button
    public void levelSixSelect()
    {
        SceneManager.LoadScene("LevelSeven");
    }

    // back button
    public void backButton()
    {
        // take player back to main menu
        SceneManager.LoadScene("OpeningScene");
    }

    
    
}

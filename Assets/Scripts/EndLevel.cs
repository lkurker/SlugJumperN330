using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject ContinueScreen;
    public static bool levelIsComplete;

    // Start is called before the first frame update
    void Start()
    {
        ContinueScreen.SetActive(false);
        levelIsComplete = false;
        Time.timeScale = 1f;
    }
    void Update()
    {
        if(levelIsComplete == true)
        {
            levelEnded();
        }
        else
        {
            ContinueScreen.SetActive(false);
        }
    }

   public void levelEnded()
    {
        Time.timeScale = 0f;
        ContinueScreen.SetActive(true);
    }

    public void nextLevel()
    {

        //int value that keeps track of what level the player is currently on
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if(currentLevel == 3)
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if(currentLevel == 4)
        {
            SceneManager.LoadScene("LevelThree");
        }
        else if(currentLevel == 5)
        {
            SceneManager.LoadScene("LevelFive");
        }
        else if (currentLevel == 6)
        {
            SceneManager.LoadScene("LevelSix");
        }
        else if (currentLevel == 7)
        {
            SceneManager.LoadScene("LevelSeven");
        }
        else if (currentLevel == 8)
        {
            SceneManager.LoadScene("BossLevel");
        }
        else
        {
            Time.timeScale = 1f;
            ContinueScreen.SetActive(false);
            levelIsComplete = false;
            SceneManager.LoadScene("OpeningScene");

        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
    }


}

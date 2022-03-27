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
    }
    void Update()
    {
        if(levelIsComplete == true)
        {
            levelEnded();
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

        if(currentLevel == 2)
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if(currentLevel == 4)
        {
            SceneManager.LoadScene("LevelThree");
        }
        else
        {
            SceneManager.LoadScene("OpeningScene");
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
    }


}

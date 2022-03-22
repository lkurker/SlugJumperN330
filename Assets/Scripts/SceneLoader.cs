using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // play button
    public void playGame() 
    {
    // change to the game scene
SceneManager.LoadScene ("LevelOne");
    }

    //quit button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSoundScript : MonoBehaviour
{
    private Scene scene;
    private int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        currentScene = scene.buildIndex;

        DontDestroyOnLoad(this.gameObject);
        
        
    }

    void Update()
    {
        if(currentScene != SceneManager.GetActiveScene().buildIndex)
        {
            Invoke("destroyAudio", 1);
        }

    }

    void destroyAudio()
    {
        Destroy(this.gameObject);
    }
   
}

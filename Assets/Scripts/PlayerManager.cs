using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Vector2 lastCheckPointPos;

    private AudioSource SlugSplat1;

    //static bool to indicate if the player has died recently
    public static bool hasDiedRecently;

    public float killCooldown = 5;

    

    // Start is called before the first frame update
    void Start()
    {
        hasDiedRecently = false;
        SlugSplat1 = GameObject.FindGameObjectWithTag("PlayerDeathSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SlugSplat1 = GameObject.FindGameObjectWithTag("PlayerDeathSound").GetComponent<AudioSource>();

        if (this.transform.position.y < -10)
       {
            Respawn();

            SlugSplat1.Play();
       }

        //check to see if slugboy has recently died
        if(hasDiedRecently == true)
        {
            Invoke("canKillAgain", killCooldown);
        }


    }

    //respawn method that places slug boi back at the last checkpoint
    void Respawn()
    {
        hasDiedRecently = true;
        this.gameObject.SetActive(false);
        SlugSplat1.Play();
        //delay the respawning of slug boy
        Invoke("lastCheckpointReached", 2);
    }

    //collision method to determine if the user has come into contact with any hazardous materials in the world
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Respawn();

            
        }

        if(collision.transform.tag == "Spike")
        {
            Respawn();

            
        }
    }

    //set slugboy back to his previous position
    void lastCheckpointReached()
    {
        this.gameObject.SetActive(true);
        this.transform.position = lastCheckPointPos;

    }

    void canKillAgain()
    {
        hasDiedRecently = false;
    }
}

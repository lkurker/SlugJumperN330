using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Vector2 lastCheckPointPos;

    private AudioSource SlugSplat1;

    // Start is called before the first frame update
    void Start()
    {
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


    }

    //respawn method that places slug boi back at the last checkpoint
    void Respawn()
    {
        this.transform.position = lastCheckPointPos;
    }

    //collision method to determine if the user has come into contact with any hazardous materials in the world
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Respawn();

            SlugSplat1.Play();
        }

        if(collision.transform.tag == "Spike")
        {
            Respawn();

            SlugSplat1.Play();
        }
    }
}

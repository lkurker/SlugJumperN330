using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Vector2 lastCheckPointPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(this.transform.position.y < -10)
       {

            Respawn();
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
        }

        if(collision.transform.tag == "Spike")
        {
            Respawn();
        }
    }
}

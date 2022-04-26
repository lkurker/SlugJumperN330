using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : MonoBehaviour
{
    private Transform slug;
    private bool collided = false;
    private bool beginFiring = false;
    private Vector2 direction;
    private Rigidbody2D rb;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    //method to set the target for the player
    public void chaseTarget(Transform _slug, float _speed)
    {
        Debug.Log("????");
        slug = _slug;
        speed = _speed;
        direction = slug.position - this.transform.position;
        beginFiring = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if the projectile hasn't hit anything yet
        if(collided == false && beginFiring == true)
        {
            Debug.Log("IS THIS WORKING?!?!?!");
            //set the velocity of the rigidbody in the direction of the slug
            rb.velocity = direction * speed * Time.deltaTime;
        }
    }

    //collision method for when the bullet hits something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "Spike" || collision.transform.tag == "Wall")
        {
            collided = true;
            Destroy(gameObject);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugMovement : MonoBehaviour
{

    //reference to our player's rigidbody
    Rigidbody2D rb;

    public float speed;
    public float maxSpeed;

    //since the baseSpeed will always be equal to the initial speed value, there is no need to make it public
    private float baseSpeed;

    //the momentum rate can be changed so that you can choose how quickly the player's speed increases as they move
    public float momentumRate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // we will set the baseSpeed value to the value of the speed value, so that whenever a character stops moving, their speed will revert to it's original value
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //we must call our player's movement method during each frame
        Move();
    }

    void Move()
    {
        //we will implement the momentum system within the movement method, which will cause the player character to gradually speed up
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (speed < maxSpeed)
            {
                speed += momentumRate * Time.deltaTime;
            }
        }

        //if the player is not holding down the movement key, revert the speed back
        else
        {
            speed = baseSpeed;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);


    }
}

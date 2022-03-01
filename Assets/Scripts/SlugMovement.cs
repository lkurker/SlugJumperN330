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

    //just an idea here for us to mess with, these variables will allow it so you can increase your jump if you hold the space bar down longer.
    //If we decide we don't like this, just set these two variables equal to one another.
    public float jumpHeight;
    

    private bool isGrounded;
    public Transform feetPos;
    public float radius;
    public LayerMask Ground;

    //variables to calculate how long the player can jump
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

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
        //each frame we must checkt to see if the player is in a spot where they can jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, radius, Ground);

        //if the player hits the space key and is grounded, they can jump
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpHeight;
        }

        //now we will test to see if the player is holding down the space key
        if (Input.GetKey(KeyCode.Space))
        {
            if(jumpTimeCounter > 0 && isJumping == true)
            {
                rb.velocity = Vector2.up * jumpHeight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        
    }

    void FixedUpdate()
    {
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

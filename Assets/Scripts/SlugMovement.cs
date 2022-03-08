using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugMovement : MonoBehaviour
{

    //reference to our player's rigidbody
    Rigidbody2D rb;

    //public bool value that determines how the slug sliding works
    public bool checkKey;

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

    //the following variables will apply directly to the player character sticking to walls
    private bool touchingWall;
    public Transform checkFront;
    private bool wallStick;
    public float slideSpeed;

    //variables that will allow our slug to jump off walls
    bool stickJump;
    public float xStickForce;
    public float yStickForce;
    public float stickJumpTime;

    //private variable that determines if the slug can continue sticking to the wall or not
    private bool canStick;

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
        float movement = Input.GetAxisRaw("Horizontal");

        //each frame we must checkt to see if the player is in a spot where they can jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, radius, Ground);

        //if the player hits the space key and is grounded, they can jump
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpHeight;
        }

        //now we will test to see if the player is holding down the space key
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0 && isJumping == true)
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

        //by having the ground layer as a parameter even for the checkFront bool, we can have a much easier time creating areas for the slug to slide on
        touchingWall = Physics2D.OverlapCircle(checkFront.position, radius, Ground);

        //the player's rotation will determine the mechanics involving moving away from the wall while slugboy is falling
        if (this.gameObject.transform.rotation.eulerAngles.y == 0)
        {
            //checking to see if the player is connected to a wall. they must release both the left and right movement key in order to free fall and stop sticking
            if (touchingWall == true && isGrounded == false && checkKey == false && (Input.GetKey(KeyCode.D) || canStick == true))
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    wallStick = true;
                    canStick = true;
                }

                else
                {
                    wallStick = false;
                    canStick = false;
                }

            }
            else if (touchingWall == true && isGrounded == false && checkKey == true)
            {
                wallStick = true;
            }

            else
            {

                wallStick = false;
                canStick = false;

            }
        }

        else
        {
            //checking to see if the player is connected to a wall. they must release both the left and right movement key in order to free fall and stop sticking
            if (touchingWall == true && isGrounded == false && checkKey == false && (Input.GetKey(KeyCode.A) || canStick == true))
            {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                {
                    wallStick = true;
                    canStick = true;
                }
                else
                {
                    wallStick = false;
                    canStick = false;
                }

            }
            else if (touchingWall == true && isGrounded == false && checkKey == true)
            {
                wallStick = true;
            }

            else
            {
                wallStick = false;
                canStick = false;
            }
        }


        //check to see if the slug is sticking to a wall
        if (wallStick == true)
        {
            //we are constraining the speed at which the velocity of the character takes them down by clamping the y value between two seperate values
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
        }

        else if (wallStick == false)
        {

            Move();
        }

        //check to see if the player has initiated a wall jump
        if (wallStick == true && Input.GetKeyDown(KeyCode.Space))
        {
            stickJump = true;
            //timer to set the wall jump to false
            Invoke("stickJumpFalse", stickJumpTime);
        }

        if (stickJump == true)
        {
            //if else statements to determine which direcion the player character will move whe jumping based on their current rotation
            if (this.gameObject.transform.rotation.y == 0)
            {
                rb.velocity = new Vector2(xStickForce * -1, yStickForce);
            }

            else
            {
                rb.velocity = new Vector2(xStickForce * 1, yStickForce);
            }

        }




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

        if (x == -1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if (x == 1)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);


    }

    void stickJumpFalse()
    {
        Debug.Log("Test");
        stickJump = false;
    }



}

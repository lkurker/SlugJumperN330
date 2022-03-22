using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public boolean variable that willd determine if the enemy begins by moving right or left
    public bool moveRight;

    Rigidbody2D rb;
    public float speed;
    public int delayTime;
    private float x;
    private float xTurn;
    private int counter;

    //IMPORTANT: always make this variable a position value!!!!
    public float moveDistance;
    private float moveDestination;

    //we will use this vector to calculate the movement of the enemy
    public static Vector2 startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPoint = this.transform.position;

        //these if statements will what the private variable moveDestination becomes
        if (moveRight == true)
        {
            x = 1;
            xTurn = -1;
            moveDestination = this.transform.position.x + moveDistance;
        }
        else if(moveRight == false)
        {
            x = -1;
            xTurn = 1;
            moveDestination = this.transform.position.x - moveDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //these if statements will determine if the enemy should pause for a moment or continue on their movement path
        if(moveRight == true)
        {
            if (this.transform.position.x > moveDestination || this.transform.position.x == startingPoint.x || this.transform.position.x < startingPoint.x)
            {
                Debug.Log("Testing");
                counter = 0;
                Invoke("Move", delayTime);
            }
            else
            {
                Move();
            }
        }

        else 
        {
            if (this.transform.position.x < moveDestination || this.transform.position.x == startingPoint.x || this.transform.position.x > startingPoint.x)
            {
                Debug.Log("Testing");
                counter = 0;
                Invoke("Move", delayTime);
            }
            else
            {
                Move();
            }
        }
        
    }

    void Move()
    {
        //number of if statements to automatically determine which direction an enemy should be facing, as well as where they should be headed
        if (moveRight == true)
        {
            if (counter != 1)
            {
                if (this.transform.position.x > moveDestination && moveRight == true)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    counter = 1;
                }
                else if (this.transform.position.x < startingPoint.x && moveRight == true)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    counter = 1;
                }
            }

            if (this.gameObject.transform.rotation.y == 0 && moveRight == true)
            {
                rb.velocity = new Vector2(speed * x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * xTurn, rb.velocity.y);
            }


        }

        else if(moveRight == false)
        {
            if(counter != 1)
            {
                if (this.transform.position.x < moveDestination && moveRight == false)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    counter = 1;
                }
                else if (this.transform.position.x > startingPoint.x && moveRight == false)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    counter = 1;
                }
            }

            if (this.gameObject.transform.rotation.y == 0 && moveRight == false)
            {
                rb.velocity = new Vector2(speed * xTurn, rb.velocity.y);
            }
            else 
            {
                Debug.Log("Testing2");
                rb.velocity = new Vector2(speed * x, rb.velocity.y);
            }

        }

        
        
        

        
       
    }
}

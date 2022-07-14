
using UnityEngine;

public class UpdatedCamera : MonoBehaviour
{
    public Transform slugBoy;
    public float xDistance;
    private float reversexDistance;
    public float smoothness;
    public float pullBack;
    public float cameraMomentum;
    private float newXposition;
    private float newYposition;
  

    //float variables to determine how long the user has been holding down in one direction or another
    private float moveRight;
    private float moveLeft;
    private Vector3 offset;

    //private floats for looking up and down
    private float upDirection;
    private float downDirection;
    
    

    void Start()
    {
        moveRight = 5;
        moveLeft = 5;
        offset = new Vector3(0, 0, pullBack);

        upDirection = 5;
        downDirection = -5;

        //we will convert the reverse x transformation of the camera to be the negative value of the xDistance value that the user already put in
        reversexDistance = xDistance * -1;
    }

    private void FixedUpdate()
    {
        if(SlugMovement.touchingWall == false)
        {
            cameraShift();
        }

        //if the player is not trying to look up or down then the camera will follow them
        if ((!Input.GetKey(KeyCode.W)) && (!Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            //in the case where the player no longer is looking up or down, reset the newYposition value
            newYposition = 0;
            offset = new Vector3(newXposition, newYposition, pullBack);
            Follow();
        }

        //else if statements for the player to look up or down
        else if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            LookUp();
        }

        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            LookDown();
        }
        
    }

    void Follow()
    {
        Vector3 targetPosition = slugBoy.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    private void cameraShift()
    {
        if (Input.GetKey(KeyCode.D))
        {
            moveRight += moveRight * Time.deltaTime;
            moveLeft = 5;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            moveLeft += moveLeft * Time.deltaTime;
            moveRight = 5;

        }

        else
        {
            moveLeft = 5;
            moveRight = 5;
        }

        //if the player is moving right for an extended period of time
        if(moveRight > 10)
        {
            shiftRight();
        }

        //if the player is moving left for an extended period of time
        else if(moveLeft > 10)
        {
            shiftLeft();
        }
    }

    private void shiftRight()
    {
        

        if (newXposition < xDistance)
        {
            newXposition += cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, 0, pullBack);
        }

    }

    private void shiftLeft()
    {
        
        if(newXposition > reversexDistance)
        {
            newXposition -= cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, 0, pullBack);
        }
    }

    //methods for letting the player look up and down
    private void LookUp()
    {
        if(newYposition < upDirection)
        {
            newYposition += cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, newYposition, pullBack);
            Follow();
        }
        Debug.Log("Looking up");
    }

    private void LookDown()
    {
        if (newYposition > downDirection)
        {
            newYposition -= cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, newYposition, pullBack);
            Follow();
        }
        Debug.Log("Looking down");
    }

    


}
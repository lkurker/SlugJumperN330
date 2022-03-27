
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

    //float variables to determine how long the user has been holding down in one direction or another
    private float moveRight;
    private float moveLeft;
    private Vector3 offset;
    

    void Start()
    {
        moveRight = 5;
        moveLeft = 5;
        offset = new Vector3(0, 0, pullBack);

        //we will convert the reverse x transformation of the camera to be the negative value of the xDistance value that the user already put in
        reversexDistance = xDistance * -1;
    }

    private void FixedUpdate()
    {
        if(SlugMovement.touchingWall == false)
        {
            cameraShift();
        }
        Follow();
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
        Debug.Log("Move Right");

        if (newXposition < xDistance)
        {
            newXposition += cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, 0, pullBack);
        }

    }

    private void shiftLeft()
    {
        Debug.Log("Move Left");
        if(newXposition > reversexDistance)
        {
            newXposition -= cameraMomentum * Time.deltaTime;
            offset = new Vector3(newXposition, 0, pullBack);
        }
    }


}
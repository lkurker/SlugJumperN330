using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyScript : MonoBehaviour
{
    //public object variable to track if the graphic should rotate or not
    public AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateEnemy();
    }

    //method used to rotate the enemy sprite if needed
    public void rotateEnemy()
    {
        //check the velocity that the enemy is currently following
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
